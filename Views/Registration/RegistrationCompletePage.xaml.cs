using HSNP.Models;
using HSNP.Services;

namespace HSNP.Mobile.Views;

public partial class RegistrationCompletePage : ContentPage
{
	public RegistrationCompletePage()
	{
        InitializeComponent();
      //  BindingContext = new RegistrationViewModel(true);
    }
    protected override void OnAppearing()
    {
        BindingContext = new RegistrationViewModel(true);
    }
    private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            // Get the selected item (if any) from the event arguments
            if (e.CurrentSelection.FirstOrDefault() is Household selectedItem)
            {
                App.HouseholdId = selectedItem.HouseholdId;
                // Shell.Current.GoToAsync("//Household");
                Shell.Current.GoToAsync($"/{nameof(AddNewHouseholdPage)}?HouseholdId={selectedItem.HouseholdId}");
            }

            // Clear the selection to allow re-selection of the same item
            // collectionView.SelectedItem = null;
        }
    }


    async void DataGrid_ItemSelected(Object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count() > 0)
        {
            var selected = (Household)e.CurrentSelection.FirstOrDefault();
            App.HouseholdId = selected.HouseholdId;
            await Shell.Current.GoToAsync("//Household");

            //   var test = ((AppShell)App.Current.MainPage).Items.First().Items.First(i=>i.Route== "Household");
            //   Shell.Current.CurrentItem = test;

            //   ((AppShell)App.Current.MainPage).SwitchtoTab(2);
            //  await Shell.Current.GoToAsync(nameof(HouseholdPage));
            //  await Shell.Current.GoToAsync($"{nameof(DwellingAddEditPage)}?HouseholdId={selected.HouseholdId}");
            //  await Shell.Current.GoToAsync($"registrationdetails?HouseholdId={selected.HouseholdId}");
            //  ((DataGrid)sender).SelectedItem = null;
        }

    }


}