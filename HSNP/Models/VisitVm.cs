namespace HSNP.Models
{
    public class VisitVm:ApiStatus
    {
        public string EnumeratorId { get; set; }
        public Visit Visit { get; set; }
       
    }

}