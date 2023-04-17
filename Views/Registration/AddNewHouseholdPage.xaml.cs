using HSNP.Services;

namespace HSNP.Mobile.Views.Registration;

public partial class AddNewHouseholdPage : ContentPage
{
	public AddNewHouseholdPage()
	{
		InitializeComponent();
        BindingContext = new AddNewHouseholdViewModel(ApiService.Instance, Navigation);
    }

	async void Next_Clicked(System.Object sender, System.EventArgs e)
	{
		//await Navigation.PushAsync(new DwellingAddEditPage());

	}
}
