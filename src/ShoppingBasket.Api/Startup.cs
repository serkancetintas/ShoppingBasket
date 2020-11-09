using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ShoppingBasket.Application.Baskets;
using ShoppingBasket.Application.Contracts.Services;
using ShoppingBasket.Repository.Concrete;
using ShoppingBasket.Repository.Abstract;
using Microsoft.OpenApi.Models;
using ShoppingBasket.Application.Stocks;
using ShoppingBasket.Api.Middlewares;

namespace ShoppingBasket.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        void ConfigureCorsPolicy(IServiceCollection services)
        {
            services.AddCors(o =>
            {
                o.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .WithExposedHeaders("WWW-Authenticate");
                });

            });
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            ConfigureDatabase(services);

            services.AddScoped<IBasketContext, BasketContext>();
            services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            AddServiceDependency(services);

            services.AddControllers().AddApplicationPart(typeof(Startup).Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Basket API", Version = "v1" });
            });

            ConfigureCorsPolicy(services);
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                            serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        }

        private static void AddServiceDependency(IServiceCollection services)
        {
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IStockService, StockService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Basket API v1");
            });
        }
    }
}
