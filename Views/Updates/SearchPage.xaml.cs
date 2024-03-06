using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class SearchPage : ContentPage
{
	public SearchPage()
	{
		InitializeComponent();
        BindingContext = new UpdatesSearchViewModel(ApiService.Instance, Navigation);
    }
}
