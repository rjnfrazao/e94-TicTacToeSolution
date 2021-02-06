using Microsoft.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestClientSDKLibrary;
using System;

namespace FunctionalTest
{
    [TestClass]
    public class FunctionalTests
    {
        [TestMethod]
        public void TestMethodWinner()
        {
            // Arrange
            ServiceClientCredentials serviceClientCredentials = new TokenCredentials("FakeTokenValue");

            RestClientSDKLibraryClient client = new RestClientSDKLibraryClient(
                new Uri("http://localhost: 18778"), serviceClientCredentials);

            // Act Make the HTTP POST
            // IList<WeatherForecast> results = client.Post();

            //Assert Verify exactly 5 are returned
            Assert.AreEqual(5, results.Count);
        }
    }
}
