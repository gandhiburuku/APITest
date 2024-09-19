using FluentAssertions.Equivalency;
using RestSharp;

namespace SpecFlowProject.Helpers
{
    public class ApiHelper
    {
        private readonly RestClient _client;
        private string _url;

        public ApiHelper(string url)
        {
            _url = url;
            _client = new RestClient(_url);

            //_client = new RestClient("https://qacandidatetest.ensek.io");
        }

        public RestResponse ResetData()
        {
            var request = new RestRequest("/ENSEK/reset", Method.Post);
            return _client.Execute(request);
        }
        public RestResponse GetAccessToken(string username, string password)
        {
            var request = new RestRequest("ENSEK/login", Method.Post);
            request.AddJsonBody(new { username = username, password = password });
            return _client.Execute(request);
        }

        public RestResponse BuyFuel( int quantity, int fuelId)
        {
            var request = new RestRequest($"/ENSEK/buy/{fuelId}/{quantity}", Method.Put);
            return _client.Execute(request);
        }

        public RestResponse GetOrders()
        {
            var request = new RestRequest("ENSEK/orders", Method.Get);
            return _client.Execute(request);
        }
    }
}
