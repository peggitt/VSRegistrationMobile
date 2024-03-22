using System.Text;
using HSNP.Interfaces;
using HSNP.Mobile;
using HSNP.Models;
using Mopups.Services;
using Refit;

namespace HSNP.ViewModels
{
    public partial class DownloadHousholdsViewModel : BaseViewModel
    {
      
        private IApi _api;
        public DownloadHousholdsViewModel(IApi api, INavigation navigation) : base(navigation)
        {

            _api = api;
            _ = GetItems();

        }
        [ObservableProperty]
        private List<Constituency> constituencies;
        [ObservableProperty]
        private Constituency constituency;
        partial void OnConstituencyChanged(Constituency value)
        {
            if (value != null)
                GetSubLocations(value.Id);
        }
        [ObservableProperty]
        private SubLocation subLocation;

        [ObservableProperty]
        private List<SubLocation> subLocations;
        [ObservableProperty]
        private List<Village> villages;
        [ObservableProperty]
        private string currentStatus;
        
        public async Task GetItems()
        {
            Constituencies = await App.db.Table<Constituency>().ToListAsync();
        }
        public async void GetSubLocations(int id)
        {
            SubLocations = await App.db.Table<SubLocation>().Where(i => i.ConstituencyId == id).ToListAsync();
        }
        [RelayCommand]
        private async Task Download()
        {
            if (SubLocation == null)
            {
                await Toast.SendToastAsync("Select Sub Location");
            }
            else
            {

                if (IsBusy)
                    return;
                IsBusy = true;
                var lm = new
                {
                    UserEmail = App.User.Email,
                    SublocationId = SubLocation.Id
                };

                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");



                //Uri baseUrl = new Uri("http://app.hsnpmis.or.ke:7300/householdviewdownload");
                //   var apiEndpoint = baseUrl;
                 
                //   using (var httpClient = new HttpClient())
                //   {

                //       var responseData = await httpClient.PostAsync(apiEndpoint, content);
                //       responseData.EnsureSuccessStatusCode(); // throws an exception if the status code is not in the 200 range
                //       var responseContent = await responseData.Content.ReadAsStringAsync();
                //       // do something with the response content
                //   }
                   

               
                try
                {
                    CurrentStatus = "Downloading Households";
                   
                
                    var response = await _api.DownloadHouseholds(content, $"Bearer {App.User.Token}");

                    response.returnDetails.ForEach(i => i.MarkForDownload = true);
                    response.returnDetails.ForEach(i => i.SubLocationId = SubLocation.Id);
                    
                   
                 
                    await App.Database._database.Table<Household>().Where(i=>i.SubLocationId == SubLocation.Id && i.MarkForDownload==true).DeleteAsync();
                    await App.Database._database.Table<HouseholdMember>().Where(i => i.SubLocationId == SubLocation.Id).DeleteAsync();
                    await App.Database._database.Table<HouseholdCharacteristic>().Where(i => i.SubLocationId == SubLocation.Id).DeleteAsync();

                    await App.db.InsertAllAsync(response.returnDetails);


                    var detailsParms = new
                    {
                        UserName = App.User.Email,
                        SublocationId = SubLocation.Id.ToString()
                    };

                    var detailsContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(detailsParms), Encoding.UTF8, "application/json");

                    CurrentStatus = "Downloading Households Details";
                    var detailsResponse = await _api.DownloadHouseholdsDetails(detailsContent, $"Bearer {App.User.Token}");
                    detailsResponse.returnDetails.ForEach(i => i.SubLocationId = SubLocation.Id);
                    await App.db.InsertAllAsync(detailsResponse.returnDetails);


                    CurrentStatus = "Downloading Members";
                    var parms = new
                    {
                        UserName = App.User.Email,
                        SubLocationId = SubLocation.Id
                    };

                    var memberContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(parms), Encoding.UTF8, "application/json");

                    var mResponse = await _api.DownloadMembers(memberContent, $"Bearer {App.User.Token}");
                    try
                    {
                       
                        mResponse.returnDetails.ForEach(i => i.SubLocationId = SubLocation.Id);
                        mResponse.returnDetails.ForEach(i => i.Id = i.HHMemberRosterId);                
                        mResponse.returnDetails.ForEach(i => i.IsComplete = true);
                      

                        await App.db.InsertAllAsync(mResponse.returnDetails); 
                    }catch(Exception ex)
                    {
                        var test = ex.Message;
                    }
                    //if (response.returnDetails != null)
                    //{
                    //    CurrentStatus = "Processing System Codes";
                    //    await App.Database._database.DeleteAllAsync<SystemCodeDetail>();
                    //    response.returnDetails.ForEach(i => i.IdComboCode = $"{i.Id}{i.ComboCode}");

                    //    foreach (var item in response.returnDetails.OrderBy(i => i.Id))
                    //    {
                    //        App.Database.AddOrUpdate(item);
                    //    }
                    //    await Toast.SendToastAsync("System Codes downloaded successfully");
                    //}
                    //else
                    //{

                    //    await Toast.SendToastAsync(response.detail);
                    //}


                    await MopupService.Instance.PopAsync();
                    await Application.Current.MainPage.DisplayAlert("Download Complete", $"{response.returnDetails.Count()} households downloaded","OK");
                     await Shell.Current.GoToAsync($"/{nameof(SyncPage)}");
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
                        await Application.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");
                    }
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Exception", ex.Message, "OK");

                }
                IsBusy = false;
            }

        }

    }
}
