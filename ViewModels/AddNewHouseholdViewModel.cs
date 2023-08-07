using System;
using HSNP.Interfaces;
using HSNP.Mobile.Views;
using HSNP.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using static Android.Content.ClipData;
using HSNP.Mobile.Validators;
using FluentValidation;

namespace HSNP.Mobile.ViewModels
{
    public partial class AddNewHouseholdViewModel : BaseViewModel
    {
        private readonly IApi _api;
        private readonly AddHouseholdValidator _hhValidator;
        private readonly AddHouseholdMemberValidator _hhMValidator;
        public AddNewHouseholdViewModel(IApi api, INavigation navigation) : base(navigation)
        {
            _api = api;
            _ = GetItems();
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
            if (value != null)
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

        public async Task GetItems()
        {
            try
            {

                CountyName = App.User.CountyId.ToString();
                Constituencies = await App.db.Table<Constituency>().ToListAsync();

                AreaTypes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "RuralUrban").ToListAsync();

                BooleanAnswers = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "YesNo").ToListAsync();

                //  IdentificationDocumentTypes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Identification").ToListAsync();
                //IdentificationDocumentType = IdentificationDocumentTypes.SingleOrDefault(i => i.Id == HouseholdMember.IdTypeId);

                //   Sexes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Sex").ToListAsync();
                //  Sex = Sexes.SingleOrDefault(i => i.Id == HouseholdMember.SexId);

                //   Relationships = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Relationship").ToListAsync();
                //  Relationship = Relationships.SingleOrDefault(i => i.Id == HouseholdMember.RelationshipId);
                if (App.HouseholdId != null)
                {
                    Household = await App.db.Table<Household>().FirstAsync(i => i.HouseholdId == App.HouseholdId);
                    HouseholdMember = await App.db.Table<HouseholdMember>().FirstAsync(i => i.HouseholdId == App.HouseholdId);
                    AreaType = AreaTypes.FirstOrDefault(i => i.Id == Household.RuralUrbanId);

                    string villageId = Household.VillageId;
                   
                    var village= await App.db.Table<Village>().FirstOrDefaultAsync(i => i.Id == villageId);
                    var sublocationId = village.SubLocationId;
                    Villages = await App.db.Table<Village>().Where(i => i.SubLocationId == sublocationId).ToListAsync();
                  
                    var subLocation = await App.db.Table<SubLocation>().FirstOrDefaultAsync(i => i.Id == sublocationId);
                    var constituencyId = subLocation.ConstituencyId;
                   // SubLocations = await App.db.Table<SubLocation>().Where(i => i.ConstituencyId == constituencyId).ToListAsync();
                   

                    Constituency = Constituencies.FirstOrDefault(i => i.Id == constituencyId);
                    SubLocation = subLocation;
                    Village = village;
                }
                else
                {
                    Household = new Household { HouseholdId = Guid.NewGuid().ToString(), CreatedOn = DateTime.UtcNow, EntryDate = DateTime.UtcNow };
                    HouseholdMember = new HouseholdMember { Id = Guid.NewGuid().ToString(), IsApplicant = true, CreatedOn = DateTime.UtcNow.ToString("yyyy-MM-dd") };
                    Household.ApplicantId = HouseholdMember.Id;
                    HouseholdMember.HouseholdId = Household.HouseholdId;

                }

                if (Constituencies == null || !Constituencies.Any())
                {
                    await Application.Current.MainPage.DisplayAlert("Geo locations missing", "Go to Sync page and update Apps settings", "OK");
                    await Shell.Current.GoToAsync("..");
                    await Shell.Current.GoToAsync($"/{nameof(SyncPage)}");
                    return;
                }

                IsBeneficiaryHH = BooleanAnswers.SingleOrDefault(i => i.Id == Household.IsBeneficiaryHHId);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Sorry!", ex.ToString(), "Ok");
            }


        }
        public async void GetSubLocations(int id)
        {
            SubLocations = await App.db.Table<SubLocation>().Where(i => i.ConstituencyId == id).ToListAsync();
        }
        public async void GetVillages(int id)
        {
            Villages = await App.db.Table<Village>().Where(i => i.SubLocationId == id).ToListAsync();
        }
        [RelayCommand]
        async void Save()
        {
            try
            {
                string errors = "";
                // if (Programme == null)
                // errors += "Programme is required";

                //if (IsBeneficiaryHH == null)
                //    errors += "Is Applicant Household Head is required\n";
                if (Village == null)
                    errors += "Village is required\n";
                if (AreaType == null)
                    errors += "Area Type is required\n";
                //if (Relationship == null)
                //    errors += "Member's relationship is Required\n";



                if (Household.HouseholdAddress == null)
                    errors += "(1.14) PHYSICAL ADDRESS is required\n";
                if (Household.NearestChurchMqs == null)
                    errors += "(1.16) NEAREST CHURCH/MOSQUE is required\n";
                if (Household.NearestSchool == null)
                    errors += "(1.17) NEAREST SCHOOL is required\n";


                var hhValidationResult = _hhValidator.Validate(Household);
                var hhMvalidationResult = _hhMValidator.Validate(HouseholdMember);

                //HouseholdMember.IdTypeId = IdentificationDocumentType?.Id;
                //HouseholdMember.SexId = Sex?.Id;
                //HouseholdMember.RelationshipId = Relationship?.Id;
                HouseholdMember.SerialNo = "1";

                if (!string.IsNullOrEmpty(errors) || !hhValidationResult.IsValid)
                {
                    var validateMessage = GetErrorListFromValidationResult(hhValidationResult);
                    await Application.Current.MainPage.DisplayAlert("Error", $"{errors}{validateMessage}{hhMvalidationResult}", "OK");
                    //  await Application.Current.MainPage.DisplayAlert("Error", $"{errors}{validateMessage}", "OK");
                    return;
                }
                IsBusy = true;
                Household.IsBeneficiaryHHId = IsBeneficiaryHH.Id;
                Household.IsBeneficiaryHH = IsBeneficiaryHH.Description.Equals("Yes");
                Household.VillageId = Village.Id;
                Household.Village = Village.Name;

                Household.RuralUrbanId = AreaType.Id;
                Household.RegisteredBy = App.User.Email;
                Household.ApplicantName = $"{HouseholdMember.FirstName} {HouseholdMember.MiddleName} {HouseholdMember.Surname}";

                await GetLocationAsync();

                App.Database.AddOrUpdate(Household);
                App.Database.AddOrUpdate(HouseholdMember);
                App.HouseholdId = Household.HouseholdId;
                // await Navigation.PushAsync(new MembersPage(Household.HouseholdId));

                //  await Navigation.PushAsync(new DwellingAddEditPage());
                await Shell.Current.GoToAsync($"/{nameof(DwellingAddEditPage)}?HouseholdId={Household.HouseholdId}");
                await Toast.SendToast("Geographic identification information successfully");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Sorry!", ex.Message, "Ok");
            }

        }
        async Task GetLocationAsync()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium, // You can adjust the desired accuracy
                    Timeout = TimeSpan.FromSeconds(30) // Set a timeout for the location request
                });

                if (location != null)
                {
                    Household.Latitude = location.Latitude;
                    Household.Longitude = location.Longitude;

                    // Do something with latitude and longitude, e.g., display on UI
                    // You can also store these values or use them in any other way as needed.
                }
                else
                {
                    // Handle the case when the location is not available or the user denied permission.
                }
            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException ex)
            {
                // Handle feature not enabled on device exception
            }
            catch (PermissionException ex)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
            IsBusy = false;
        }

    }
}