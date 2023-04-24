using CommunityToolkit.Maui.Alerts;
using HSNP.Mobile.Views;
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
        BindingContext = new MembersViewModel();
    }
    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        App.MemberId = null;
        Navigation.PushAsync(new MembersAddPage());
    }
    async void DataGrid_ItemSelected(Object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count() == 0) { return; }
        HouseholdMember selected = (HouseholdMember)e.CurrentSelection.FirstOrDefault();
        App.HouseholdId = selected.HouseholdId;
        App.MemberId = selected.Id;
        await  Navigation.PushAsync(new MembersAddPage());
     //   await Shell.Current.GoToAsync(nameof(MembersAddPage));
        //  ((DataGrid)sender).SelectedItem = null;
    }

    
}
