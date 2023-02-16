using System;
using SQLite;

namespace HSNP.Models
{
    public class Visitor
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string IdNumber { get; set; }
        public string PhoneNo { get; set; }
        public string Institution { get; set; }
        public string RegNo { get; set; }
        public int? VisitorTypeId { get; set; }
        public int? CarModelId { get; set; }   
        public string CarModel { get; set; }
        public bool IsDriver => VisitorTypeId ==1;

    }
    public class Visit
    {
        [PrimaryKey]
        public Guid Id { get; set; }
        public Guid VisitorId { get; set; }
       // public Visitor Visitor { get; set; }
        public string RegNo { get; set; }
        public Guid DestinationId { get; set; }
        public bool IsCheckedIn { get; set; } = true;
        // public Destination Destination { get; set; }
        public DateTime? CheckIn { get; set; }
        public DateTime? CheckOut { get; set; }
        public string Name  { get; set; }
        public string VisitReason { get; set; }
        public int? BadgeId { get; set; }
        public string BadgeNo { get; set; }

        public int? TagId { get; set; }
        public string TagNo { get; set; }

        public bool IsDriver { get; set; }
        public int? CarModelId { get; set; }
        public int? TypeId { get; set; } // 1. Gate, 2. Reception
    }
    public class Destination
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid BuildingId { get; set; }
       // public Building Building { get; set; }
    }
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    public class Badge 
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string BadgeNo { get; set; }
        public Guid BuildingId { get; set; }
        public bool IsTaken { get; set; }

    }
    public class VisitorTag
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string TagNo { get; set; }
        public Guid BuildingId { get; set; }
        public bool IsTaken { get; set; }

    }
}