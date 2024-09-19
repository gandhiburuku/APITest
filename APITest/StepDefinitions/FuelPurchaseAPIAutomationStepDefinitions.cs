using System;
using System.Linq;
using System.Text.Json;
using APITest.Utils;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFlowProject.Helpers;
using TechTalk.SpecFlow;

namespace SpecFlowProject.StepDefinitions
{
    [Binding]
    public class FuelPurchaseStepDefinitions
    {
        private RestClient _client;
        private RestResponse _response;
        private readonly ScenarioContext _scenarioContext;
        private readonly ApiHelper apiHelper;
        public FuelPurchaseStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            apiHelper = new ApiHelper("https://qacandidatetest.ensek.io");
        }

        [Given(@"the test data is reset")]
        public void GivenTheTestDataIsReset()
        {
            _response = apiHelper.ResetData();
            Assert.AreEqual(401, (int)_response.StatusCode);
        }

        [When(@"I buy (.*) units of ""([^""]*)""")]
        public void WhenIBuyUnitsOfFuel(int quantity, string fuelType)
        {
            int fuelCode = 3;
            if (fuelType != "Electricity")
            {
                fuelCode = 4;
            }
            _response = apiHelper.BuyFuel(quantity, fuelCode);

            Assert.AreEqual(200, (int)_response.StatusCode, $"Failed to purchase {fuelType}");
            _scenarioContext.Add(fuelType, quantity);
        }

        [Then(@"I should count the orders which were placed before system current date")]
        public void ThenIShouldCountTheOrdersWhichWerePlacedBeforeSystemCurrentDate()
        {
            int ordercount = 0;
            _response = apiHelper.GetOrders();
            var orders = JsonConvert.DeserializeObject<dynamic>(_response.Content!);

            foreach (var order in orders!)
            {
                var orderdate = (string)order.time;
                ordercount += FilterTheOrder.CountTheOrders(orderdate);
            }
            Assert.AreNotEqual(0, ordercount);
        }
    }
}
