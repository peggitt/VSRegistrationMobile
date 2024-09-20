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
    public partial class UpdatesSearchViewModel : BaseViewModel
    {
        int count;
        int total;
        
        public UpdatesSearchViewModel(IApi api, INavigation navigation) : base(navigation)
        {
            IsBusy = false;
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
           // HouseholdId= (await App.db.Table<Household>().FirstOrDefaultAsync()).HouseholdId;
          
            if (string.IsNullOrEmpty(HouseholdId) && string.IsNullOrEmpty(NationalIdNo)) {
                await Toast.SendToastAsync("Household ID or National ID No. is required");
            }
            else {
                IsBusy = true;
                var household = new Household();
                if(NationalIdNo!=null)
                {
                    var householdId= (await App.db.Table<HouseholdMember>().FirstOrDefaultAsync(i => i.IdNumber== NationalIdNo))?.HouseholdId;
                   // var test = await App.db.Table<HouseholdMember>().ToListAsync();
                    household = await App.db.Table<Household>().FirstOrDefaultAsync(i => i.HouseholdId==householdId);
                }
                else
                {
                    household = await App.db.Table<Household>().FirstOrDefaultAsync(i => i.HouseholdId == HouseholdId);
                }

                // Temp Delete
              //  if (household == null)
              //  household = await App.db.Table<Household>().FirstOrDefaultAsync();
                
                  

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
                        household.HouseholdMembers = await App.db.Table<HouseholdMember>().CountAsync(i => i.HouseholdId == household.HouseholdId);
                        household.Editting = true;
                        household.CreatedOn = DateTime.UtcNow;
                        App.Database.Update(household);
                        await Shell.Current.GoToAsync($"/{nameof(UpdateHouseholdPage)}?HouseholdId={household.HouseholdId}");
                       // await Shell.Current.GoToAsync($"/{nameof(UpdatesMembersPage)}?HouseholdId={household.HouseholdId}");

                    }
                        catch (Exception ex)
                        {
                            await Application.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");

                        }
                    
                }
                IsBusy = false;
            }

        }


    }
}
