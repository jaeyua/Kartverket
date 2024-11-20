using NettsideApi.API_Models;

namespace NettsideApi.Services
{
    public interface IStedsnavnService
    {
        Task<StedsnavnResponse> GetStedsnavnAsync(string search);
    }
}
