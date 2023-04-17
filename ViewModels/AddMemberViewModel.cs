using HSNP.Interfaces;
using HSNP.Models;

namespace HSNP.Mobile.ViewModels;


public partial class AddMemberViewModel : BaseViewModel
{

    private readonly IApi _api;
    [ObservableProperty]
    private int heightRequest;

    [ObservableProperty]
    private string householdId;

    [ObservableProperty]
    private string memberId;

    [ObservableProperty]
    private HouseholdMember member;

    public AddMemberViewModel(INavigation navigation, IApi api,string id,string memberId) : base(navigation)
    {
        _api = api;
        HouseholdId = id;
        MemberId = memberId;
        GetItems();
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    public async void GetItems()
    {
        Member = await App.db.Table<HouseholdMember>().FirstOrDefaultAsync(i => i.Id == MemberId);
    }

    [RelayCommand]
    async void Save()
    {
        string errors = "";
        var test = Member;
    }
}
