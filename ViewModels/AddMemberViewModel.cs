using Android.Util.Proto;
using HSNP.Interfaces;
using HSNP.Models;
using Java.Lang.Reflect;
using static Android.Media.TV.TvContract;

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
    private List<SelectableItemWrapper<SystemCodeDetail>> disabilities;

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

    [ObservableProperty]
    private List<SystemCodeDetail> identificationDocumentTypes;
    [ObservableProperty]
    private SystemCodeDetail identificationDocumentType;

    public AddMemberViewModel(INavigation navigation, IApi api,string id,string memberId) : base(navigation)
    {
        _api = api;
        HouseholdId = App.HouseholdId;
        MemberId = App.MemberId;
        _ = GetItems();
    }

    [ObservableProperty]
    private List<HouseholdMember> householdMembers;

    public async Task GetItems()
    {
        Member = await App.db.Table<HouseholdMember>().FirstOrDefaultAsync(i => i.Id == MemberId);
        Member ??= new HouseholdMember { Id = Guid.NewGuid().ToString() };
        MemberId = Member.Id;
        Caregivers= await App.db.Table<HouseholdMember>().ToListAsync();
        Spouses = await App.db.Table<HouseholdMember>().ToListAsync();
        BooleanAnswers = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "YesNo").ToListAsync();

        IdentificationDocumentTypes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Identification").ToListAsync();
        IdentificationDocumentType = IdentificationDocumentTypes.SingleOrDefault(i=>i.Id==Member.IdTypeId);

        Relationships = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Relationship").ToListAsync();
        Relationship = Relationships.SingleOrDefault(i => i.Id == Member.RelationshipId);

        Sexes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Sex").ToListAsync();
        Sex = Sexes.SingleOrDefault(i => i.Id == Member.SexId);

        MaritalStatuses = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Marital").ToListAsync();
        MaritalStatus = MaritalStatuses.SingleOrDefault(i => i.Id == Member.SexId);

       var  disabilityOptions = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Disability").ToListAsync();
        var selected = await App.db.Table<MemberDisability>().Where(i => i.MemberId == MemberId).ToListAsync();
        var list = new List<SelectableItemWrapper<SystemCodeDetail>>();

        
        if (!selected.Any())
            foreach (var item in disabilityOptions)
                list.Add(new SelectableItemWrapper<SystemCodeDetail> { Item = item, IsSelected = false });
        else
            foreach (var item in disabilityOptions)
            {
                list.Add(new SelectableItemWrapper<SystemCodeDetail>
                { Item = item, IsSelected = selected.Any(x => x.DisabilityId == item.Id) });
            }
        Disabilities = list;


        LearningStatuses = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_School").ToListAsync();
        LearningStatus = LearningStatuses.SingleOrDefault(i => i.Id == Member.LearningInstitutionId);

        EducationLevels = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_School_Grade").ToListAsync();
        EducationLevel = EducationLevels.SingleOrDefault(i => i.Id == Member.LearningInstitutionId);

        WorkTypes = await App.db.Table<SystemCodeDetail>().Where(i => i.ComboCode == "Member_Work_last_7days").ToListAsync();
        WorkType = WorkTypes.SingleOrDefault(i => i.Id == Member.WorkLast7daysId);

        SpouseInHousehold = BooleanAnswers.SingleOrDefault(i => i.Id == Member.SpouseStatusId);
        FatherAlive = BooleanAnswers.SingleOrDefault(i => i.Id == Member.FatherAliveId);
        MotherAlive = BooleanAnswers.SingleOrDefault(i => i.Id == Member.MotherAliveId);
        ChronicIllness = BooleanAnswers.SingleOrDefault(i => i.Id == Member.ChronicIllnessId);
        HasDisability = BooleanAnswers.SingleOrDefault(i => i.Id == Member.DisabilityId);

        DisabilityCareStatus = BooleanAnswers.SingleOrDefault(i => i.Id == Member.Need24HrCareId);

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

        Member.IdTypeId = IdentificationDocumentType?.Id;
        Member.RelationshipId = Relationship?.Id;
        Member.SexId = Sex?.Id;
        Member.MaritalStatusId = MaritalStatus?.Id;
        Member.SpouseStatusId = SpouseInHousehold?.Id;
        Member.SpouseId = Spouse?.Id;
        Member.FatherAliveId = FatherAlive?.Id;
        Member.MotherAliveId = MotherAlive?.Id;
        Member.ChronicIllnessId = ChronicIllness?.Id;
        Member.DisabilityId = HasDisability?.Id;
        Member.Need24HrCare = DisabilityCareStatus.Description.Equals("Yes");
        Member.Need24HrCareId = DisabilityCareStatus?.Id;
        Member.CaregiverId = Caregiver?.Id;
        Member.LearningInstitutionId = LearningStatus?.Id;
        Member.HighestGradeCodeId = EducationLevel?.Id;
        Member.WorkLast7daysId = WorkType?.Id;
        Member.WorkingInFormalJobId = JobOption?.Id;
        Member.Id = MemberId;
        Member.HouseholdId = HouseholdId;
        Member.IsComplete = true;
        App.Database.AddOrUpdate(Member);
        var selected = Disabilities.Where(i => i.IsSelected);
        foreach (var disability in selected)
        {
            var item = new MemberDisability { MemberId = Member.Id, DisabilityId = disability.Item.Id };
            App.Database.AddOrUpdate(item);

        }
        // await Navigation.PushAsync(new MembersPage(Household.HouseholdId));
        await Toast.SendToast("Household member information saved successfully");
        await Shell.Current.GoToAsync($"/{nameof(MembersPage)}?HouseholdId={Member.HouseholdId}");
    }
}
