using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class SyncPage : ContentPage
{
	public SyncPage()
	{
		InitializeComponent();
        BindingContext = new SyncViewModel(ApiService.Instance, Navigation);
    }
}
