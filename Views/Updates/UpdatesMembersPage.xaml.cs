﻿using CommunityToolkit.Maui.Alerts;
using HSNP.Mobile.Views;
using HSNP.Models;
using HSNP.Services;
using static Java.Util.Jar.Attributes;

namespace HSNP.Mobile.Views;

[QueryProperty(nameof(HouseholdId), nameof(HouseholdId))]
public partial class UpdatesMembersPage : ContentPage
{
    string hhId;
    UpdatesMembersViewModel viewModel;
    public string HouseholdId
    {
        set
        {
            hhId = value;
        }
    }
    public UpdatesMembersPage()
	{
		InitializeComponent();
	}
    
    protected override void OnAppearing()
    {
         viewModel= new UpdatesMembersViewModel();
        BindingContext = viewModel;
    }
    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        if (viewModel.IsComplete)
        {
            
             Application.Current.MainPage.DisplayAlert("Complete", $"All household members already added.", "OK");
        }
        else
        {
            App.MemberId = null;
            Navigation.PushAsync(new MembersAddPage());
        }
    }
    async void DataGrid_ItemSelected(Object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.Count() == 0) { return; }
        HouseholdMember selected = (HouseholdMember)e.CurrentSelection.FirstOrDefault();
        App.HouseholdId = selected.HouseholdId;
        App.MemberId = selected.Id;
        await  Navigation.PushAsync(new UpdateMemberPage());
     //   await Shell.Current.GoToAsync(nameof(MembersAddPage));
        //  ((DataGrid)sender).SelectedItem = null;
    }

    
}
