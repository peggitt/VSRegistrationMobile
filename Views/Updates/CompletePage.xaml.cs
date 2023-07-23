using HSNP.Models;

namespace HSNP.Mobile.Views.Updates;

public partial class CompletePage : ContentPage
{
	public CompletePage()
	{
		InitializeComponent();
	}
    async void DataGrid_ItemSelected(Object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count() > 0)
        {
            HouseholdMember selected = (HouseholdMember)e.CurrentSelection.FirstOrDefault();
            App.HouseholdId = selected.HouseholdId;
            await Shell.Current.GoToAsync("//Household");
           
        }

    }
}
