using Microsoft.AspNetCore.Http;
using Microsoft.Rest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestClientSdkLibrary;
using RestClientSdkLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FunctionalTest
{
    [TestClass]
    public class FunctionalTests
    {

        // DEMO: Local testing
        const string EndpointUrlString = "https://localhost:44376/";

        // DEMO: Testing against Azure instance
        //const string EndpointUrlString = "https://weatherforecast-cscie94.azurewebsites.net";

        ServiceClientCredentials _serviceClientCredentials;

        RestClientSdkLibraryClient _client;

        TicTacToeMoveDto _payload;

        object _resultObject;

        HttpOperationResponse<object> _resultHttpObject;

        TicTacToeMoveResultDto _resultPayload;

        [TestInitialize]
        public void Initialize()
        {
            _serviceClientCredentials = new TokenCredentials("FakeTokenValue");

            _client = new RestClientSdkLibraryClient(
                new Uri(EndpointUrlString), _serviceClientCredentials);
        }

        // *********************************
        // ** TEST Winner X
        // *********************************
        [TestMethod]
        public async Task TestMethodWinnerX()
        {
            // ** Player X Winner
            // Arrange 
            _payload = new TicTacToeMoveDto()
            {
                Move = 3,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() {
                "?",
                "X",
                "X",
                "O",
                "?",
                "O",
                "?",
                "?",
                "?"
                }               
            };

            // Act
            _resultObject = await _client.ExecuteMoveAsync(_payload);
            _resultPayload = _resultObject as TicTacToeMoveResultDto;

            // Assert
            if (_resultPayload != null)
            {
                Assert.IsTrue(_resultPayload.Winner.Equals("X")); // Contains(payload.MessageContent)); ;
            }
            else
            {
                Assert.Fail("Expected a TicTacToeMoveResultDto but didn't recieve one");
            }
        }

        // *********************************
        // ** TEST Winner O
        // *********************************
        [TestMethod]
        public async Task TestMethodWinnerO()
        {
            // ** Player O Winner
            // Arrange 
            _payload = new TicTacToeMoveDto()
            {
                Move = 2,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() {
                "?",
                "X",
                "X",
                "X",
                "O",
                "O",
                "X",
                "?",
                "O"
                }
            };

            // Act
            _resultObject = await _client.ExecuteMoveAsync(_payload);
            _resultPayload = _resultObject as TicTacToeMoveResultDto;

            // Assert
            if (_resultPayload != null)
            {
                Assert.IsTrue(_resultPayload.Winner.Equals("O")); 
            }
            else
            {
                Assert.Fail("Expected a TicTacToeMoveResultDto but didn't recieve one");
            }

        }


        // *********************************
        // ** TEST Tie
        // *********************************
        [TestMethod]
        public async Task TestMethodTie()
        {
            // ** Match Tie
            // Arrange 
            _payload = new TicTacToeMoveDto()
            {
                Move = 4,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() {
                "O",
                "X",
                "X",
                "X",
                "O",
                "O",
                "?",
                "O",
                "X"
                }
            };

            // Act
            _resultObject = await _client.ExecuteMoveAsync(_payload);
            _resultPayload = _resultObject as TicTacToeMoveResultDto;

            // Assert
            if (_resultPayload != null)
            {
                Assert.IsTrue(_resultPayload.Winner.Equals("tie", StringComparison.CurrentCultureIgnoreCase)); 
            }
            else
            {
                Assert.Fail("Expected a TicTacToeMoveResultDto but didn't recieve one");
            }

        }

        // *********************************
        // ** TEST Inconclusive
        // *********************************
        [TestMethod]
        public async Task TestMethodInconclusive()
        {
            // ** Match Tie
            // Arrange 
            _payload = new TicTacToeMoveDto()
            {
                Move = 3,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() {
                "?",
                "?",
                "X",
                "O",
                "?",
                "O",
                "?",
                "?",
                "?"
                }
            };

            // Act
            _resultObject = await _client.ExecuteMoveAsync(_payload);
            _resultPayload = _resultObject as TicTacToeMoveResultDto;

            // Assert
            if (_resultPayload != null)
            {
                Assert.IsTrue(_resultPayload.Winner.Equals("inconclusive", StringComparison.CurrentCultureIgnoreCase));
            }
            else
            {
                Assert.Fail("Expected a TicTacToeMoveResultDto but didn't recieve one");
            }

        }

 

        // *********************************
        // ** TEST - It should fail, if players use the symbol O. 
        // *********************************
        [TestMethod]
        public async Task TestMethodErrorIfPlayersAreO()
        {
            // ** Match Tie
            // Arrange 
            _payload = new TicTacToeMoveDto()
            {
                Move = 4,
                AzurePlayerSymbol = "O",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() {
                "?",
                "?",
                "?",
                "O",
                "X",
                "?",
                "?",
                "?",
                "?"
                }
            };

            // Act
            _resultHttpObject = await _client.ExecuteMoveWithHttpMessagesAsync(_payload);
 
            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)_resultHttpObject.Response.StatusCode);    // Check status code 400.
            int? resultInt = _resultHttpObject.Body as int?; 
            Assert.IsNotNull(resultInt);                            // Check somethins was returned.
            Assert.AreEqual(102, resultInt.Value);                  // Error code expected 
            //StringAssert.Contains("ERR002", resultString);              // Disabled. Not able to desirialize to a string - Check if ERR002 was returned.
        }


        // *********************************
        // ** TEST - It should fail, if players use the symbol X. 
        // *********************************
        [TestMethod]
        public async Task TestMethodErrorIfPlayersAreX()
        {
            // ** Match Tie
            // Arrange 
            _payload = new TicTacToeMoveDto()
            {
                Move = 3,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "X",
                GameBoard = new List<string>() {
                "?",
                "?",
                "?",
                "O",
                "X",
                "?",
                "?",
                "?",
                "?"
                }
            };

            // Act
            _resultHttpObject = await _client.ExecuteMoveWithHttpMessagesAsync(_payload);

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)_resultHttpObject.Response.StatusCode);    // Check status code 400.
            int? resultInt = _resultHttpObject.Body as int?;
            Assert.IsNotNull(resultInt);                                   // Check somethins was returned.
            Assert.AreEqual(102, resultInt.Value);                         // Error code expected 
            //StringAssert.Contains("ERR002", resultString);               // Disabled. Not able to desirialize to a string - Check if ERR002 was returned.
        }



        // *********************************
        // ** TEST Invalid Symbol
        // *********************************
        [TestMethod]
        public async Task TestMethodInvalidSymbol()
        {
             // Arrange 
            _payload = new TicTacToeMoveDto()
            {
                Move = 3,
                AzurePlayerSymbol = "X",
                HumanPlayerSymbol = "O",
                GameBoard = new List<string>() {
                "?",
                "A",
                "Z",
                "Z",
                "U",
                "R",
                "E",
                "?",
                "?"
                }
            };

            // Act
            _resultHttpObject = await _client.ExecuteMoveWithHttpMessagesAsync(_payload);

            // Assert
            Assert.AreEqual(StatusCodes.Status400BadRequest, (int)_resultHttpObject.Response.StatusCode);    // Check status code 400.
            int? resultInt = _resultHttpObject.Body as int?;
            Assert.IsNotNull(resultInt);                            // Check somethins was returned.
            Assert.AreEqual(103, resultInt.Value);                  // Error code expected 

        }

    }
}
