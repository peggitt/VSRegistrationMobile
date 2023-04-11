using SQLite;
using System.Collections.Generic;

namespace HSNP.Models
{
   
   
    public class Setting
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }

    public static class SettingsKey
	{
        public static string LastLoginUserId => "LastLoginUserId";
        public static string SubLocationId => "SubLocationId";
        public static string Username => "Username";
        public static string PasswordHash => "PasswordHash";
        public static string Token => "Token";
        public static string TokenExpiry => "TokenExpiry";
    }

   

    /*public class StatisticsVm
    {
        public int ListedHouseholdsNo { get; set; }

        public int ListedApprovedHouseholdsNo { get; set; }

        public int ListedRejectedHouseholdsNo { get; set; }

        public int RegisteredHouseholdsNo { get; set; }

        public int RegisteredApprovedHouseholdsNo { get; set; }

        public int RegisteredRejectedHouseholdsNo { get; set; }
    }*/

    public class Statistics
    {
        public int Id { get; set; }

        public string Title { get; set; }

        [Ignore]
        public List<StatisticDetail> Details { get; set; }
    }

    public class StatisticDetail
    {
        public int Id { get; set; }
        public int? StatisticsId { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}