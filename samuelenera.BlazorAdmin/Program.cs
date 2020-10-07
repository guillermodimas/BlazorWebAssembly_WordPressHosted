using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using System.ComponentModel.Design;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using samuelenera.BlazorAdmin.Services;
using FireSharp.Interfaces;

namespace samuelenera.BlazorAdmin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            var WPDomain = builder.Configuration["WPDomain"];
            
            //Blazoriuse UI Services
            builder.Services
              .AddBlazorise(options =>
              {
                  options.ChangeTextOnKeyPress = false;
              })
              .AddBootstrapProviders()
              .AddFontAwesomeIcons();

            //Enable use of local storage
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();

            //Add custom authentication provider
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            //Configure Custom Memberpress API service to pass JWT Token
            builder.Services.AddScoped<IMemberpressAPIClient, MemberpressAPIClient>();

            //Configure Custom Firebase API service to database
            builder.Services.AddScoped<IFirebaseAPIClient, FirebaseAPIClient>();

            

            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(WPDomain) });
            var host = builder.Build();

            host.Services
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            //await builder.Build().RunAsync();
            await host.RunAsync();

        }
    }
}
