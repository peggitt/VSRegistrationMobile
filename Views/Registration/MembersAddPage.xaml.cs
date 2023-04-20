using HSNP.Models;
using HSNP.Services;

namespace HSNP.Mobile.Views;
[QueryProperty(nameof(HouseholdId), nameof(HouseholdId))]
[QueryProperty(nameof(MemberId), nameof(MemberId))]
public partial class MembersAddPage : ContentPage
{
    string hhId;
    public string HouseholdId
    {
        set
        {
            hhId = value;
        }
    }
    string memberId;
    public string MemberId
    {
        set
        {
            memberId = value;
        }
    }
   
    public MembersAddPage()
	{
		InitializeComponent();
        BindingContext = new AddMemberViewModel(Navigation, ApiService.Instance, hhId, memberId);
    }
  
   
}
