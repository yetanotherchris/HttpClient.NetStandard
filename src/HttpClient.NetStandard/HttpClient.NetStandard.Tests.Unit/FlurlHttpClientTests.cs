using Flurl.Http.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using TotalJobsGroup.HttpClient.NetStandard.Flurl;

namespace TotalJobsGroup.HttpClient.NetStandard.Tests.Unit
{
    [TestFixture]
    public class FlurlHttpClientTests
    {
        private FlurlHttpClient _sut;        

        [SetUp]
        public void Setup()
        {
            _sut=new FlurlHttpClient("http://test.com");
        }
        [Test]
        public async Task ExecuteGetWithoutParameters()
        {
            const string output = "HTTP content";

            using (var httpTest = new HttpTest())
            {
                httpTest.RespondWith(output);

                var request = new RestRequest {Method = HttpMethod.Get};

                IRestResponse response = await _sut.ExecuteAsync(request);

                Assert.That(response.Content,Is.EqualTo(output));
            }
        }

        [Test]
        public async Task ExecuteGetWithParameter()
        {
            using (var httpTest = new HttpTest())
            {
                var request = new RestRequest {Method = HttpMethod.Get};
                request.AddParameter("Par1", 54);
                
                await _sut.ExecuteAsync(request);
                httpTest.ShouldHaveCalled("http://test.com/?Par1=54");
            }
        }

        [Test]
        public async Task ExecuteGetWithResourceName()
        {
            using (var httpTest = new HttpTest())
            {
                var request = new RestRequest("/Data/Articles") {Method = HttpMethod.Get};

                await _sut.ExecuteAsync(request);
                Assert.That(httpTest.CallLog[0].Url.Path,Is.EqualTo("http://test.com/Data/Articles"));
            }
        }

        [Test]
        public async Task ExecutePostWithParameters()
        {
            using (var httpTest = new HttpTest())
            {
                var request = new RestRequest {Method = HttpMethod.Post};
                request.AddParameter("Par1", 54);
                request.AddParameter("Par2", 254);

                await _sut.ExecuteAsync(request);
                httpTest.ShouldHaveCalled("http://test.com");
                Assert.That(httpTest.CallLog[0].RequestBody,Is.EqualTo("Par1=54&Par2=254"));
            }
        }

        [Test]
        public async Task ExecutePostWithResource()
        {
            using (var httpTest = new HttpTest())
            {
                var request = new RestRequest("/Data/Articles");
                request.Method = HttpMethod.Post;
                
                await _sut.ExecuteAsync(request);
                Assert.That(httpTest.CallLog[0].Url.ToString(), Is.EqualTo("http://test.com/Data/Articles"));
            }
        }

        [Test]
        public async Task Should_EncodeGetQueryParameters()
        {
            using (var httpTest = new HttpTest())
            {
                var request = new RestRequest();
                request.AddParameter("Par1", "a 'a");
                request.Method = HttpMethod.Get;
                
                await _sut.ExecuteAsync(request); 
                Assert.That(httpTest.CallLog[0].Url.ToString(), Is.EqualTo("http://test.com/?Par1=a%20%27a"));
            }
        }


        [Test]
        public async Task Should_EncodePostQueryParameters()
        {
            using (var httpTest = new HttpTest())
            {
                var request = new RestRequest();
                request.AddParameter("Par1", "a 'a");
                request.Method = HttpMethod.Post;

                await _sut.ExecuteAsync(request);
                Assert.That(httpTest.CallLog[0].RequestBody, Is.EqualTo("Par1=a+%27a"));
            }
        }
    }
}
