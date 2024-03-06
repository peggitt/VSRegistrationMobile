using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class SyncPage : ContentPage
{
	public SyncPage()
	{
		InitializeComponent();
      
    }
    protected override void OnAppearing()
    {
        BindingContext = new SyncViewModel(ApiService.Instance, Navigation);
    }
}
