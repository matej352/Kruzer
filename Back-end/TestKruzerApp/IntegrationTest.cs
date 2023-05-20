using FluentAssertions;
using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using NUnit.Framework;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KruzerApp.Models;

namespace TestKruzerApp
{

    public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Xunit.Theory]
        [InlineData("/api/Putnik/GetAll")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }
    }
}
