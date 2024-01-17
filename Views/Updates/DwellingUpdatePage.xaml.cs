using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class DwellingUpdatePage : ContentPage
{
	public DwellingUpdatePage()
	{
		InitializeComponent();
        BindingContext = new UpdateDwellingViewModel(Navigation);
    }
    protected override void OnAppearing()
    {
       // BindingContext = new DwellingAddEditViewModel(Navigation);
    }
}