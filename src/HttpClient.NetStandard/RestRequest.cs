using System.Collections.Generic;

namespace TotalJobsGroup.HttpClient.NetStandard
{
    public class RestRequest : IRestRequest
    {
        private readonly List<Parameter> _parameters = new List<Parameter>();

        public RestRequest()
        {
        }
        public RestRequest(string resource)
        {
            Resource = resource;
        }

        public void AddParameter(string name, object value)
        {
            var parameter = new Parameter() { Name = name, Value = value };
            _parameters.Add(parameter);
        }

        public IEnumerable<Parameter> Parameters => _parameters;
        public HttpMethod Method { get; set; }
        public string Resource { get; set; }
    }
}