﻿using Android.Icu.Number;
using HSNP.Interfaces;
using HSNP.Models;
using MvvmHelpers;
namespace HSNP.Mobile.ViewModels;

public partial class RegistrationViewModel : BaseViewModel
{

    private readonly IApi _api;
    public RegistrationViewModel(INavigation navigation, IApi api) : base(navigation)
    {
        _api = api;
        GetItems();
       
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    [ObservableProperty]
    private int heightRequest;

    private ObservableRangeCollection<HouseholdMember> _sales;
    public  ObservableRangeCollection<HouseholdMember> Sales
    {
        get => _sales;
        set { _sales = value; OnPropertyChanged(); }
    }

    public async void GetItems()
    {
        var test= await App.db.Table<HouseholdMember>().ToListAsync();
        HouseholdMembers = await App.db.Table<HouseholdMember>().OrderByDescending(i=>i.CreatedOn).Where(i => i.IsApplicant == true && i.IsComplete==false).ToListAsync();
        HeightRequest = 100 + HouseholdMembers.Count() * 50;
        //Sales = new ObservableRangeCollection<HouseholdMember>();
        //  Sales.AddRange(HouseholdMembers);
    }
}
