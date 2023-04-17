namespace HSNP.Mobile;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        
        Routing.RegisterRoute(nameof(MembersPage), typeof(MembersPage));
        Routing.RegisterRoute(nameof(MembersAddPage), typeof(MembersAddPage));
        Routing.RegisterRoute(nameof(DwellingAddEditPage), typeof(DwellingAddEditPage));
        


    }
}
