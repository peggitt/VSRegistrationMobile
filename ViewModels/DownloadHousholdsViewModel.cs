using System.Text;
using HSNP.Interfaces;
using HSNP.Mobile;
using HSNP.Models;
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
                    UserName = App.User.Email,
                    SublocationId = SubLocation.Id
                };
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");
                try
                {
                    CurrentStatus = "Downloading Households";
                    int count = 0;
                    /*
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
                    */

                    await Application.Current.MainPage.DisplayAlert("Download Complete", $"{count} households downloaded","OK");
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

        }

    }
}
