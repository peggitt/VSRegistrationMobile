using HSNP.Interfaces;
using HSNP.Models;

namespace HSNP.Mobile.ViewModels;

public partial class RegistrationViewModel : BaseViewModel
{

    private readonly IApi _api;
    public RegistrationViewModel(IApi api, INavigation navigation) : base(navigation)
    {
        _api = api;
        GetItems();
       
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    public async void GetItems()
    {

        HouseholdMembers = await App.db.Table<HouseholdMember>().Where(i => i.ComboCode == "RuralUrban").ToListAsync();
       
    }
}
