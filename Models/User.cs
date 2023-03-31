using SQLite;
using System;

namespace HSNP.Models
{
    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public Boolean IsLoggedIn { get; set; }
        public int CountyId { get; set; }
    }

    public class Role
    {
        public string Id { get; set; }
        public string ClientId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
    }

    public class BearerLogin
    {
        public string username { get; set; }
        public string password { get; set; }
        public string grant_type { get; set; }
    }
    public class BaseVm
    {
        public string UserName { get; set; }
    }

}