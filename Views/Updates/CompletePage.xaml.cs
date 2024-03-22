using HSNP.Models;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views.Updates;

public partial class CompletePage : ContentPage
{
	public CompletePage()
	{
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        try
        {
            BindingContext = new UpdatesViewModel(true);
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Sorry!", ex.Message, "Ok");
        }

    }
    async void DataGrid_ItemSelected(Object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count() > 0)
        {
            HouseholdMember selected = (HouseholdMember)e.CurrentSelection.FirstOrDefault();
            App.HouseholdId = selected.HouseholdId;
           // await Shell.Current.GoToAsync("//Household");
            await Shell.Current.GoToAsync($"/{nameof(UpdateHouseholdPage)}?HouseholdId={selected.HouseholdId}");
        }

    }
}
