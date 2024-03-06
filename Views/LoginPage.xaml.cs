using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
        this.BindingContext = new LoginViewModel(ApiService.Instance, Navigation);

    }
    void ForgotPinTapped(object sender, EventArgs args)
    {
      //  Navigation.PushAsync(new ForgotPinPage());
    }
}
