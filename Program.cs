using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Front_end;
using Microsoft.JSInterop;
using Blazored.Modal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");

builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddBlazoredModal();

builder.Services.AddBlazorBootstrap();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddHttpClient("Miras", (serviceProvider, client) =>
{
    var url = builder.Configuration.GetSection("MirasLink").Get<string>();

    client.BaseAddress = new Uri(url!);
});

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "/Shared/Resources";
});

var host = builder.Build();

const string defaultCulture = "az-AZ";

var js = host.Services.GetRequiredService<IJSRuntime>();

var result = await js.InvokeAsync<string>("blazorCulture.get");

var culture = CultureInfo.GetCultureInfo(result ?? defaultCulture);

if (result == null)
{
    await js.InvokeVoidAsync("blazorCulture.set", defaultCulture);
}

CultureInfo.DefaultThreadCurrentCulture = culture;

CultureInfo.DefaultThreadCurrentUICulture = culture;

await host.RunAsync();