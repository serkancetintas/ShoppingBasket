using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MongoDB.Driver;
using Newtonsoft.Json;
using ShoppingBasket.Application.Contracts.Dtos;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingBasket.Api.IntegrationTest
{
    public class BasketControllerTests: IDisposable
    {
        private readonly HttpClient _client;
        private readonly TestServer _testServer;
        public BasketControllerTests()
        {
             _testServer = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseEnvironment("Development"));
            _client = _testServer.CreateClient();
        }

        [Fact]
        public async Task AddItemToBasket_Should_Return_Ok_With_ItemCount_When_Insert_Success()
        {
            var expectedStatusCode = HttpStatusCode.OK;

            //Arrange
            var request = new AddItemDto
            {
                CatalogItemId = "H22",
                Sku = "H22XSB",
                Price = 22,
                Quantity = 2
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/v1/ShoppingBasket/AddItemToBasket", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();
            var actualResultJson = JsonConvert.DeserializeObject<AddItemResultDto>(actualResult);

            // Assert
            Assert.Equal(expectedStatusCode, actualStatusCode);
            Assert.Equal (1, actualResultJson.ItemCount);
        }

        public void Dispose()
        {
            var client = new MongoClient(TestStartup.ConnectionString);
            client.DropDatabase(TestStartup.DbName);
        }
    }
}
