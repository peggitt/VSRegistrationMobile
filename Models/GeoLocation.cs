using SQLite;
using System.Collections.Generic;

namespace HSNP.Models
{
    public class County
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class Constituency
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int CountyId { get; set; }
    }
    public class SubLocation
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ConstituencyId { get; set; }
    }
    public class Village
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public int SubLocationId { get; set; }
    }
}