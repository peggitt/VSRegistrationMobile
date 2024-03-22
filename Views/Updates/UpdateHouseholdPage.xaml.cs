using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class UpdateHouseholdPage : ContentPage
{
    public UpdateHouseholdPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        BindingContext = new UpdateHouseholdViewModel(ApiService.Instance, Navigation);
    }

    async void Next_Clicked(System.Object sender, System.EventArgs e)
	{
		//await Navigation.PushAsync(new DwellingAddEditPage());

	}
}
