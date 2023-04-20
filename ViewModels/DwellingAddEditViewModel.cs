using System;
using Acr.UserDialogs;
using HSNP.Interfaces;
using HSNP.Mobile.Validators;
using HSNP.Mobile.Views;
using HSNP.Models;
using Kotlin.Jvm.Internal;

namespace HSNP.Mobile.ViewModels
{
    [QueryProperty(nameof(HouseholdId), nameof(HouseholdId))]
    [QueryProperty(nameof(Id), nameof(Id))]

    public partial class DwellingAddEditViewModel: BaseViewModel
    {
       
        [ObservableProperty]
        private string householdId;
        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private List<SystemCodeDetail> ownershipOptions;
        [ObservableProperty]
        private List<SystemCodeDetail> roofMaterials;
        [ObservableProperty]
        private SystemCodeDetail roofMaterial;

        [ObservableProperty]
        private List<SystemCodeDetail> wallMaterials;
        [ObservableProperty]
        private SystemCodeDetail wallMaterial;

        [ObservableProperty]
        private List<SystemCodeDetail> floorMaterials;
        [ObservableProperty]
        private SystemCodeDetail floorMaterial;

        [ObservableProperty]
        private List<SystemCodeDetail> waterSources;
        [ObservableProperty]
        private SystemCodeDetail waterSource;

        [ObservableProperty]
        private List<SystemCodeDetail> wasteDisposals;
        [ObservableProperty]
        private SystemCodeDetail wasteDisposal;


        [ObservableProperty]
        private List<SystemCodeDetail> cookingFuels;
        [ObservableProperty]
        private SystemCodeDetail cookingFuel;

        [ObservableProperty]
        private List<SystemCodeDetail> lightingFuels;
        [ObservableProperty]
        private SystemCodeDetail lightingFuel;

        [ObservableProperty]
        private List<SystemCodeDetail> booleanAnswers;
        [ObservableProperty]
        private SystemCodeDetail tVOwned;
        [ObservableProperty]
        private SystemCodeDetail motorCycleOwned;
        [ObservableProperty]
        private SystemCodeDetail tuktukOwned;
       
        [ObservableProperty]
        private SystemCodeDetail refrigeratorOwned;
        [ObservableProperty]
        private SystemCodeDetail carOwned;
        [ObservableProperty]
        private SystemCodeDetail bicycleOwned;
        [ObservableProperty]
        private SystemCodeDetail mobileOwned;
        [ObservableProperty]
        private SystemCodeDetail skippedMeal;
        [ObservableProperty]
        private SystemCodeDetail isReceivingOtherSocialAssistance;
        [ObservableProperty]
        private string otherProgrammeNames;

        [ObservableProperty]
        private SystemCodeDetail benefitType;

        [ObservableProperty]
        private HouseholdCharacteristic householdCharacteristic;
        [ObservableProperty]
        private Household household;

        private readonly HouseholdCharacteristicValidator _validator;
        private readonly HouseholdValidator _householdValidator;
        public DwellingAddEditViewModel(INavigation navigation) : base(navigation)
        {
            GetItems();
            HouseholdId = App.HouseholdId;
            _validator = new HouseholdCharacteristicValidator();
            _householdValidator = new HouseholdValidator();
        }
        public async void GetItems()
        {
            HouseholdCharacteristic = await App.db.Table<HouseholdCharacteristic>().FirstOrDefaultAsync(i=>i.HouseholdId==HouseholdId);
            Household = await App.db.Table<Household>().FirstOrDefaultAsync(i => i.HouseholdId == HouseholdId);
            OwnershipOptions = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_Tenure").ToListAsync();
            RoofMaterials = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Roof_Material").ToListAsync();
            WallMaterials = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Wall_Material").ToListAsync();
            FloorMaterials = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_Floor_material").ToListAsync();
            WaterSources = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Drinking_Water").ToListAsync();
            WasteDisposals = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Latrine").ToListAsync();
            CookingFuels = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Cooking_Fuel").ToListAsync();
            LightingFuels = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Lighting_Fuel").ToListAsync();
            BooleanAnswers = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "YesNo").ToListAsync();

        }
       [RelayCommand]
        async void Save()
        {
            string errors = "";
            // if (Programme == null)
            // errors += "Programme is required";

            if (RoofMaterial == null)
                errors += "- (2.03) is required\n";
            if (WallMaterial == null)
                errors += "- (2.04) is required\n";
            if (FloorMaterial == null)
                errors += "- (2.05) is required\n";

            if (!string.IsNullOrEmpty(errors))
            {
               // await Application.Current.MainPage.DisplayAlert("Error", errors, "OK");
               // return;
            }

            Household.HouseholdCutMealId = SkippedMeal?.Id;
            Household.BenefitTypeId = BenefitType?.Id;

           // IsReceivingOtherSocialAssistanceId = IsReceivingOtherSocialAssistance?.Id,
            //BenefitTypeId = BenefitType?.Id

            HouseholdCharacteristic = new HouseholdCharacteristic
            {
                HouseholdId=HouseholdId,
                RoofMaterialId=RoofMaterial?.Id,
                WallMaterialId = WallMaterial?.Id,
                FloorMaterialId= FloorMaterial?.Id,
                DrinkWaterId= WaterSource?.Id,
                HHToiletId= WasteDisposal?.Id,
                CookFuelId = CookingFuel?.Id,
                LightfuelId=LightingFuel?.Id,
                TVOwnedId=TVOwned?.Id,
                MotorCycleOwnedId= MotorCycleOwned?.Id,
                TuktukOwnedId= TuktukOwned?.Id,
                RefrigeratorOwnedId= RefrigeratorOwned?.Id,
                MobileOwnedId= MobileOwned?.Id,
                CarOwnedId = CarOwned?.Id,
                BicycleOwnedId= BicycleOwned?.Id,
               
              
            };
           
            var hhValidationResult = _householdValidator.Validate(Household);
            var validationResult = _validator.Validate(HouseholdCharacteristic);
            if (validationResult.IsValid && hhValidationResult.IsValid)
            {
                App.Database.AddOrUpdate(Household);
                App.Database.AddOrUpdate(HouseholdCharacteristic);
                await Shell.Current.GoToAsync($"{nameof(MembersPage)}?HouseholdId={HouseholdId}");
            }
            else
            {
                var validateMessage = GetErrorListFromValidationResult(validationResult);
                await Application.Current.MainPage.DisplayAlert("Validation Errors",$"{hhValidationResult}{validateMessage}", "OK");
            }
               
        }
	}
}

