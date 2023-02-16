using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using HSNP.Constants;
using HSNP.Interface;
using HSNP.Models;
using System.Net.Http;

namespace HSNP.Services
{
    public class ApiService : IApi
    {
        private static ApiService _instance;
        public static ApiService Instance = _instance ?? (_instance = new ApiService());
        private readonly IApi _api;

        private ApiService()
        {
            var url = AppConstants.BaseApiAddress;
           // _api = RestService.For<IApi>(url);
            _api = RestService.For<IApi>(url,
                new RefitSettings
                {
                    ContentSerializer = new NewtonsoftJsonContentSerializer(
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver(),
                            DateTimeZoneHandling = DateTimeZoneHandling.Local
                        })
                });
        }
        //public async Task<LoginResultsVm> Login(LoginVm model)
        //{
        //    return await _api.Login(model);
        //}
        //public async Task<ProductsListViewModel> GetHomePageItems(LoginVm model)
        //{
        //    return await _api.GetHomePageItems(model);
        //}

        public async Task<LoginVm> Init(LoginVm vm)
        {
            return await _api.Init(vm);
        }
        public async Task<LoginVm> Login(StringContent vm)
        {
            return await _api.Login(vm);
        }

        public Task<AccountResponse> ForgotPasswordAsync(ForgotPinVm vm, string token)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiStatus> ResetPasswordsEnumerator(ResetPasswordVm vm, string token)
        {
            throw new System.NotImplementedException();
        }

       
        async Task<string> IApi.LogoutAsync(string token)
        {
            return await _api.LogoutAsync(token);
        }
        public async Task<RegisterVisitorVm> Register(RegisterVisitorVm vm)
        {
            return await _api.Register(vm);
        }
        public async Task<SearchVm> Search(SearchVm vm)
        {
            return await _api.Search(vm);
        }

        public async Task<VisitVm> CheckIn(VisitVm vm)
        {
            return await _api.CheckIn(vm);
        }

        public async Task<VisitVm> CheckOut(VisitVm vm)
        {
            return await _api.CheckOut(vm);
        }
        public async Task<CheckedInVm> CheckedIn(CheckedInVm vm)
        {
            return await _api.CheckedIn(vm);
        }

    }
}
