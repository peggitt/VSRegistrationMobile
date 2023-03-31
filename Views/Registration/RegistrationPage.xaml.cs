using HSNP.Mobile.Views.Registration;

namespace HSNP.Mobile.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
	}
    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddNewHouseholdPage());
    }
}
