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

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        if (TxtPassword.IsPassword == true)
            TxtPassword.IsPassword = false;
        else
            TxtPassword.IsPassword = true;
    }
}
