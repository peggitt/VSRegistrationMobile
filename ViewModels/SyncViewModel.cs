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
    public partial class SyncViewModel : BaseViewModel
    {
        int count;
        int total;
        private IApi _api;
        public SyncViewModel(IApi api, INavigation navigation) : base(navigation)
        {

            _api = new ApiService(AppConstants.SettingsServiceUrl);
            _ = GetItems();

        }

        [ObservableProperty]
        private string completeHouseholds;
        [ObservableProperty]
        private string completeUpdates;
        [ObservableProperty]
        private string currentStatus;
        public async Task GetItems()
        {
            CompleteHouseholds = $"Upload Households ({await App.db.Table<Household>().CountAsync(i => i.IsComplete)})";
            CompleteUpdates = $"Upload Updates ({await App.db.Table<Update>().CountAsync(i => i.IsComplete)})";
        }

        [RelayCommand]
        private async Task Download()
        {

            if (IsBusy)
                return;
            IsBusy = true;
            var lm = new
            {
                UserName = App.User.Email,
                CountyId = App.User.CountyId
            };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");
            try
            {
                CurrentStatus = "Downloading System Codes";

                var response = await _api.DownloadSettings(content, $"Bearer {App.User.Token}");

                if (response.returnDetails != null)
                {
                    CurrentStatus = "Processing System Codes";
                    await App.Database._database.DeleteAllAsync<SystemCodeDetail>();
                    response.returnDetails.ForEach(i => i.IdComboCode = $"{i.Id}{i.ComboCode}");

                    foreach (var item in response.returnDetails.OrderBy(i => i.Id))
                    {
                        App.Database.AddOrUpdate(item);
                    }
                    await Toast.SendToastAsync("System Codes downloaded successfully");
                }
                else
                {

                    await Toast.SendToastAsync(response.detail);
                }
                Thread.Sleep(1000);
                CurrentStatus = "Downloading Constituencies";
                content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");
                var constituenciesResponse = await _api.GetConstituencies(content, $"Bearer {App.User.Token}");

                if (constituenciesResponse.returnDetails != null)
                {
                    CurrentStatus = "Processing Constituencies";
                    await App.Database._database.DeleteAllAsync<Constituency>();
                    foreach (var item in constituenciesResponse.returnDetails)
                    {
                        App.Database.AddOrUpdate(item);
                    }
                    await Toast.SendToastAsync("Constituencies downloaded successfully");
                }
                else
                {
                    await Toast.SendToastAsync(response.detail);
                }
                Thread.Sleep(1000);
                CurrentStatus = "Downloading Sub Locations";
                content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");
                var subLocationResponse = await _api.GetSubLocations(content, $"Bearer {App.User.Token}");

                if (subLocationResponse.returnDetails != null)
                {
                    CurrentStatus = "Processing Sub Locations";
                    await App.Database._database.DeleteAllAsync<SubLocation>();
                    foreach (var item in subLocationResponse.returnDetails)
                    {
                        App.Database.AddOrUpdate(item);
                    }
                    await Toast.SendToastAsync("Sub Locations downloaded successfully");
                }
                else
                {
                    await Toast.SendToastAsync(response.detail);
                }
                Thread.Sleep(1000);
                CurrentStatus = "Downloading Villages";
                content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");
                var villagesResponse = await _api.GetVillages(content, $"Bearer {App.User.Token}");

                if (villagesResponse.returnDetails != null)
                {
                    CurrentStatus = "Processing Villages";
                    await App.db.DeleteAllAsync<Village>();
                    await App.db.InsertAllAsync(villagesResponse.returnDetails);
                    await Toast.SendToastAsync("Villages downloaded successfully");
                }
                else
                {
                    await Toast.SendToastAsync(response.detail);
                }

                await Toast.SendToastAsync("Latest settings from the server downloaded successfully");
            }
            catch (ApiException ex)
            {
                if (ex.Message.Contains("401"))
                {
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                    await Toast.SendToastAsync("Session timeout, kindly login again");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");
                }


            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");

            }

            IsBusy = false;

        }

        [RelayCommand]
        private async Task UploadHouseholds()
        {
            var householdIds = await App.db.Table<Household>().Where(i => i.IsComplete).ToListAsync();
            total = householdIds.Count();
            if (total == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Sorry", "No complete households found.", "OK");
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
