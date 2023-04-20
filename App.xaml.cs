using HSNP.Database;
using HSNP.Models;
using HSNP.Services;
using SQLite;

namespace HSNP.Mobile;

public partial class App : Application
{
    public static User User;
    public static string HouseholdId;
    public static string MemberId;
    public static SQLiteAsyncConnection db;
    public static DataStore Database { get; private set; }
    public App()
	{
		InitializeComponent();
        Database = new DataStore();
        db = Database._database;
        DependencyService.Register<MockDataStore>();
        User = new User();
        MainPage = new AppShell();
	}
    protected override async void OnStart()
    {
        await Task.Delay(1000);
        var status = await CheckStatusAsync();
        if (status.Equals("home"))
            MainPage = new AppShell();
        else if (status.Equals("login"))
            MainPage = new NavigationPage(new LoginPage());
      
    }
    public  async Task<string> CheckStatusAsync()
    {
        var str = "";
        User = await db.Table<User>().FirstOrDefaultAsync();
        if (User == null || User.IsLoggedIn == false)
            str = "login";
        else
        {
            str = "home";
        }
        return str;

    }
}
