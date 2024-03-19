using SQLite;

namespace HSNP.Models
{
    public class Household
    {
        [PrimaryKey]
        public string HouseholdId { get; set; }
        public string ApplicantId { get; set; } = "";
        public string UserName { get; set; }
        public string VillageId { get; set; }
        public DateTime? EntryDate { get; set; }
        public int? IsBeneficiaryHHId { get; set; }
        public bool? IsBeneficiaryHH { get; set; } = false;

        public string BeneficiaryGroup { get; set; }
        public int? HHReceivingNSNPBenefictsId { get; set; } /*New*/

        public bool CTOVC { get; set; }
        public bool HSNP { get; set; }
        public bool OPCT { get; set; }
        public bool PWSDCT { get; set; }
        public bool UFSCT { get; set; }

        public string OtherCT { get; set; } = "";
        public string HouseholdAddress { get; set; }

        public string Neighbourhood { get; set; } ="";
        public string NearestChurchMqs { get; set; }
        public string NearestSchool { get; set; }

        public int? RuralUrbanId { get; set; }
        public int? AreaTypeId { get; set; }

        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public int? LiveBirths { get; set; }
        public int? StillBirths { get; set; }
        public int? OtherBenefitsAmout { get; set; }
        public int? HouseholdCutMealId { get; set; }

        public int? HHReceivingBenefictsId { get; set; }
        public int? BenefitTypeId { get; set; }
        public string OtherBenefits { get; set; } = "";
        
        public int? DataLevelId { get; set; } = 1;
        public string BenefitKind { get; set; } = "";
        public string ProgrammeName { get; set; }
        
        public string Comments { get; set; } = "";
        public string RegisteredBy { get; set; }
        public DateTime? RegisteredOn { get; set; } = DateTime.UtcNow;
        public string ApproverName { get; set; }
        public string ApprovedOn { get; set; }
        public bool? MarkForDownload { get; set; } = false;
        public string HHIDText { get; set; } = "";
        public bool PotentialDuplicate { get; set; } = false;
       
        public DateTime? CreatedOn { get; set; }
        public int? HouseholdMembers { get; set; }

        public bool IsComplete { get; set; }
        public bool Uploaded { get; set; }

        /*Do we still need this*/
        public int? DurationYears { get; set; }
        public int? DurationMonths { get; set; }
        public string PhoneNumber { get; set; }

        public string ApplicantName { get; set; }
        public string Village { get; set; }

        /*Update Related Fields*/
        public int? SubLocationId { get; set; }
        public DateTime? UpdatedOn { get; set; } = DateTime.UtcNow;
        public string UpdatedBy { get; set; }
        public bool UpdatedComplete { get; set; }

        public bool Editting { get; set; }
    }
    public class HouseholdMember
    {
        [PrimaryKey]

        public string Id { get; set; }
        public string HouseholdId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Surname { get; set; }
        public string IdNumber { get; set; }
        public int? IdTypeId { get; set; }
        public bool IsApplicant { get; set; }
        public bool IsComplete { get; set; }
        public string CreatedOn { get; set; }
        public string Name => $"{FirstName} {MiddleName} {Surname}";


        public string UserName { get; set; }



        private string _hhMemberRosterId;

        public string HHMemberRosterId
        {
            get
            {
                // Check if _hhMemberRosterId is null, and if so, assign a value
                return _hhMemberRosterId ?? Id;
            }
            set
            {
                _hhMemberRosterId = value;
            }
        }
        public int? RelationshipId { get; set; }
       
        public int? SexId { get; set; }
        public string BirthCertificate { get; set; } = "";


        public DateTime DateOfBirth { get; set; }



