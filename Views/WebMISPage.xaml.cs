namespace HSNP.Mobile.Views;

public partial class WebMISPage : ContentPage
{
	public WebMISPage()
	{
		InitializeComponent();
	}
	async void LaunchWebClicked(object sender, EventArgs args)
	{
		var url = App.User.WebUrl;
		if (string.IsNullOrEmpty(url))
			url = "https://google.co.ke";
;        await Launcher.OpenAsync(new Uri(url));
    }
}