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
    }
}