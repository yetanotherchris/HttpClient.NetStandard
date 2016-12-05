using System.Threading.Tasks;

namespace TotalJobsGroup.HttpClient.NetStandard
{
    public interface IHttpClient
    {
        Task<IRestResponse> ExecuteAsync(IRestRequest request);
    }
}
