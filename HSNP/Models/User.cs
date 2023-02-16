using SQLite;
using System;

namespace HSNP.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string hsnp_key { get; set; }
        public Boolean IsLoggedIn { get; set; }     
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
}