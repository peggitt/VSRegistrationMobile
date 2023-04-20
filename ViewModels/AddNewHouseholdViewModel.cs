﻿using System;
using HSNP.Interfaces;
using HSNP.Mobile.Views;
using HSNP.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using static Android.Content.ClipData;
using HSNP.Mobile.Validators;
using FluentValidation;

namespace HSNP.Mobile.ViewModels
{
	public partial class AddNewHouseholdViewModel: BaseViewModel
    {
        private readonly IApi _api;
        private readonly AddHouseholdValidator _hhValidator;
        private readonly AddHouseholdMemberValidator _hhMValidator;
        public AddNewHouseholdViewModel(IApi api, INavigation navigation) : base(navigation)
        {
            _api = api;
            GetItems();
            _hhValidator = new AddHouseholdValidator();
            _hhMValidator = new AddHouseholdMemberValidator();
        }

        [ObservableProperty]
        private Household household;

        [ObservableProperty]
        private HouseholdMember householdMember;

        [ObservableProperty]
        private List<County> counties;
        [ObservableProperty]
        private List<Constituency> constituencies;
        [ObservableProperty]
        private List<SubLocation> subLocations;
        [ObservableProperty]
        private List<Village> villages;

        [ObservableProperty]
        private Village village;

        [ObservableProperty]
        private List<SystemCodeDetail> areaTypes;
        [ObservableProperty]
        private SystemCodeDetail areaType;


        [ObservableProperty]
        private List<SystemCodeDetail> booleanAnswers;
        [ObservableProperty]
        private SystemCodeDetail isBeneficiaryHH;

        [ObservableProperty]
        private SystemCodeDetail programme;

        [ObservableProperty]
        private List<SystemCodeDetail> relationships;
        [ObservableProperty]
        private SystemCodeDetail relationship;

        [ObservableProperty]
        private string countyName;

        [ObservableProperty]
        private Constituency constituency;
        partial void OnConstituencyChanged(Constituency value)
        {
            if (value != null)
                GetSubLocations(value.Id);
        }
        
        [ObservableProperty]
        private SubLocation subLocation;
        partial void OnSubLocationChanged(SubLocation value)
        {
            if(value!=null)
             GetVillages(value.Id);
        }


        [ObservableProperty]
        private List<SystemCodeDetail> identificationDocumentTypes;
        [ObservableProperty]
        private SystemCodeDetail identificationDocumentType;

        [ObservableProperty]
        private List<SystemCodeDetail> sexes;
        [ObservableProperty]
        private SystemCodeDetail sex;

        public async void GetItems()
        {
            CountyName = App.User.CountyId.ToString();
            Constituencies = await App.db.Table<Constituency>().ToListAsync();
            // Villages = await App.db.Table<Village>().ToListAsync();
            if (Constituencies == null || !Constituencies.Any())
            {
                await Application.Current.MainPage.DisplayAlert("Geo locations missing", "Go to Sync page and update Apps settings", "OK");
                await Shell.Current.GoToAsync("..");
                return;
            }
           
            AreaTypes = await App.db.Table<SystemCodeDetail>().Where(i=>i.ComboCode== "RuralUrban").ToListAsync();
            BooleanAnswers = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "YesNo").ToListAsync();

            IdentificationDocumentTypes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Identification").ToListAsync();
            Sexes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Sex").ToListAsync();
            Relationships = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Relationship").ToListAsync();

            if (App.HouseholdId != null)
            {
                Household =await App.db.Table<Household>().FirstAsync(i=>i.HouseholdId== App.HouseholdId);
                HouseholdMember =await App.db.Table<HouseholdMember>().FirstAsync(i => i.HouseholdId == App.HouseholdId);

            }
            else
            {
                Household = new Household { HouseholdId = Guid.NewGuid().ToString(), CreatedOn = DateTime.UtcNow };
                HouseholdMember = new HouseholdMember { Id = Guid.NewGuid().ToString(), IsApplicant = true, CreatedOn = DateTime.UtcNow };
                Household.ApplicantId = HouseholdMember.Id;
                HouseholdMember.HouseholdId = Household.HouseholdId;
            }
        }
        public async void GetSubLocations(int id)
        {
            SubLocations =await App.db.Table<SubLocation>().Where(i => i.ConstituencyId == id).ToListAsync();
        }
        public async void GetVillages(int id)
        {
            Villages = await App.db.Table<Village> ().Where(i => i.SubLocationId == id).ToListAsync();
        }
        [RelayCommand]
        async void Save()
        {
            string errors = "";
           // if (Programme == null)
               // errors += "Programme is required";

            if (IsBeneficiaryHH == null)
                errors += "Is Applicant Household Head is required\n";
            if (Village == null)
                errors += "Village is required\n";
            if (AreaType == null)
                errors += "Area Type is required\n";
            if (Relationship == null)
                errors += "Member's relationship is Required\n";
            var hhValidationResult = _hhValidator.Validate(Household);
            var hhMvalidationResult = _hhMValidator.Validate(HouseholdMember);

            if (!string.IsNullOrEmpty(errors) || !hhValidationResult.IsValid || !hhMvalidationResult.IsValid)
            {
                var validateMessage = GetErrorListFromValidationResult(hhValidationResult);
                await Application.Current.MainPage.DisplayAlert("Error",$"{errors}{validateMessage}{hhMvalidationResult}", "OK");
                return;
            }
            Household.IsBeneficiaryHH = IsBeneficiaryHH.Id;
            Household.VillageId = Village.Id;
            Household.AreaTypeId = AreaType.Id;
            
            App.Database.AddOrUpdate(Household);
            App.Database.AddOrUpdate(HouseholdMember);
            App.HouseholdId = Household.HouseholdId;
           // await Navigation.PushAsync(new MembersPage(Household.HouseholdId));
             await Shell.Current.GoToAsync($"/{nameof(DwellingAddEditPage)}?HouseholdId={Household.HouseholdId}");
        }
	}
}