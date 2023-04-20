using Android.Util.Proto;
using HSNP.Interfaces;
using HSNP.Models;

namespace HSNP.Mobile.ViewModels;


public partial class AddMemberViewModel : BaseViewModel
{

    private readonly IApi _api;
    [ObservableProperty]
    private int heightRequest;

    [ObservableProperty]
    private string householdId;

    [ObservableProperty]
    private string memberId;

    [ObservableProperty]
    private HouseholdMember member;

   
    [ObservableProperty]
    private List<SystemCodeDetail> relationships;
    [ObservableProperty]
    private SystemCodeDetail relationship;

    [ObservableProperty]
    private List<SystemCodeDetail> sexes;
    [ObservableProperty]
    private SystemCodeDetail sex;

    [ObservableProperty]
    private List<SystemCodeDetail> maritalStatuses;
    [ObservableProperty]
    private SystemCodeDetail maritalStatus;

    [ObservableProperty]
    private List<SystemCodeDetail> booleanAnswers;

    [ObservableProperty]
    private List<HouseholdMember> spouses;
    [ObservableProperty]
    private HouseholdMember spouse;

    [ObservableProperty]
    private SystemCodeDetail spouseInHousehold;
    

    [ObservableProperty]
    private SystemCodeDetail fatherAlive;

    [ObservableProperty]
    private SystemCodeDetail motherAlive;

    [ObservableProperty]
    private SystemCodeDetail chronicIllness;
    


    [ObservableProperty]
    private List<SystemCodeDetail> disabilities;
    [ObservableProperty]
    private SystemCodeDetail hasDisability;

    [ObservableProperty]
    private SystemCodeDetail disabilityCareStatus;
    

    [ObservableProperty]
    private List<HouseholdMember> caregivers;
    [ObservableProperty]
    private HouseholdMember caregiver;

    [ObservableProperty]
    private List<SystemCodeDetail> learningStatuses;
    [ObservableProperty]
    private SystemCodeDetail learningStatus;

    [ObservableProperty]
    private List<SystemCodeDetail> educationLevels;
    [ObservableProperty]
    private SystemCodeDetail educationLevel;

    [ObservableProperty]
    private List<SystemCodeDetail> workTypes;
    [ObservableProperty]
    private SystemCodeDetail workType;
    [ObservableProperty]
    private SystemCodeDetail jobOption;
    

    public AddMemberViewModel(INavigation navigation, IApi api,string id,string memberId) : base(navigation)
    {
        _api = api;
        HouseholdId = App.HouseholdId;
        MemberId = App.MemberId;
        GetItems();
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    public async void GetItems()
    {
        Member = await App.db.Table<HouseholdMember>().FirstOrDefaultAsync(i => i.Id == MemberId);
        if (Member == null)
            Member = new HouseholdMember { Id = Guid.NewGuid().ToString() };
        MemberId = Member.Id;

        Caregivers= await App.db.Table<HouseholdMember>().ToListAsync();
        Spouses = await App.db.Table<HouseholdMember>().ToListAsync();

        BooleanAnswers = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "YesNo").ToListAsync();
        Relationships = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Relationship").ToListAsync();
        Sexes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Sex").ToListAsync();
        MaritalStatuses = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Marital").ToListAsync();
        Disabilities = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Disability").ToListAsync();
        LearningStatuses = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_School").ToListAsync();
        EducationLevels = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_School_Grade").ToListAsync();
        WorkTypes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Work_last_7days").ToListAsync();
       
    }

    [RelayCommand]
    async void Save()
    {
        var errors="";
        if (DisabilityCareStatus == null)
            errors += "(3.12) is required";

        if (!string.IsNullOrEmpty(errors))
        {
            await Shell.Current.DisplayAlert("Error", errors, "OK");
            return;
        }
        Member = new HouseholdMember
        {
            RelationshipId=Relationship?.Id,
            SexId=Sex?.Id,
            MaritalStatusId=MaritalStatus?.Id,
            SpouseStatusId= SpouseInHousehold?.Id,
            SpouseId=Spouse?.Id,
            FatherAliveId=FatherAlive?.Id,
            MotherAliveId=MotherAlive?.Id,
            ChronicIllnessId=ChronicIllness?.Id,
            DisabilityId=HasDisability?.Id,
            Need24HrCare=DisabilityCareStatus.Description.Equals("Yes"),
            CaregiverId=Caregiver?.Id,
            LearningInstitutionId= LearningStatus?.Id,
            HighestGradeCodeId = EducationLevel?.Id,
            WorkLast7daysId=WorkType?.Id,
            WorkingInFormalJobId = JobOption?.Id

        };
        Member.Id = MemberId;
        Member.HouseholdId = HouseholdId;
  
        App.Database.AddOrUpdate(Member);
        // await Navigation.PushAsync(new MembersPage(Household.HouseholdId));
        await Shell.Current.GoToAsync($"/{nameof(MembersPage)}?HouseholdId={Member.HouseholdId}");
    }
}
