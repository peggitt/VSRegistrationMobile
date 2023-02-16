using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using Refit;
using Rg.Plugins.Popup.Extensions;
using HSNP.Interface;
using HSNP.Models;
using HSNP.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HSNP.ViewModels
{
    public partial class SearchViewModel : BaseVm
    {
        [ObservableProperty]
        private bool isGate;
        private string _idNumber;
        public string IdNumber
        {
            get => _idNumber;
            set { _idNumber = value; OnPropertyChanged(); }
        }
        private string _phoneNo;
        public string PhoneNo
        {
            get => _phoneNo;
            set { _phoneNo = value; OnPropertyChanged(); }
        }

        private string _regNo;
        public string RegNo
        {
            get => _regNo;
            set { _regNo = value; OnPropertyChanged(); }
        }

        private readonly IApi _api;
        public  SearchViewModel(INavigation navigation, IApi api) : base(navigation)
        {
            Title = "Search";
            _api=api;
          
            // OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
        }

        public ICommand OpenWebCommand { get; }

        private Command _searchCommand;
        public Command SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command(async x => await Search()));
            }
        }
        private async Task Search()
        {
            
            if (string.IsNullOrEmpty(IdNumber) && string.IsNullOrEmpty(PhoneNo) && string.IsNullOrEmpty(RegNo))
                Toast.SendToast("Please specify a search parameter");
            else
            {
                // Do a local search
                RegNo = RegNo?.Trim();
                PhoneNo = PhoneNo?.Trim();
                IdNumber = IdNumber?.Trim();
                var visitor =await App.Database._database.Table<Visitor>()
                    .FirstOrDefaultAsync(i => i.IdNumber == IdNumber || i.PhoneNo==PhoneNo || i.RegNo==RegNo);
                if (visitor != null)
                {
                    // Check if User is logged in
                    var visit = await App.Database._database.Table<Visit>()
                                .FirstOrDefaultAsync(i=>i.VisitorId==visitor.Id);
                    if(visit!=null)
                        await Application.Current.MainPage.DisplayAlert("Sorry", "Visitor already checked-in.", "OK");
                    else
                    {
                        App.Visitor = visitor;
                        IdNumber = PhoneNo = RegNo= "";
                    //  await Navigation.PushAsync(new VisitPage());
                    }
                  
                }
                else
                {
                    // Do online search
                    try
                    {
                        IsBusy = true;
                        var vm = new SearchVm
                        {
                            RegNo = RegNo,
                            IdNumber = IdNumber,
                            PhoneNo = PhoneNo
                        };
                        var str = JsonConvert.SerializeObject(vm);
                        var response = await _api.Search(vm);
                      
                        if (response.Message.Contains("success") && response.Visitor != null)
                        {
                            RegNo = IdNumber = PhoneNo;
                            App.Database.AddOrUpdate(response.Visitor);
                            App.Visitor = response.Visitor;
                            IdNumber = PhoneNo = RegNo;
                           // await Navigation.PushAsync(new VisitPage());
                        }
                        else
                        {
                            Toast.SendToast("Visitor not found. Procede to register.");
                            App.Visitor = new Visitor
                            {
                                RegNo = RegNo,
                                IdNumber = IdNumber,
                                PhoneNo = PhoneNo
                            };
                            IdNumber = PhoneNo = RegNo="";
                            await Navigation.PushAsync(new RegisterPage());
                        }
                    }
                    catch(ApiException ex)
                    {
                        Toast.SendToast("ApiException "+ex.ToString());
                    }
                    catch(Exception ex)
                    {
                        //Toast.SendToast("Exception " + ex.ToString());
                        await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");
                    }
                    IsBusy = false;

                }
                
            }
           
          
            //    IsBusy = true;
            //    var vm = new VisitorSearchVm
            //    {

            //    };
            //    var response = await _api.CreateExpenseCategory(vm);
            //    IsBusy = false;
            //    if (response.Message.Contains("success"))
            //    {
            //        App.Database.AddOrUpdate(response.Category);
            //        Toast.SendToast("Expense Category saved successfully");
            //        var page = Navigation.NavigationStack.Last().Title;
            //        if (page == "Expenses Categories")
            //        {
            //            await Navigation.PopAsync();
            //            await Navigation.PushAsync(new ExpensesCategoriesPage());
            //            await Navigation.PopPopupAsync();
            //        }
            //        else
            //        {
            //            await Navigation.PopPopupAsync();
            //            await Navigation.PopPopupAsync();
            //            await Navigation.PushPopupAsync(new ExpensesAddPage());
            //        }

            //    }
            //    else
            //        Toast.SendToast(response.Message);
            //}


        }

    }
}