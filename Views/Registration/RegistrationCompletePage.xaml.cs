﻿using HSNP.Models;
using HSNP.Services;

namespace HSNP.Mobile.Views;

public partial class RegistrationCompletePage : ContentPage
{
	public RegistrationCompletePage()
	{
        InitializeComponent();
        BindingContext = new RegistrationViewModel(true);
    }

    private void AddNew_OnClicked(object sender, EventArgs e)
    {
        App.HouseholdId = null;
        App.MemberId = null;
        Navigation.PushAsync(new AddNewHouseholdPage());
    }

    async void DataGrid_ItemSelected(Object sender, SelectionChangedEventArgs e)
    {
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

    }


}