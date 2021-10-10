using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PeterLeslieMorris.Blazor.Validation;
using PingPong.Blazor.Validators;
using PingPong.Sdk;

namespace PingPong.Blazor.ClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<HttpClient>(s =>
            {
                //var client = new HttpClient() { BaseAddress = new System.Uri(Configuration["PingPongApi"]) };
                //var client = new HttpClient() { BaseAddress = new System.Uri("https://localhost:5001/") };
                var client = new HttpClient() { BaseAddress = new System.Uri("https://pingpong-api.azurewebsites.net/") };
                return client;
            });
            builder.Services.AddScoped<IApiClient, ApiClient>();
            builder.Services.AddFormValidation(config => config.AddFluentValidation(typeof(GameValidator).Assembly));

            await builder.Build().RunAsync();
        }
    }
}
