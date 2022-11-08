using AltairCA.Blazor.WebAssembly.Cookie.Framework;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Nordic_Door.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();
builder.Services.AddAltairCACookieService(options =>
{
    options.DefaultExpire = TimeSpan.FromHours(2); // Inloggingscookie vil utgå hver 2. time
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:32766/") });

await builder.Build().RunAsync();
