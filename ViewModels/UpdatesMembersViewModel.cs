﻿using HSNP.Interfaces;
using HSNP.Models;

namespace HSNP.Mobile.ViewModels;


public partial class UpdatesMembersViewModel : BaseViewModel
{

    private readonly IApi _api;
    [ObservableProperty]
    private int heightRequest;
    [ObservableProperty]
    private string householdId;

    [ObservableProperty]
    private string membersCount;

    [ObservableProperty]
    private bool isComplete;

    [ObservableProperty]
    private bool hideCompleteButton;

    public UpdatesMembersViewModel() 
    {
        HouseholdId = App.HouseholdId;
        GetItems();
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    public async void GetItems()
    {
        HouseholdMembers = await App.db.Table<HouseholdMember>().Where(i => i.HouseholdId == HouseholdId).ToListAsync();
        HeightRequest = 100 + HouseholdMembers.Count() * 50;

        var current = HouseholdMembers.Where(i=>i.IsComplete).Count();
        var total = (await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == HouseholdId)).HouseholdMembers;
        IsComplete = current == total;
        MembersCount = $"{current} / {total}";

        HideCompleteButton= (await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == App.HouseholdId)).IsComplete;
    }
    [RelayCommand]
    async void Save()
    {
        var current = HouseholdMembers.Where(i => i.IsComplete).Count();
        var total = (await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == HouseholdId)).HouseholdMembers;
        if (current == total) {
            var household = await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == App.HouseholdId);
            household.IsComplete = true;
            await App.db.UpdateAsync(household);
            HideCompleteButton = true;
            await Toast.SendToast("Household marked as ready for upload");
        }
        else {

            await Application.Current.MainPage.DisplayAlert("Please Add all Members", $"Added members: {current} / {total}", "OK");
        }
        

    }
}
