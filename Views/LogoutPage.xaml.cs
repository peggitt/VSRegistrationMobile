using System.Net.Http.Headers;
using HSNP.Models;
using Xamarin.KotlinX.Coroutines.Channels;

namespace HSNP.Mobile.Views;

public partial class LogoutPage : ContentPage
{
    public string AppVersion { get; set; }

    public LogoutPage()
	{
		InitializeComponent();
        AppVersion = $"App Version {AppInfo.Current.VersionString}";
        BindingContext = this;
    }
    async void LogoutClicked(object sender, EventArgs args)
    {
        try
        {
            var user = App.User;
            user.IsLoggedIn = false;
            await App.db.UpdateAsync(user);
            await Navigation.PopToRootAsync();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Sorry!", ex.ToString(), "Ok");
        }
       
    }
}
