using HSNP.Services;

namespace HSNP.Mobile.Views;

public partial class UpdateHouseholdPage : ContentPage
{
	public UpdateHouseholdPage()
	{
		InitializeComponent();
        BindingContext = new UpdateHouseholdViewModel(ApiService.Instance, Navigation);
    }

	async void Next_Clicked(System.Object sender, System.EventArgs e)
	{
		//await Navigation.PushAsync(new DwellingAddEditPage());

	}
}
