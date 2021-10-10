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
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddScoped(_ => new HttpClient() { BaseAddress = new System.Uri(builder.Configuration["PingPongApi"]) });
            builder.Services.AddScoped<IApiClient, ApiClient>();
            builder.Services.AddFormValidation(config => config.AddFluentValidation(typeof(GameValidator).Assembly));

            await builder.Build().RunAsync();
        }
    }
}
