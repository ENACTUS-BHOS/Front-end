using System.Globalization;
using System.Reflection;
using System.Resources;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Front_end;
using Blazored.Modal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "/Shared/Resources";
});

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazoredModal();

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("Miras", (serviceProvider, client) =>
{
    var url = builder.Configuration.GetSection("MirasLink").Get<string>();

    client.BaseAddress = new Uri(url!);
});

await builder.Build().RunAsync();