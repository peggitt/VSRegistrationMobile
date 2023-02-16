using System;
using System.Collections.Generic;

namespace HSNP.Models
{
    public class RegisterVisitorVm:ApiStatus
    {
        public Visitor Visitor { get; set; }
     
    }
    public class SearchVm : ApiStatus
    {
        public string IdNumber { get; set; }
        public string PhoneNo { get; set; }
        public string RegNo { get; set; }
        public Visitor Visitor { get; set; }

    }
    public class CheckedInVm : ApiStatus
    {
        public Guid ClientId { get; set; }
        public string UserId { get; set; }
        public Guid BuildingId { get; set; }
        public List<Visit> Visits { get; set; }
        public int? TypeId { get; set; }
    }
}