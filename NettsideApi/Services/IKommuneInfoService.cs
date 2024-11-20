using NettsideApi.API_Models;

namespace NettsideApi.Services
{
    public interface IKommuneInfoService
    {
        Task<KommuneInfo> GetKommuneInfoAsync(string kommuneNr);
    }
}
