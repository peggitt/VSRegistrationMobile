﻿using SQLite;

namespace HSNP.Models
{
    public class Household
    {
        [PrimaryKey]
        public string HouseholdId { get; set; }
        public string ApplicantId { get; set; }
        public string UserName { get; set; }
        public string VillageId { get; set; }
        public string EntryDate { get; set; }
        public int? IsBeneficiaryHHId { get; set; }
        public string BeneficiaryGroup { get; set; }
        public int? HHReceivingNSNPBenefictsId { get; set; } /*New*/
        public int? CTOVC { get; set; }
        public int? HSNP { get; set; }
        public int? OPCT { get; set; }
        public int? PWSDCT { get; set; }
        public int? UFSCT { get; set; }
        public string OtherCT { get; set; }
        public string HouseholdAddress { get; set; }
        public string Neighbourhood { get; set; }
        public string NearestChurchMqs { get; set; }
        public string NearestSchool { get; set; }
        public int? RuralUrbanId { get; set; }
        public int? Longitude { get; set; }
        public int? Latitude { get; set; }
        public int? LiveBirths { get; set; }
        public int? StillBirths { get; set; }
        public int? OtherBenefitsAmout { get; set; }
        public int? HouseholdCutMealId { get; set; }

        public int? HHReceivingBenefictsId { get; set; }
        public int? BenefitTypeId { get; set; }
        public int? DataLevelID { get; set; }
        public string BenefitKind { get; set; }
        public string ProgrammeName { get; set; }
        public string OtherBenefits { get; set; }
        public string Comments { get; set; }
        public string RegisteredBy { get; set; }
        public string RegisteredOn { get; set; }
        public string ApproverName { get; set; }
        public string ApprovedOn { get; set; }
        public int? MarkForDownload { get; set; }
        public string HHIDText { get; set; }
        public string PotentialDuplicate { get; set; }



        public int? AreaTypeId { get; set; }

        public DateTime CreatedOn { get; set; }
        public int? HouseholdMembers { get; set; }

        public bool IsComplete { get; set; }
        public bool Uploaded { get; set; }
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
        public DateTime CreatedOn { get; set; }
        public string Name => $"{FirstName} {MiddleName} {Surname}";


        public string Username { get; set; }

        public string HhmemberRosterID => Id;


        public int? RelationshipId { get; set; }
        public int? SexId { get; set; }
        public string BirthCertificate { get; set; }
        public int? YearOfBirth { get; set; }
        public int? FatherAliveId { get; set; }
        public int? MotherAliveId { get; set; }
        public int? HighestGradeCodeId { get; set; }
        public int? WorkLast7daysId { get; set; }
        public int? ChronicIllnessId { get; set; }
        public int? ChronicIllYrs { get; set; }
        public int? ChronicIllMths { get; set; }
        public bool VisualDisability { get; set; }
        public bool HearingDisability { get; set; }
        public bool SpeechDisability { get; set; }
        public bool MentalDisability { get; set; }
        public bool SelfcareDisability { get; set; }
        public bool Need24HrCare { get; set; }
        public int? Need24HrCareId { get; set; }
        public string EntryDate { get; set; }
        public string Idno { get; set; }
        public string RetypedIdno { get; set; }
        public bool IDIprsvalid { get; set; }
        public string Usercode { get; set; }
        public bool IsNominatedAccHoloder { get; set; }
        public int? MaritalStatusId { get; set; }
        public string SpouseId { get; set; }
        public string MobileNo { get; set; }
        public string CaregiverId { get; set; }
        public int? DisabilityId { get; set; }
        public int? LearningInstitutionId { get; set; }
        public int? MonthEarnings { get; set; }
        public int? SpouseStatusId { get; set; }
        public bool MarkForDownload { get; set; }
        public string RegisteredBy { get; set; }
        //(3.17) Does Member's work on a formal job, teaching, public sector, NGO/FBO?
        public int? WorkingInFormalJobId { get; set; }
        public string Working { get; set; }
        public string Serialno { get; set; }
        public string WaitingCardNo { get; set; }
        public string PspaccountId { get; set; }


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
        public int? DrinkWaterId { get; set; }
        public string DrinkWaterOther { get; set; }
        public int? LightfuelId { get; set; }
        public string LightfuelOther { get; set; }
        public int? CookFuelId { get; set; }
        public string CookFuelOther { get; set; }
        public int? RefrigeratorOwnedId { get; set; }
        public int? MotorCycleOwnedId { get; set; }
        public int? BicycleOwnedId { get; set; }
        public int? TVOwnedId { get; set; }
        public int? ZebuCattleOwned { get; set; }
        public int? ExoticCattleOwned { get; set; }
        public int? ShoatsOwned { get; set; }
        public int? CamelsOwned { get; set; }
        public int? DonkeysOwned { get; set; }
        public string EntryDate { get; set; }
        public string UserCode { get; set; }
        public int? DwellingRiskId { get; set; }
        public int? CarOwnedId { get; set; }
        public int? TuktukOwnedId { get; set; }
        public int? GoatsOwned { get; set; }
        public int? Tenure { get; set; }
        public int? MarkForDownload { get; set; }
        public string RegisteredBy { get; set; }
        public int? MobileOwnedId { get; set; }
        public int? ChickenOwned { get; set; }
        public int? PigsOwned { get; set; }



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