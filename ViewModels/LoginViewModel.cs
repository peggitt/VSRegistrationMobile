using System.Text;
using HSNP.Constants;
using HSNP.Interfaces;
using HSNP.Mobile;
using HSNP.Models;
using HSNP.Services;
using Refit;

namespace HSNP.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {

        private readonly IApi _api;
        public LoginViewModel(IApi api, INavigation navigation) : base(navigation)
        {
           //  Email= "hsnpdata@hsnp.or.ke";
           //  Password = "H6lBiPd8";

          //  Email = "tonyouh@gmail.com";
          //  Password = "Kenya!@2024";

            //  IsBusy = true;
            _api = new ApiService(AppConstants.SecurityApiAddress);

          
            AppVersion = $"App Version {AppInfo.Current.VersionString}" ;

        }
        [ObservableProperty]
        private string appVersion;
        [ObservableProperty]
        private string email;

        [ObservableProperty]
        private string password;
        [RelayCommand]
        private async Task Login()
        {
            var errorMsg = "";
            if (string.IsNullOrEmpty(Email))
            {
                errorMsg += "Email is Required.\n";
            }
            if (string.IsNullOrEmpty(Password))
            {
                errorMsg += "Password is Required.\n";
            }
            if (!string.IsNullOrEmpty(errorMsg))
            {
               await Toast.SendToastAsync(errorMsg);
            }
            else
            {
               if (IsBusy)
                   return;
                IsBusy = true;
                var lm = new LoginVm
                {
                    AccessType = 1,
                    UserEmail = Email,
                    Password = Password
                };
                var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(lm), Encoding.UTF8, "application/json");
                try
                {

                    /*
                    Uri baseUrl = new Uri("http://197.254.7.126:7200/userlogin");
                    var apiEndpoint = baseUrl;
                    var jsonBody = new LoginVm
                    {
                        AccessType = 1,
                        UserEmail = "admin@hsnp.or.ke",
                        Password = "kubaffmita"
                    };
                    using (var httpClient = new HttpClient())
                    {

                        var responseData = await httpClient.PostAsync(apiEndpoint, content);
                        responseData.EnsureSuccessStatusCode(); // throws an exception if the status code is not in the 200 range
                        var responseContent = await responseData.Content.ReadAsStringAsync();
                        // do something with the response content
                    }
                    */
                    var login = await _api.Login(content);
                   
                    if (login.hsnp_key != null)
                    {

                        var user = await App.Database.GetDefaultUser() ?? new User {Id="hsnpuser", Email = Email };
                        IsLoggedIn = user.IsLoggedIn = true;
                        user.CountyId = login.CountyId;
                        user.WebUrl = login.WebUrl;
                        user.Token = login.hsnp_key;
                        user.Email = Email;
                        await App.Database._database.DeleteAllAsync<User>();
                        App.Database.AddOrUpdate(user);
                        App.User = user;
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(login.Error))
                            await Toast.SendToastAsync(login.Error);
                       await Toast.SendToastAsync("Email or Password incorrect");
                    }
                }
                catch (ApiException ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry!", ex.Content.ToString(), "Ok");
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Sorry!", ex.Message, "Ok");
                }

                IsBusy = false;
            }
        }


    }
}
