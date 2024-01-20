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

    public partial class UpdateDwellingViewModel : BaseViewModel
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
        private SystemCodeDetail receivingNSPPBeneficts;
        partial void OnReceivingNSPPBenefictsChanged(SystemCodeDetail value)
        {
            IsReceivingNSPPBeneficts = value?.Description == "Yes";
        }

        [ObservableProperty]
        private bool isReceivingNSPPBeneficts;

        [ObservableProperty]
        private SystemCodeDetail receivingOtherSocialAssistance;
        partial void OnReceivingOtherSocialAssistanceChanged(SystemCodeDetail value)
        {
           IsReceivingOtherSocialAssistance= value?.Description == "Yes";
        }

        [ObservableProperty]
        private bool isReceivingOtherSocialAssistance;



        

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
        partial void OnBenefitTypeChanged(SystemCodeDetail value)
        {
            IsCash = value?.Description == "CASH";
            IsInKind = !IsCash;
        }

        [ObservableProperty]
        private bool isCash;

        [ObservableProperty]
        private bool isInKind;
        

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

        [ObservableProperty]
        private List<SelectableItemWrapper<SystemCodeDetail>> programmes;
        public UpdateDwellingViewModel(INavigation navigation) : base(navigation)
        {

            _ = GetItems();
            HouseholdId = App.HouseholdId;
            _validator = new HouseholdCharacteristicValidator();
            _householdValidator = new HouseholdValidator();


        }
        public async Task GetItems()
        {
            try
            {
                var codes = await App.db.Table<SystemCodeDetail>().ToListAsync();
                HouseholdCharacteristic = await App.db.Table<HouseholdCharacteristic>().FirstOrDefaultAsync(i => i.HouseholdId == HouseholdId);
                HouseholdCharacteristic ??= new HouseholdCharacteristic { HouseholdId = HouseholdId };
                Household = await App.db.Table<Household>().FirstOrDefaultAsync(i => i.HouseholdId == HouseholdId);
                OwnershipOptions =  codes.Where(i => i.ComboCode == "HH_Tenure" && i.Id < 4).ToList();
               // OwnershipOption = OwnershipOptions.FirstOrDefault(i => i.Id == HouseholdCharacteristic.own);
                TenureStatuses = codes.Where(i => i.ComboCode == "HH_Tenure").ToList();
                TenureStatus = TenureStatuses.FirstOrDefault(i => i.Id == HouseholdCharacteristic.TenureId);

                RoofMaterials = codes.Where(i => i.ComboCode == "Roof_Material").ToList();
                RoofMaterial = RoofMaterials.FirstOrDefault(i=>i.Id==HouseholdCharacteristic.RoofMaterialId);

                WallMaterials = codes.Where(i => i.ComboCode == "Wall_Material").ToList();
                WallMaterial = WallMaterials.FirstOrDefault(i => i.Id == HouseholdCharacteristic.WallMaterialId);

                FloorMaterials = codes.Where(i => i.ComboCode == "HH_Floor_material").ToList();
                FloorMaterial = FloorMaterials.FirstOrDefault(i => i.Id == HouseholdCharacteristic.FloorMaterialId);

                WaterSources = codes.Where(i => i.ComboCode == "Drinking_Water").ToList();
                WaterSource = WaterSources.FirstOrDefault(i => i.Id == HouseholdCharacteristic.DrinkWaterMainId);

                WasteDisposals = codes.Where(i => i.ComboCode == "Latrine").ToList();
                WasteDisposal = WasteDisposals.FirstOrDefault(i => i.Id == HouseholdCharacteristic.HHToiletId);


                CookingFuels = codes.Where(i => i.ComboCode == "Cooking_Fuel").ToList();
                CookingFuel = CookingFuels.FirstOrDefault(i => i.Id == HouseholdCharacteristic.CookFuelId);

                LightingFuels = codes.Where(i => i.ComboCode == "Lighting_Fuel").ToList();
                LightingFuel = LightingFuels.FirstOrDefault(i => i.Id == HouseholdCharacteristic.LightFuelId);

                BooleanAnswers = codes.Where(i => i.ComboCode == "YesNo").ToList();
                DwellingRisks = codes.Where(i => i.ComboCode == "HH_DwellingRisk").ToList();
                DwellingRisk = DwellingRisks.FirstOrDefault(i => i.Id == HouseholdCharacteristic.DwellingRiskId);

                

               HouseholdConditions = codes.Where(i => i.ComboCode == "HH_Condition").ToList();
              //  HouseholdCondition = HouseholdConditions.FirstOrDefault(i => i.Id == HouseholdCharacteristic.con);

                BenefitTypes = codes.Where(i => i.ComboCode == "HH_BenefitKind").ToList();
                BenefitType = BenefitTypes.FirstOrDefault(i => i.Id == Household.BenefitTypeId);

                ReceivingNSPPBeneficts = BooleanAnswers.FirstOrDefault(i => i.Id == Household.HHReceivingNSNPBenefictsId);
                ReceivingOtherSocialAssistance = BooleanAnswers.FirstOrDefault(i => i.Id == Household.HHReceivingBenefictsId);

                SkippedMeal = BooleanAnswers.FirstOrDefault(i => i.Id == Household.HouseholdCutMealId);

                TVOwned= BooleanAnswers.FirstOrDefault(i => i.Id == HouseholdCharacteristic.TVOwnedId);
                MotorCycleOwned = BooleanAnswers.FirstOrDefault(i => i.Id == HouseholdCharacteristic.MotorcycleOwnedId);
                TukTukOwned = BooleanAnswers.FirstOrDefault(i => i.Id == HouseholdCharacteristic.TuktukOwnedId);
                RefrigeratorOwned = BooleanAnswers.FirstOrDefault(i => i.Id == HouseholdCharacteristic.RefrigeratorOwnedId);
                CarOwned = BooleanAnswers.FirstOrDefault(i => i.Id == HouseholdCharacteristic.CarOwnedId);
                MobileOwned = BooleanAnswers.FirstOrDefault(i => i.Id == HouseholdCharacteristic.MobileOwnedId);
                BicycleOwned = BooleanAnswers.FirstOrDefault(i => i.Id == HouseholdCharacteristic.BicycleOwnedId);

                var selectedProgs= await App.db.Table<HouseholdNSNPProgramme>().Where(i=>i.HouseholdId==HouseholdId).ToListAsync();
                var list = new List<SelectableItemWrapper<SystemCodeDetail>>();

                var nsnpProgrammes = codes.Where(i => i.ComboCode == "HH_Programmes").ToList();
                if (!selectedProgs.Any())
                    foreach (var item in nsnpProgrammes)
                        list.Add(new SelectableItemWrapper<SystemCodeDetail> { Item = item, IsSelected = false });
                else
                    foreach (var item in nsnpProgrammes)
                    {
                        list.Add(new SelectableItemWrapper<SystemCodeDetail>
                        { Item = item, IsSelected = selectedProgs.Any(x => x.ProgrammeId == item.Id) });
                    }
                Programmes = list;
                //var otherP = App.Database.SystemCodeDetailsGetByCode("Other SP Programme").ToList();
                //var progs = App.Database.GetTableRows<RegistrationProgramme>("RegistrationProgramme", "RegistrationId", Registration.Id.ToString());

                //var list = new List<SelectableItemWrapper<SystemCodeDetail>>();

                //if (!progs.Any())
                //{
                //    foreach (var item in otherP)
                //    {
                //        list.Add(new SelectableItemWrapper<SystemCodeDetail> { Item = item, IsSelected = false });
                //    }
                //}
                //else
                //{
                //    foreach (var item in otherP)
                //    {
                //        list.Add(new SelectableItemWrapper<SystemCodeDetail>
                //        { Item = item, IsSelected = progs.Any(x => x.ProgrammeId == item.Id) });
                //    }
                //}
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Sorry!", ex.ToString(), "Ok");
            }

        }
        [RelayCommand]
        async void Save()
        {
            string errors = "";
            // if (Programme == null)
            // errors += "Programme is required";
            if (TenureStatus == null)
                errors += "- (2.02) is required\n";
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
            Household.HHReceivingNSNPBenefictsId = ReceivingNSPPBeneficts?.Id;
            Household.HHReceivingBenefictsId = ReceivingOtherSocialAssistance?.Id;

            Household.HouseholdCutMealId = SkippedMeal?.Id;
            Household.BenefitTypeId = BenefitType?.Id;

          

            HouseholdCharacteristic.HouseholdId = HouseholdId;
            HouseholdCharacteristic.TenureId = TenureStatus?.Id;
            HouseholdCharacteristic.RoofMaterialId = RoofMaterial?.Id;
            HouseholdCharacteristic.WallMaterialId = WallMaterial?.Id;
            HouseholdCharacteristic.FloorMaterialId = FloorMaterial?.Id;
            HouseholdCharacteristic.DwellingRiskId = DwellingRisk?.Id;
            HouseholdCharacteristic.DrinkWaterMainId = WaterSource?.Id;
            HouseholdCharacteristic.HHToiletId = WasteDisposal?.Id;
            HouseholdCharacteristic.CookFuelId = CookingFuel?.Id;
            HouseholdCharacteristic.LightFuelId = LightingFuel?.Id;
            HouseholdCharacteristic.TVOwnedId = TVOwned?.Id;
            HouseholdCharacteristic.MotorcycleOwnedId = MotorCycleOwned?.Id;
            HouseholdCharacteristic.TuktukOwnedId = TukTukOwned?.Id;
            HouseholdCharacteristic.RefrigeratorOwnedId = RefrigeratorOwned?.Id;
            HouseholdCharacteristic.MobileOwnedId = MobileOwned?.Id;
            HouseholdCharacteristic.CarOwnedId = CarOwned?.Id;
            HouseholdCharacteristic.BicycleOwnedId = BicycleOwned?.Id;


            foreach(var prog in Programmes.Where(i => i.IsSelected)) {
                var item = new HouseholdNSNPProgramme { HouseholdId = Household.HouseholdId, ProgrammeId = prog.Item.Id };
                App.Database.AddOrUpdate(item);
                
            }
           

            var hhValidationResult = _householdValidator.Validate(Household);
            var validationResult = _validator.Validate(HouseholdCharacteristic);
            if (validationResult.IsValid && hhValidationResult.IsValid)
            {
                App.Database.AddOrUpdate(Household);
                App.Database.AddOrUpdate(HouseholdCharacteristic);
                await Toast.SendToast("Dwelling and household information successfully");
                await Shell.Current.GoToAsync($"{nameof(UpdatesMembersPage)}?HouseholdId={HouseholdId}");
            }
            else
            {
                var validateMessage = GetErrorListFromValidationResult(validationResult);
                await Application.Current.MainPage.DisplayAlert("Validation Errors", $"{hhValidationResult}{validateMessage}", "OK");
            }

        }
    }
}

