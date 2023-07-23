using HSNP.Services;
using HSNP.ViewModels;
using Mopups.Pages;

namespace HSNP.Mobile.Views;

public partial class DownloadHouseholdsPage : PopupPage
{
	public DownloadHouseholdsPage()
	{
		InitializeComponent();
        BindingContext = new DownloadHousholdsViewModel(ApiService.Instance, Navigation);
    }
}
