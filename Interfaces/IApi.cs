using HSNP.Models;
using Refit;
using System.Threading.Tasks;
using HSNP.Database;
using HSNP.ViewModels;
using System.Net.Http;

namespace HSNP.Interfaces
{
    public interface IApi
    {
        [Post("/userlogin")]
        Task<LoginVm> Init([Body] LoginVm model);

        [Post("/userlogin")]
        Task<LoginVm> Login(StringContent model);

        [Post("/getallcombocodes")]
        Task<SettingsVm> DownloadSettings([Body] StringContent model, [Header("Authorization")] string token);

        [Post("/getconstituency")]
        Task<ConstituencyVm> GetConstituencies([Body] StringContent model, [Header("Authorization")] string token);

        [Post("/getallvillages")]
        Task<VillageVm> GetVillages([Body] StringContent model, [Header("Authorization")] string token);

        [Post("/getallsublocations")]
        Task<SubLocationVm> GetSubLocations([Body] StringContent model, [Header("Authorization")] string token);

        [Post("/api/Account/ForgotPassword/")]
        Task<AccountResponse> ForgotPasswordAsync(ForgotPinVm vm, [Header("Authorization")] string token);


        [Post("/ResetPassword")]
        Task<ApiStatus> ResetPasswordsEnumerator([Body] ResetPasswordVm vm, [Header("Authorization")] string token);


        [Post("/api/Account/Logout/")]
        Task<string> LogoutAsync([Header("Authorization")] string token);


        [Post("/householdmobilecreate")]
        Task<ApiStatus> AddHousehold(StringContent model, [Header("Authorization")] string token);
        [Post("/HHCharacteristicsmobilecreate")]
        Task<ApiStatus> AddHHCharacteristicscreate(StringContent model, [Header("Authorization")] string token);

        [Post("/membermobilecreate")]
        Task<ApiStatus> AddHouseholdMembers(StringContent model, [Header("Authorization")] string token);

        [Post("/householdviewdownload")]
        Task<HouseholdsDownloadVm> DownloadHouseholds(StringContent model, [Header("Authorization")] string token);

        [Post("/HouseholdViewDetailsdownload")]
        Task<HouseholdsDetailsDownloadVm> DownloadHouseholdsDetails(StringContent model, [Header("Authorization")] string token);
        

        [Post("/memberviewdownload")]
        Task<MembersDownloadVm> DownloadMembers(StringContent model, [Header("Authorization")] string token);


        [Post("/householdmobileupdate")]
        Task<ApiStatus> UpdateHousehold(StringContent model, [Header("Authorization")] string token);
        [Post("/HHCharacteristicsmobileupdate")]
        Task<ApiStatus> UpdateHHCharacteristicscreate(StringContent model, [Header("Authorization")] string token);

        [Post("/membermobileupdate")]
        Task<ApiStatus> UpdateHouseholdMembers(StringContent model, [Header("Authorization")] string token);
    }
}