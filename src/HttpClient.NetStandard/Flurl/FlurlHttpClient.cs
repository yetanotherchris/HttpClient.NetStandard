using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TotalJobsGroup.HttpClient.NetStandard.Flurl
{
    public class FlurlHttpClient : IHttpClient
    {
        private readonly string _baseUrl;

        public FlurlHttpClient(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        public async Task<IRestResponse> ExecuteAsync(IRestRequest request)
        {
            var url = new Url(_baseUrl);

            if (!string.IsNullOrEmpty(request.Resource))
                url.AppendPathSegment(request.Resource);

            if (request.Method == HttpMethod.Get)
                return await ExecuteGet(request, url);
            if (request.Method == HttpMethod.Post)
                return await ExecutePost(request, url);

            throw new NotSupportedException();
        }

        private static async Task<IRestResponse> ExecutePost(IRestRequest request, Url url)
        {
            var arguments = new Dictionary<string, object>();

            foreach (var parameter in request.Parameters)
                arguments[parameter.Name] = parameter.Value;

            HttpResponseMessage flurlResponse = await url.PostUrlEncodedAsync(arguments);
            var response = new RestResponse { Content = await flurlResponse.Content.ReadAsStringAsync() };

            return response;
        }

        private static async Task<IRestResponse> ExecuteGet(IRestRequest request, Url url)
        {
            foreach (var parameter in request.Parameters)
                url.SetQueryParam(parameter.Name, parameter.Value);

            HttpResponseMessage flurlResponse = await url.GetAsync();
            var response = new RestResponse { Content = await flurlResponse.Content.ReadAsStringAsync() };

            return response;
        }
    }
}