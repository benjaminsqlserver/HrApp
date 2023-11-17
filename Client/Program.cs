using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using HrApp.Client;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddRadzenComponents();
builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<HrApp.Client.HrDBService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddHttpClient("HrApp.Server", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));
builder.Services.AddTransient(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("HrApp.Server"));
builder.Services.AddScoped<HrApp.Client.SecurityService>();
builder.Services.AddScoped<AuthenticationStateProvider, HrApp.Client.ApplicationAuthenticationStateProvider>();
var host = builder.Build();
await host.RunAsync();