using HSNP.Models;
using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views.Updates;

public partial class UpdatesPage : ContentPage
{
	public UpdatesPage()
	{
		InitializeComponent();
    }
    protected override void OnAppearing()
    {
        try
        {
            BindingContext = new UpdatesViewModel(false);
        }
        catch (Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Sorry!", ex.ToString(), "Ok");
        }

    }
    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        App.HouseholdId = null;
        App.MemberId = null;
        Navigation.PushAsync(new SearchPage());
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
