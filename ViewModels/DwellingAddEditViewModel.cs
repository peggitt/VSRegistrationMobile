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
        private List<SystemCodeDetail> dwellingRisks;
        [ObservableProperty]
        private SystemCodeDetail dwellingRisk;

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
        private SystemCodeDetail tukTukOwned;
       
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
        private List<SystemCodeDetail> householdConditions;
        [ObservableProperty]
        private SystemCodeDetail householdCondition;

        [ObservableProperty]
        private List<SystemCodeDetail> benefitTypes;
        [ObservableProperty]
        private SystemCodeDetail benefitType;
        
        [ObservableProperty]
        private HouseholdCharacteristic householdCharacteristic;
        [ObservableProperty]
        private Household household;

        [ObservableProperty]
        private List<SystemCodeDetail> tenureStatuses;
        [ObservableProperty]
        private SystemCodeDetail tenureStatus;

        private readonly HouseholdCharacteristicValidator _validator;
        private readonly HouseholdValidator _householdValidator;
        public DwellingAddEditViewModel(INavigation navigation) : base(navigation)
        {
            _ = GetItems();
            HouseholdId = App.HouseholdId;
            _validator = new HouseholdCharacteristicValidator();
            _householdValidator = new HouseholdValidator();
        }
        public async Task GetItems()
        {
            HouseholdCharacteristic = await App.db.Table<HouseholdCharacteristic>().FirstOrDefaultAsync(i=>i.HouseholdId==HouseholdId);
            HouseholdCharacteristic ??= new HouseholdCharacteristic { HouseholdId = HouseholdId };
            Household = await App.db.Table<Household>().FirstOrDefaultAsync(i => i.HouseholdId == HouseholdId);
            OwnershipOptions = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_Tenure" && i.Id<4).ToListAsync();
            TenureStatuses = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_Tenure" && i.Id > 3).ToListAsync();
            RoofMaterials = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Roof_Material").ToListAsync();
            WallMaterials = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Wall_Material").ToListAsync();
            FloorMaterials = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_Floor_material").ToListAsync();
            WaterSources = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Drinking_Water").ToListAsync();
            WasteDisposals = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Latrine").ToListAsync();
            CookingFuels = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Cooking_Fuel").ToListAsync();
            LightingFuels = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Lighting_Fuel").ToListAsync();
            BooleanAnswers = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "YesNo").ToListAsync();
            DwellingRisks = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_DwellingRisk").ToListAsync();
            HouseholdConditions = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_Condition").ToListAsync();
            BenefitTypes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "HH_Condition").ToListAsync();
            



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

            HouseholdCharacteristic.HouseholdId = HouseholdId;
            HouseholdCharacteristic.RoofMaterialId = RoofMaterial?.Id;
            HouseholdCharacteristic.WallMaterialId = WallMaterial?.Id;
            HouseholdCharacteristic.FloorMaterialId = FloorMaterial?.Id;
            HouseholdCharacteristic.DwellingRiskId = DwellingRisk?.Id;
            HouseholdCharacteristic.DrinkWaterId = WaterSource?.Id;
            HouseholdCharacteristic.HHToiletId = WasteDisposal?.Id;
            HouseholdCharacteristic.CookFuelId = CookingFuel?.Id;
            HouseholdCharacteristic.LightfuelId = LightingFuel?.Id;
            HouseholdCharacteristic.TVOwnedId = TVOwned?.Id;
            HouseholdCharacteristic.MotorCycleOwnedId = MotorCycleOwned?.Id;
            HouseholdCharacteristic.TuktukOwnedId = TukTukOwned?.Id;
            HouseholdCharacteristic.RefrigeratorOwnedId = RefrigeratorOwned?.Id;
            HouseholdCharacteristic.MobileOwnedId = MobileOwned?.Id;
            HouseholdCharacteristic.CarOwnedId = CarOwned?.Id;
            HouseholdCharacteristic.BicycleOwnedId = BicycleOwned?.Id;

            var hhValidationResult = _householdValidator.Validate(Household);
            var validationResult = _validator.Validate(HouseholdCharacteristic);
            if (validationResult.IsValid && hhValidationResult.IsValid)
            {
                App.Database.AddOrUpdate(Household);
                App.Database.AddOrUpdate(HouseholdCharacteristic);
                await Toast.SendToast("Dwelling and household information successfully");
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

