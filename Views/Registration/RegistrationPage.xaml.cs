using HSNP.Mobile.Views;
using HSNP.Models;
using HSNP.Services;
using Maui.DataGrid;
using static Android.Telecom.Call;

namespace HSNP.Mobile.Views;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage()
	{
		InitializeComponent();
       // BindingContext = new RegistrationViewModel();
    }
    protected override void OnAppearing()
    {
        try {
            BindingContext = new RegistrationViewModel(false);
        }
        catch (Exception ex) {
             Application.Current.MainPage.DisplayAlert("Sorry!", ex.Message, "Ok");
        }
        
    }
    private async void AddNew_OnClicked(object sender, EventArgs e)
    {
        App.HouseholdId = null;
        App.MemberId = null;
        Shell.Current.GoToAsync($"/{nameof(AddNewHouseholdPage)}");
        // await Shell.Current.GoToAsync("//AddNewHouseholdPage");
    }
    
    async void DataGrid_ItemSelected(Object sender,SelectionChangedEventArgs e)
    {
        try {
            if (e.CurrentSelection.Count() > 0)
            {
                HouseholdMember selected = (HouseholdMember)e.CurrentSelection.FirstOrDefault();
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
        }catch(Exception ex) {
            var error = ex.Message;
        }
       
       
    }
    private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is CollectionView collectionView)
        {
            // Get the selected item (if any) from the event arguments
            if (e.CurrentSelection.FirstOrDefault() is Household selectedItem)
            {
                App.HouseholdId = selectedItem.HouseholdId;
                //  Shell.Current.GoToAsync("//Household");
                 Shell.Current.GoToAsync($"/{nameof(AddNewHouseholdPage)}?HouseholdId={selectedItem.HouseholdId}");
               // Shell.Current.GoToAsync($"/{nameof(MembersPage)}?HouseholdId={selectedItem.HouseholdId}");
            }

            // Clear the selection to allow re-selection of the same item
           // collectionView.SelectedItem = null;
        }
    }

}
