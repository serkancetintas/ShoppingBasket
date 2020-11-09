using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Repository.Abstract;
using ShoppingBasket.Repository.Concrete;
using System;

namespace ShoppingBasket.Api.IntegrationTest
{
    public class TestStartup: Startup
    {
        public static string ConnectionString;
        public static string DbName;
       
        public TestStartup(IConfiguration configuration) : base(configuration)
        {
            var config = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();

            ConnectionString = config.GetValue<string>("MongoDbSettings:ConnectionString");
            DbName = $"test_db_{Guid.NewGuid()}";
        }

        public override void ConfigureDatabase(IServiceCollection services)
        {
            var mongoDbSettings = new MongoDbSettings();
            mongoDbSettings.ConnectionString = ConnectionString;
            mongoDbSettings.DatabaseName = DbName;

            services.AddSingleton<IMongoDbSettings>(serviceProvider => mongoDbSettings);
        }

       


    }
}
