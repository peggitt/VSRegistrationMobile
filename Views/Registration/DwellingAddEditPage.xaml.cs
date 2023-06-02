using HSNP.Services;
using HSNP.ViewModels;

namespace HSNP.Mobile.Views;

public partial class DwellingAddEditPage : ContentPage
{
	public DwellingAddEditPage()
	{
		InitializeComponent();
        BindingContext = new DwellingAddEditViewModel(Navigation);
    }
    protected override void OnAppearing()
    {
       // BindingContext = new DwellingAddEditViewModel(Navigation);
    }
}