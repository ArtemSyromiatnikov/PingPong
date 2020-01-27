using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using PeterLeslieMorris.Blazor.Validation;
using PingPong.Blazor.Validators;
using PingPong.Sdk;
using System.Net.Http;

namespace PingPong.Blazor.ClientApp
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<HttpClient>(s =>
            {
                //var client = new HttpClient() { BaseAddress = new System.Uri(Configuration["PingPongApi"]) };
                //var client = new HttpClient() { BaseAddress = new System.Uri("http://localhost:5001/") };
                var client = new HttpClient() { BaseAddress = new System.Uri("https://pingpong-api.azurewebsites.net/") };
                return client;
            });
            services.AddScoped<IApiClient, ApiClient>();

            services.AddFormValidation(config => config.AddFluentValidation(typeof(GameValidator).Assembly));
        }

        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
