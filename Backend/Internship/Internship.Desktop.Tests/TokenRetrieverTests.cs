using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Internship.Desktop.Tests
{
    [TestFixture]
    class TokenRetrieverTests
    {
        private TestableTokenRetriever sut;

        [SetUp]
        public void SetUp()
        {
            sut = TestableTokenRetriever.CreateInstance();
        }

        [Test]
        public void RetrieveToken_ShouldReturnTokenWhenLoginValid()
        {
            //Arrange
            var username = Guid.NewGuid().ToString();
            var password = Guid.NewGuid().ToString();
            var uri = "http://localhost:57280/Token";

            //Act
            var token = sut.RetrieveToken(username, password, uri) as Task<string>;

            //Assert
            Assert.That(token, Is.Not.Null);
        }

        [Test]
        public void SetUpClient_ShouldReturnValidHttpClient()
        {
            //Arrange
            var uri = "http://localhost:57280/Token";

            //Act
            var client = sut.SetUpClient(uri) as HttpClient;

            //Assert
            Assert.That(client, Is.Not.Null);
            Assert.That(client.BaseAddress.AbsoluteUri, Is.EqualTo(uri));
            Assert.That(client.DefaultRequestHeaders.Accept.Where(m => m.MediaType.Equals("application/json")),Is.Not.Null);
        }

        private class TestableTokenRetriever : TokenRetriever
        {
            private TestableTokenRetriever() { }

            public static TestableTokenRetriever CreateInstance()
            {
                return new TestableTokenRetriever();
            }
        }
    }
}
