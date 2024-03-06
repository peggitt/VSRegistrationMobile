using Device = Microsoft.Maui.Controls.Device;

namespace HSNP.Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        

        Routing.RegisterRoute(nameof(AddNewHouseholdPage), typeof(AddNewHouseholdPage));
        Routing.RegisterRoute(nameof(MembersPage), typeof(MembersPage));
        Routing.RegisterRoute(nameof(MembersAddPage), typeof(MembersAddPage));
        Routing.RegisterRoute(nameof(DwellingAddEditPage), typeof(DwellingAddEditPage));
        Routing.RegisterRoute(nameof(HouseholdPage), typeof(HouseholdPage));
        Routing.RegisterRoute(nameof(SyncPage), typeof(SyncPage));

        // Updates Page
        Routing.RegisterRoute(nameof(UpdateHouseholdPage), typeof(UpdateHouseholdPage));
        Routing.RegisterRoute(nameof(UpdatesMembersPage), typeof(UpdatesMembersPage));
        Routing.RegisterRoute(nameof(UpdateMemberPage), typeof(UpdateMemberPage));
        Routing.RegisterRoute(nameof(DwellingUpdatePage), typeof(DwellingUpdatePage));
        
    }
    public void SwitchtoTab(int tabIndex)
    {
        Device.BeginInvokeOnMainThread(() =>
        {
            switch (tabIndex)
            {
               // case 0: shelltabbar.CurrentItem = shelltab_0; break;
              //  case 1: shelltabbar.CurrentItem = shelltab_1; break;
              //  case 2: shelltabbar.CurrentItem = shelltab_2; break;
              
            };
        });
    }
}
