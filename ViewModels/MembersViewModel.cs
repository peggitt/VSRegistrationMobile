using HSNP.Interfaces;
using HSNP.Models;

namespace HSNP.Mobile.ViewModels;


public partial class MembersViewModel : BaseViewModel
{

    private readonly IApi _api;
    [ObservableProperty]
    private int heightRequest;
    [ObservableProperty]
    private string householdId;


    public MembersViewModel(string id) 
    {
        HouseholdId = id;
        GetItems();
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    public async void GetItems()
    {
        HouseholdMembers = await App.db.Table<HouseholdMember>().Where(i => i.HouseholdId == HouseholdId).ToListAsync();
        HeightRequest = 100 + HouseholdMembers.Count() * 50;
    }
}
