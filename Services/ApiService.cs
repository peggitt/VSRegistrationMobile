using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;
using HSNP.Constants;
using HSNP.Interfaces;
using HSNP.Models;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace HSNP.Services
{
    public class ApiService : IApi
    {
        private static ApiService _instance;
        public static ApiService Instance = _instance ?? (_instance = new ApiService());
        private readonly IApi _api;

        public ApiService(string baseUrl=null)
        {
            var url = baseUrl == null? AppConstants.BaseApiAddress:baseUrl;

            var regex = new Regex(Regex.Escape("o"));
            var newText = regex.Replace("Hello World", "Foo", 1);

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
        public async Task<SettingsVm> DownloadSettings(StringContent vm, string token)
        {
            return await _api.DownloadSettings(vm, token);
        }
        
       
        public async Task<ConstituencyVm> GetConstituencies(StringContent vm, string token)
        {
            return await _api.GetConstituencies(vm, token);
        }
        public async Task<VillageVm> GetVillages(StringContent vm, string token)
        {
            return await _api.GetVillages(vm, token);
        }
        public async Task<SubLocationVm> GetSubLocations(StringContent vm, string token)
        {
            return await _api.GetSubLocations(vm, token);
        }
        public Task<AccountResponse> ForgotPasswordAsync(ForgotPinVm vm, string token)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApiStatus> ResetPasswordsEnumerator(ResetPasswordVm vm, string token)
        {
            throw new System.NotImplementedException();
        }
       
       
        public Task<string> LogoutAsync([Header("Authorization")] string token)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiStatus> AddHousehold(StringContent model, [Header("Authorization")] string token)
        {
            return await _api.AddHousehold(model, token);
        }
        public async Task<ApiStatus> AddHHCharacteristicscreate(StringContent model, [Header("Authorization")] string token)
        {
            return await _api.AddHHCharacteristicscreate(model, token);
        }
        public async Task<ApiStatus> AddHouseholdMembers(StringContent model, [Header("Authorization")] string token)
        {
            return await _api.AddHouseholdMembers(model, token);
        }
    }
}
