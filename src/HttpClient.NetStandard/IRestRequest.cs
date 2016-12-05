using System.Collections.Generic;

namespace TotalJobsGroup.HttpClient.NetStandard
{
    public interface IRestRequest
    {
        void AddParameter(string name, object value);
        IEnumerable<Parameter> Parameters { get; }
        HttpMethod Method { get; }
        string Resource { get; }
    }
}