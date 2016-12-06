# HttpClient.NetStandard
A HttpClient abstraction based off the contracts/interfaces from Restsharp


```csharp

var httpClient = new FlurlHttpClient("http://test.com");
var request = new RestRequest {Method = HttpMethod.Get, Resource = "/Data/Items"};
request.AddParameter("Parameter1", 54);
request.AddParameter("Parameter2", "text");
                
IRestResponse response = await httpClient.ExecuteAsync(request);

Console.WriteLine(response.Content);

```
