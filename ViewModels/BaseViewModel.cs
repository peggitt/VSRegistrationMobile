using CommunityToolkit.Maui.Core;
using HSNP.Mobile.Droid;
using HSNP.Mobile.Interfaces;
using HSNP.Services;

namespace HSNP.Mobile.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    protected readonly IToaster Toast;
    public BaseViewModel(INavigation navigation = null)
    {
        Toast = new Toaster();
        _permissionService = new PermissionService();

        // we handle the connectivity services
        _httpClient = new HttpClient();
        // Handle connectivity
        Connectivity.ConnectivityChanged += ConnectivityOnConnectivityChanged;
        //  IsNotConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;

        //handle logged  in status
        //   IsNotLoggedIn = false; // Settings.Current.IsNotLoggedIn;
        Navigation = navigation;
    }


    ~BaseViewModel()
    {
        Connectivity.ConnectivityChanged -= ConnectivityOnConnectivityChanged;
    }


    [ObservableProperty]
    private bool isLoggedIn;

    [ObservableProperty]
    public string searchBarText;
    [ObservableProperty]
    private bool isRefreshing = false;

    [ObservableProperty]
    private bool isNotConnected;

    [ObservableProperty]
    private bool isNotLoggedIn;

    [ObservableProperty]
    private bool isBusy;

    [ObservableProperty]
    private bool isOwner;

    [ObservableProperty]
    private bool isIdle;

    [ObservableProperty]
    private string title;

    public PermissionService _permissionService;
    public readonly HttpClient _httpClient;
    // Navigation property inherited in view models
    public INavigation Navigation { get; set; }

    public bool IsNotBusy => !isBusy;


    public async Task<bool> PreNetworkCheck()
    {
        try
        {
            var connected = _permissionService.CheckNetworkConnectivity();
            if (connected != NetworkAccess.Internet)
            {
                //  UserDialogs.Instance.Alert("No internet connection available.", "Oops", "Ok");
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

            //  await UserDialogs.Instance.AlertAsync("Something went wrong, please try again later.", "Error", "Ok");

            return true;
        }
    }
    private void ConnectivityOnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
    {
        // IsNotConnected = e.NetworkAccess != NetworkAccess.Internet;
    }
    // protected IToast Toast { get; } = DependencyService.Get<IToast>();

}
public static class ObservableCollectionExtensions
{
    public static void AddRange<T>(this ObservableCollection<T> lista, IEnumerable<T> items)
    {
        foreach (var item in items)
        {
            lista.Add(item);
        }
    }
}
