using System;
using HSNP.Interfaces;
using HSNP.Mobile.Views.Registration;
using HSNP.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace HSNP.Mobile.ViewModels
{
	public partial class AddNewHouseholdViewModel: BaseViewModel
    {
        private readonly IApi _api;
        public AddNewHouseholdViewModel(IApi api, INavigation navigation) : base(navigation)
        {
            _api = api;
            GetItems();
        }

        [ObservableProperty]
        private List<County> counties;
        [ObservableProperty]
        private List<Constituency> constituencies;
        [ObservableProperty]
        private List<SubLocation> subLocations;
        [ObservableProperty]
        private List<Village> villages;

        [ObservableProperty]
        private List<SystemCodeDetail> areaTypes;
        [ObservableProperty]
        private SystemCodeDetail areaType;

        [ObservableProperty]
        private string countyName;

        [ObservableProperty]
        private Constituency constituency;
        partial void OnConstituencyChanged(Constituency value)
        {
             GetSubLocations(value.Id);
        }
        
        [ObservableProperty]
        private SubLocation subLocation;
        partial void OnSubLocationChanged(SubLocation value)
        {
            GetVillages(value.Id);
        }

        public async void GetItems()
        {
            CountyName = App.User.CountyId.ToString();
            Constituencies = await App.db.Table<Constituency>().ToListAsync();
            if (Constituencies == null || !Constituencies.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Geo locations missing", "Go to Sync page and update Apps settings", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }
            var test= await App.db.Table<SystemCodeDetail>().ToListAsync();
            AreaTypes = await App.db.Table<SystemCodeDetail>().Where(i=>i.ComboCode== "RuralUrban").ToListAsync();
        }
        public async void GetSubLocations(int id)
        {
            SubLocations =await App.db.Table<SubLocation>().Where(i => i.ConstituencyId == id).ToListAsync();
        }
        public async void GetVillages(int id)
        {
            var test = await App.db.Table<Village>().ToListAsync();
            Villages = await App.db.Table<Village> ().Where(i => i.SubLocationId == id).ToListAsync();
        }
        [RelayCommand]
        async void SaveHouseHold()
        {
            await Navigation.PushAsync(new MembersPage());
        }
	}
}

