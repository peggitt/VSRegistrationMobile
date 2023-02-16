using System.Net.Http;

namespace HSNP.Interface
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}