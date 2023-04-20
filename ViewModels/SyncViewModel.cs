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

        private readonly IApi _api;
        public SyncViewModel(IApi api, INavigation navigation) : base(navigation)
        {
           
             _api = new ApiService(AppConstants.SettingsServiceUrl);
        }
       
        [ObservableProperty]
        private string currentStatus;

      
        [RelayCommand]
        private async Task Download()
        {
            
               if (IsBusy)
                   return;
                IsBusy = true;
                var lm = new 
                {
                    UserName = App.User.Email,
                    CountyId=App.User.CountyId
                };
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");
            try
            {
                CurrentStatus = "Downloading Combo Codes";

                var response = await _api.DownloadSettings(content, $"Bearer {App.User.Token}");

                if (response.returnDetails != null)
                {
                    CurrentStatus = "Processing Combo Codes";
                    await App.Database._database.DeleteAllAsync<SystemCodeDetail>();
                    response.returnDetails.ForEach(i => i.IdComboCode = $"{i.Id}{i.ComboCode}");
                    
                    foreach (var item in response.returnDetails.OrderBy(i=>i.Id))
                    {
                        App.Database.AddOrUpdate(item);
                    }
                    await Toast.SendToastAsync("Combo Codes downloaded successfully");
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
                    await App.Database._database.DeleteAllAsync<Village>();
                    foreach (var item in villagesResponse.returnDetails)
                    {
                        App.Database.AddOrUpdate(item);
                    }
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
