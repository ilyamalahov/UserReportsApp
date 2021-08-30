using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using MapsterMapper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WebClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            var apiServiceUrl = builder.Configuration["ApiUrl"];

            builder.Services.AddHttpClient<IApiHttpClient, ApiHttpClient>(c => c.BaseAddress = new Uri(apiServiceUrl));

            builder.Services.AddScoped<IMapper, Mapper>();

            builder.Services.AddBlazorise()
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            await builder.Build().RunAsync();
        }
    }
}
