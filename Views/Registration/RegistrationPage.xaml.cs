using HSNP.Mobile.Views.Registration;
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
        BindingContext = new RegistrationViewModel(Navigation, ApiService.Instance);
    }
    protected override void OnAppearing()
    {
       
    }
    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new AddNewHouseholdPage());
    }
    
    async void DataGrid_ItemSelected(Object sender,SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count() > 0)
        {
            HouseholdMember selected = (HouseholdMember)e.CurrentSelection.FirstOrDefault();

           // await Shell.Current.GoToAsync($"{nameof(MembersPage)}?HouseholdId={selected.HouseholdId}");
            await Shell.Current.GoToAsync($"{nameof(DwellingAddEditPage)}?HouseholdId={selected.HouseholdId}");
          //  await Shell.Current.GoToAsync($"registrationdetails?HouseholdId={selected.HouseholdId}");

            
          //  ((DataGrid)sender).SelectedItem = null;
        }
    }

    
}
