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

    [ObservableProperty]
    private string membersCount;

    public MembersViewModel() 
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

        var current = HouseholdMembers.Count();
        var total = (await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == HouseholdId)).HouseholdMembers;
        MembersCount = $"{current} / {total}";
    }
}
