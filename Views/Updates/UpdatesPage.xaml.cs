using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class UpdatesPage : ContentPage
{
	public UpdatesPage()
	{
		InitializeComponent();
        BindingContext = new UpdatesViewModel(ApiService.Instance,Navigation);
    }
}
