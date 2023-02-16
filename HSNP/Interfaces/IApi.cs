using HSNP.Models;
using Refit;
using System.Threading.Tasks;
using HSNP.Database;
using HSNP.ViewModels;
using System.Net.Http;

namespace HSNP.Interface
{
    public interface IApi
    {
        [Post("/userlogin")]
        Task<LoginVm> Init([Body]LoginVm model);

        [Post("/userlogin")]
        Task<LoginVm> Login(StringContent model);

       
        [Post("/api/Account/ForgotPassword/")]
        Task<AccountResponse> ForgotPasswordAsync(ForgotPinVm vm, [Header("Authorization")] string token);


        [Post("/ResetPassword")]
        Task<ApiStatus> ResetPasswordsEnumerator([Body] ResetPasswordVm vm, [Header("Authorization")] string token);
 

        [Post("/api/Account/Logout/")]
        Task<string> LogoutAsync([Header("Authorization")] string token);

        [Post("/api/Account/Register/")]
        Task<RegisterVisitorVm> Register(RegisterVisitorVm vm);

        [Post("/api/account/search/")]
        Task<SearchVm> Search(SearchVm vm);

        [Post("/api/Account/CheckIn/")]
        Task<VisitVm> CheckIn(VisitVm vm);

        [Post("/api/Account/CheckOut/")]
        Task<VisitVm> CheckOut(VisitVm vm);

        [Post("/api/account/CheckedIn/")]
        Task<CheckedInVm> CheckedIn(CheckedInVm vm);
    }
}