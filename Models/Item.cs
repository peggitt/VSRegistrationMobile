using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using SQLite;

namespace HSNP.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
    public class ApiStatus
    {
        public string Message { get; set; }
        public int? StatusId { get; set; }
        public int? Id { get; set; }
        public bool ResultStatus { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
     
    }
    public class ResetPasswordVm
    {
        public string UserId { get; set; }
        public string OldPin { get; set; }
        public string NewPin { get; set; }
    }
    public class ForgotPinVm
    {
        public string NationalIdNo { get; set; }
        public int EmailAddress { get; set; }
    }
    public class LoginVm:ApiStatus
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public int AccessType { get; set; }
        public string hsnp_key { get; set; }
        public string detail { get; set; }
    }
   
    public class Subscription
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Maximum { get; set; }
        public int Amount { get; set; }
    }
    public class AccountResponse
    {
        [JsonProperty("token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("tokenExpiryHours")]
        public int ExpiresIn { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }



        [JsonProperty("tokenIssuedDate")]
        public string IssuedAt { get; set; }

        [JsonProperty("tokenExpiryDate")]
        public string ExpiresAt { get; set; }

        public string Error { get; set; }

        public string ErrorDescription { get; set; }
        public bool Success { get; set; }
    }

    public class SystemCode
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Details { get; set; }
    }
    public class SystemCodeDetail
    {
        [PrimaryKey]
        public string IdComboCode { get; set; }
        public int Id { get; set; }
        public string Details { get; set; }
        public string ComboCode { get; set; }
        public string Description => Details;
        public bool IsSelected { get; set; }

    }

    public class SettingsVm
    {
        public int status { get; set; }
        public string detail { get; set; }
        public string ReturnPKey { get; set; }
        public List<SystemCodeDetail> returnDetails { get; set; }
    }
   
    public class ConstituencyVm
    {
        public int status { get; set; }
        public string detail { get; set; }
        public string ReturnPKey { get; set; }
        public List<Constituency> returnDetails { get; set; }
    }
    public class SubLocationVm
    {
        public int status { get; set; }
        public string detail { get; set; }
        public string ReturnPKey { get; set; }
        public List<SubLocation> returnDetails { get; set; }
    }
    public class VillageVm
    {
        public int status { get; set; }
        public string detail { get; set; }
        public string ReturnPKey { get; set; }
        public List<Village> returnDetails { get; set; }
    }
    public class SelectableItemWrapper<T>
    {
        public bool IsSelected { get; set; }
        public T Item { get; set; }
    }
}