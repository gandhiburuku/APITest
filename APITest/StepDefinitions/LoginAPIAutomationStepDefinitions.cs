using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using SpecFlowProject.Helpers;
using System;
using TechTalk.SpecFlow;

namespace APITest.StepDefinitions
{
    [Binding]
    public class LoginAPIAutomationStepDefinitions
    {
        private RestClient _client;
        private RestResponse _response;
        private readonly ScenarioContext _scenarioContext;
        private readonly ApiHelper apiHelper;
        public LoginAPIAutomationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            apiHelper = new ApiHelper("https://qacandidatetest.ensek.io");
        }

        [When(@"I login with ""([^""]*)"" and ""([^""]*)""")]
        public void WhenILoginWithAnd(string username, string password)
        {
            _response = apiHelper.GetAccessToken(username, password);
            var tokenresponse = JsonConvert.DeserializeObject<dynamic>(_response.Content!);
            var token = (string)tokenresponse.access_token;
            var message = (string)tokenresponse.message;

            Assert.IsNotNull(_response);
            Assert.AreEqual(200, (int)_response.StatusCode, $"Failed to perform login with {username}");
            Assert.AreEqual("Success", message, $"Failed to perform login with {username}");

            _scenarioContext.Add("access_token", token);
        }

        [Then(@"I should get valid access token")]
        public void ThenIShouldGetValidAccessToken()
        {
            Assert.IsNotNull(_scenarioContext["access_token"]);
        }
    }
}
