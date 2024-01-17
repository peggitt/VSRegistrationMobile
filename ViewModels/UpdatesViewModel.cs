using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HSNP.Constants;
using HSNP.Interfaces;
using HSNP.Mobile;
using HSNP.Models;
using HSNP.Services;
using IntelliJ.Lang.Annotations;
using Java.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace HSNP.ViewModels
{
    public partial class UpdatesViewModel : BaseViewModel
    {
        int count;
        int total;
        private IApi _api;
        public UpdatesViewModel(IApi api, INavigation navigation) : base(navigation)
        {
        }

        [ObservableProperty]
        private string householdId;
        [ObservableProperty]
        private string nationalIdNo;
        [ObservableProperty]
        private string currentStatus;
        


        [RelayCommand]
        private async Task Next()
        {
            if(string.IsNullOrEmpty(HouseholdId) && string.IsNullOrEmpty(NationalIdNo)) {
                await Toast.SendToastAsync("Household ID or National ID No. is required");
            }
            else {
                var household = new Household();
                if(NationalIdNo!=null)
                {
                    var householdId= (await App.db.Table<HouseholdMember>().FirstOrDefaultAsync(i => i.IdNo== NationalIdNo))?.HouseholdId;
                    var test = await App.db.Table<HouseholdMember>().ToListAsync();
                    household = await App.db.Table<Household>().FirstOrDefaultAsync(i => i.HouseholdId==householdId);
                }
                else
                {
                    household = await App.db.Table<Household>().FirstOrDefaultAsync(i => i.HouseholdId == HouseholdId);
                }
            
       
         
                if (household == null)
                {
                    if (!string.IsNullOrEmpty(HouseholdId) && !string.IsNullOrEmpty(HouseholdId))
                        await Application.Current.MainPage.DisplayAlert("Sorry", $"Household with Household ID {HouseholdId} and  National ID No. {NationalIdNo} not found.", "OK");
                    else if (!string.IsNullOrEmpty(HouseholdId))
                        await Application.Current.MainPage.DisplayAlert("Sorry", $"Household with Household ID {HouseholdId} not found.", "OK");
                    else
                        await Application.Current.MainPage.DisplayAlert("Sorry", $"Household with National ID No. {NationalIdNo} not found.", "OK");
                }
                else
                {
                        try
                        {


                            App.HouseholdId = household.HouseholdId;
                           await Shell.Current.GoToAsync($"/{nameof(UpdateHouseholdPage)}?HouseholdId={household.HouseholdId}");

                        }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");

                        }
                    
                }
            }

        }


    }
}
