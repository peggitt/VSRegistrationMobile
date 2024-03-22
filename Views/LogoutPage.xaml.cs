namespace HSNP.Mobile.Views;

public partial class LogoutPage : ContentPage
{
	public LogoutPage()
	{
		InitializeComponent();
	}
    async void LogoutClicked(object sender, EventArgs args)
    {
        try
        {
            var user = await App.Database.GetDefaultUser();
            user.IsLoggedIn = false;
            await App.db.UpdateAsync(user);
            await Navigation.PopToRootAsync();
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Sorry!", ex.Message, "Ok");
        }

    }
}
