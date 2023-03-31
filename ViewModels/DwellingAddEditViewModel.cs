using System;
using HSNP.Interfaces;
using HSNP.Mobile.Views.Registration;
using HSNP.Models;

namespace HSNP.Mobile.ViewModels
{
	public partial class DwellingAddEditViewModel: BaseViewModel
    {
        private readonly IApi _api;
        public DwellingAddEditViewModel(IApi api, INavigation navigation) : base(navigation)
        {
            _api = api;
        }

        [RelayCommand]
        async void SaveHouseHold()
        {
            await Navigation.PushAsync(new MembersPage());
        }
	}
}

