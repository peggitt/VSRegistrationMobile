using SQLite;

namespace HSNP.Models
{
    public class Household
    {
        [PrimaryKey]
        public string HouseholdId { get; set; }
        public string UserName { get; set; }
        public string Village_ID { get; set; }
        public string Entry_Date { get; set; }
        public int IsBeneficiaryHH { get; set; }
        public string BeneficiaryGroup { get; set; }
        public int CTOVC { get; set; }
        public int HSNP { get; set; }
        public int OPCT { get; set; }
        public int PWSDCT { get; set; }
        public int UFSCT { get; set; }
        public string OtherCT { get; set; }
        public string HouseholdAddress { get; set; }
        public string Neighbourhood { get; set; }
        public string NearestChurchMqs { get; set; }
        public string NearestSchool { get; set; }
        public int RuralUrbanId { get; set; }
        public int Longitude { get; set; }
        public int Latitude { get; set; }
        public int LiveBirths { get; set; }
        public int StillBirths { get; set; }
        public int OtherBenefitsAmout { get; set; }
        public int Household_CutMeal { get; set; }
        public int HHReceivingBeneficts { get; set; }
        public int BenefitType { get; set; }
        public int DataLevelID { get; set; }
        public string Benefit_Kind { get; set; }
        public string ProgrammeName { get; set; }
        public string OtherBenefits { get; set; }
        public string Comments { get; set; }
        public string RegisteredBy { get; set; }
        public string RegisteredOn { get; set; }
        public string ApproverName { get; set; }
        public string ApprovedOn { get; set; }
        public int MarkForDownload { get; set; }
        public string HHIDText { get; set; }
        public string PotentialDuplicate { get; set; }
    }

}