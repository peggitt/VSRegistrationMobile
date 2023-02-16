using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Refit;
using Rg.Plugins.Popup.Extensions;
using HSNP.Interface;
using HSNP.Models;
using HSNP.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HSNP.ViewModels
{
  
    public partial class RegisterViewModel : BaseVm
    {

      
        [ObservableProperty]
        private string idNumber;
        [ObservableProperty]
        private string phoneNo;

        [ObservableProperty]
        private string institution;

        [ObservableProperty]
        private string name;
        [ObservableProperty]
        private string regNo;
        [ObservableProperty]
        private List<SystemCodeDetailView> visitorTypes;
        [ObservableProperty]
        private List<SystemCodeDetailView> carModels;
        [ObservableProperty]
        private SystemCodeDetailView carModel;
       


        private SystemCodeDetailView visitorType;
        public SystemCodeDetailView VisitorType
        {
            get => visitorType;
            set {
                visitorType = value; OnPropertyChanged();
                IsDriver = visitorType.Code == "1";
            }
        }

        [ObservableProperty]
        private bool isDriver;

        private readonly string _option;

        private readonly IApi _api;
        public RegisterViewModel(INavigation navigation, IApi api,string option) : base(navigation)
        {
            _api = api;
            _option = option;
          
            _ = GetItemsAsync();

            if (App.Visitor != null)
            {
                RegNo = App.Visitor.RegNo;
                PhoneNo = App.Visitor.PhoneNo;
                IdNumber = App.Visitor.IdNumber;
                Institution = App.Visitor.Institution;
                Name = App.Visitor.Name;
                
            }
            if (option.Equals("new"))
            {
                App.Visitor = null;
                IsDriver = false;
            }
          

        }
        async Task GetItemsAsync()
        {
            VisitorTypes = await App.Database._database.Table<SystemCodeDetailView>().Where(i=>i.ParentCode=="VT").ToListAsync();
            if (App.Visitor != null)
                VisitorType = VisitorTypes.SingleOrDefault(i => i.Id == App.Visitor.VisitorTypeId);
           
            VisitorType = VisitorTypes.FirstOrDefault();
            CarModels = await App.Database._database.Table<SystemCodeDetailView>().Where(i => i.ParentCode == "CM").ToListAsync();
            if(App.Visitor?.CarModelId!=null)
             CarModel = CarModels.SingleOrDefault(i => i.Id == App.Visitor?.CarModelId);
        }
       [RelayCommand]
        private async Task Save()
        {
            var errors = "";
            if (string.IsNullOrEmpty(PhoneNo))
                errors += "Phone Number is required";
           
            if (VisitorType == null)
                errors += "Select Visitor Type";


            if (IsDriver && string.IsNullOrEmpty(RegNo))
                errors += "Car Reg No. is required";
            if (IsDriver && CarModel==null)
                errors += "Car Model is required";

            if (string.IsNullOrEmpty(Name))
                errors += "Name is required";
            if (string.IsNullOrEmpty(IdNumber))
                errors += "ID Number is required";
            if (string.IsNullOrEmpty(PhoneNo))
                errors += "Phone Number is required";

            if (!string.IsNullOrEmpty(errors))
                Toast.SendToast(errors);
            else
            {
                try
                {

                    var visitor = new Visitor
                    {
                        IdNumber = IdNumber,
                        Institution = Institution,
                        Name = Name,
                        PhoneNo = PhoneNo,
                        VisitorTypeId = VisitorType?.Id,
                        CarModelId = CarModel?.Id

                    };
                    if (!string.IsNullOrEmpty(RegNo))
                        visitor.RegNo = RegNo.ToUpper();
                    if (App.Visitor == null)
                        visitor.Id = Guid.NewGuid();

                    var vm = new RegisterVisitorVm
                    {
                        Visitor = visitor
                    };
                    IsBusy = true;
                    var str = JsonConvert.SerializeObject(vm);
                    var response = await _api.Register(vm);
                  
                    if (response.Message.Contains("success"))
                    {
                        visitor.CarModel = CarModel?.Name;
                        App.Visitor = visitor;
                        App.Database.AddOrUpdate(visitor);
                        Toast.SendToast("Visitor Registered Successfully");
                       
                    //    await Navigation.PushAsync(new VisitPage());
                       // await Navigation.PopAsync();


                    }
                    else
                        Toast.SendToast(response.Message);
                }

                catch (ApiException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");
                }
                IsBusy = false;
            }
        }

    }
}