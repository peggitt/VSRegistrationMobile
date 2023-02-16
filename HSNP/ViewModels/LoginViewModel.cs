using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HSNP.Interface;
using HSNP.Models;
using HSNP.Views;
using Refit;
using Xamarin.Forms;

namespace HSNP.ViewModels
{
    public partial class LoginViewModel : BaseVm
    {

        private readonly IApi _api;
        public LoginViewModel(IApi api, INavigation navigation) : base(navigation)
        {
            _api = api;

        }
        [ObservableProperty]
        private bool isLoggedIn;
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
                Toast.SendToast(errorMsg);
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

                        var user = await App.Database.GetDefaultUser() ?? new User { Email = Email };
                        IsLoggedIn = user.IsLoggedIn = true;
                        user.hsnp_key = login.hsnp_key;
                        await App.Database._database.DeleteAllAsync<User>();
                        App.Database.AddOrUpdate(user);
                        App.User = user;
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(login.Error))
                            Toast.SendToast(login.Error);
                        Toast.SendToast("Email or Password incorrect");
                    }
                }
                catch (ApiException ex)
                {
                    //await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");
                    Toast.SendToast("Email or Password incorrect");
                }
                catch (Exception ex)
                {
                    // await Application.Current.MainPage.DisplayAlert("Exception", ex.ToString(), "OK");
                    Toast.SendToast("Email or Password incorrect");
                }

                IsBusy = false;
            }
        }


    }
}
