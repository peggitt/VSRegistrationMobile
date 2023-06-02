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
            
            var householdIds = await App.db.Table<Household>().Where(i => i.IsComplete).ToListAsync();
            total = householdIds.Count();
                if (total != 100)
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

                    bool answer = await Application.Current.MainPage.DisplayAlert("Confirm?", $"Upload {total} Household(s)?", "Yes", "No");
                    if (answer)
                    {
                        _api = new ApiService(AppConstants.BaseApiAddress);
                        if (IsBusy)
                            return;
                        IsBusy = true;

                        try
                        {

                            householdIds.ForEach(i => i.UserName = App.User.Email);

                            count = 1;
                            foreach (var household in householdIds)
                            {
                                CurrentStatus = $"Uploading {count}/{total}";
                                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(household), Encoding.UTF8, "application/json");
                                var response = await _api.AddHousehold(content, $"Bearer {App.User.Token}");

                                if (response.Message != null)
                                {
                                    household.IsComplete = true;
                                    var xtics = await App.db.Table<HouseholdCharacteristic>().FirstAsync(i => i.HouseholdId == household.HouseholdId);
                                    xtics.UserName = App.User.Email;
                                    content = new StringContent(System.Text.Json.JsonSerializer.Serialize(xtics), Encoding.UTF8, "application/json");
                                    response = await _api.AddHHCharacteristicscreate(content, $"Bearer {App.User.Token}");
                                    if (response.Message != null)
                                    {
                                    }
                                    var members = await App.db.Table<HouseholdMember>().Where(i => i.HouseholdId == household.HouseholdId).ToListAsync();
                                    content = new StringContent(System.Text.Json.JsonSerializer.Serialize(members), Encoding.UTF8, "application/json");
                                    response = await _api.AddHouseholdMembers(content, $"Bearer {App.User.Token}");
                                    if (response.Message != null)
                                    {
                                    }
                                }

                                count++;
                            }

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


    }
}
