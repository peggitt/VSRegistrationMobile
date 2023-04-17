using HSNP.Mobile.Views.Registration;
using HSNP.Models;
using HSNP.Services;
using static Java.Util.Jar.Attributes;

namespace HSNP.Mobile.Views;

[QueryProperty(nameof(HouseholdId), nameof(HouseholdId))]
public partial class MembersPage : ContentPage
{
    string hhId;
    public string HouseholdId
    {
        set
        {
            hhId = value;
        }
    }
    public MembersPage()
	{
		InitializeComponent();
	}
    
    protected override void OnAppearing()
    {
        BindingContext = new MembersViewModel(hhId);
    }
    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MembersAddPage());
    }
    async void DataGrid_ItemSelected(Object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count() == 0) { return; }
        HouseholdMember selected = (HouseholdMember)e.CurrentSelection.FirstOrDefault();

        await Shell.Current.GoToAsync($"{nameof(MembersAddPage)}?HouseholdId={selected.HouseholdId}&MemberId={selected.Id}");
        //  ((DataGrid)sender).SelectedItem = null;
    }
}
