using HSNP.Mobile.Views.Registration;

namespace HSNP.Mobile.Views;

public partial class MembersPage : ContentPage
{
	public MembersPage()
	{
		InitializeComponent();
	}
    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MembersAddPage());
    }
}