        public int YearOfBirth => DateOfBirth.Year;
        public int? FatherAliveId { get; set; }
        public int? MotherAliveId { get; set; }
        public int? HighestGradeCodeId { get; set; }
        public int? Worklast7daysId { get; set; }
        public int? ChronicillnessId { get; set; }
        public int? ChronicillYrs { get; set; }
        public int? ChronicillMths { get; set; }
        public bool? VisualDisability { get; set; } = false;
        public bool? HearingDisability { get; set; } = false;
        public bool? SpeechDisability { get; set; } = false;
        public bool? MentalDisability { get; set; } = false;
        public bool? SelfCareDisability { get; set; } = false;


        public bool? Need24HrCare { get; set; } = false;
        public int? Need24HrCareId { get; set; }
        public string EntryDate { get; set; } = DateTime.UtcNow.ToString("yyyy-MM-dd");

        public string RetypedIdNo { get; set; } = "";
        public bool? IDIPRSValid => false;
        public string UserCode { get; set; } = "";
        public bool isNominatedAccHoloder { get; set; } = false;
        public int? MaritalStatusId { get; set; }
        public string SpouseId { get; set; }
        public string MobileNo { get; set; } = "";
        public string CaregiverId { get; set; }
        public int? DisabilityId { get; set; }
        public int? LearningInstitutionId { get; set; }
        public decimal? MonthEarnings { get; set; } = 0;
        public int? SpouseStatusId { get; set; }
        public bool MarkForDownload { get; set; }
        public string RegisteredBy { get; set; }
        //(3.17) Does Member's work on a formal job, teaching, public sector, NGO/FBO?
        public int? WorkingId { get; set; } = 0;
        public string SerialNo { get; set; }
        public string WaitingCardNo { get; set; } = "";
        public string PSPAccountId { get; set; } = "";

        /*Temp*/
        public bool? SelfCareDisabilityId => SelfCareDisability;

        /*To Identify downloaded records*/
        public int? SubLocationId { get; set; }

    }


    public class HouseholdCharacteristic
    {
        [PrimaryKey]
        public string HouseholdId { get; set; }
        public string UserName { get; set; }

        public int? NoMainRooms { get; set; }
        public int? WallMaterialId { get; set; }
        public int? RoofMaterialId { get; set; }
        public string RoofMaterialOther { get; set; }
        public int? FloorMaterialId { get; set; }
        public string FloorMaterialOther { get; set; }
        public int? HHToiletId { get; set; }
        public string HHToiletOther { get; set; }
        public int? DrinkWaterMainId { get; set; }
        public string DrinkWaterOther { get; set; }
        public int? LightFuelId { get; set; }
        public string LightfuelOther { get; set; }

        public int? CookFuelId { get; set; }
        public string CookFuelOther { get; set; }
        public int? RefrigeratorOwnedId { get; set; }
        public int? MotorcycleOwnedId { get; set; }
        public int? BicycleOwnedId { get; set; }
        public int? TVOwnedId { get; set; }
        public int ZebuCattleOwned { get; set; }
        public int ExoticCattleOwned { get; set; }

        public int SheepOwned { get; set; }

        public int ShoatsOwned => SheepOwned;
        public int CamelsOwned { get; set; }
        public int DonkeysOwned { get; set; }
        public DateTime EntryDate => DateTime.UtcNow;
        public string UserCode { get; set; }
        public int? DwellingRiskId { get; set; }
        public int? CarOwnedId { get; set; }
        public int? TuktukOwnedId { get; set; }
        public int GoatsOwned { get; set; }
        public int? TenureId { get; set; }
        public int? MarkForDownload { get; set; } = 0;
        public string RegisteredBy { get; set; }
        public int? MobileOwnedId { get; set; }
        public int ChickenOwned { get; set; }
        public int PigsOwned { get; set; }
        public int? HouseholdConditionId { get; set; }
        



        public int? SubLocationId { get; set; }

    }

    public class HouseholdNSNPProgramme
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string HouseholdId { get; set; }
        public int ProgrammeId { get; set; }

    }
    public class MemberDisability
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string MemberId { get; set; }
        public int DisabilityId { get; set; }

    }

    public class Update
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string HouseholdId { get; set; }
        public bool IsComplete { get; set; }
    }
}