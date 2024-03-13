using AutoMapper;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SignRecognition.Client;
using SignRecognition.Client.HttpClients;
using SignRecognition.Client.HttpClients.Interface;
using SignRecognition.Client.Services;
using SignRecognition.Client.Services.Interfaces;
using SignRecognition.Client.ViewModels.Sign;
using SignRecognition.Contract.Signs;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44328") });
builder.Services.AddMudServices();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IAuthenticationHttpClient, AuthenticationHttpClient>();
builder.Services.AddScoped<ISignHttpClient, SignHttpClient>();

builder.Services.AddScoped<AuthStateProviderService>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<AuthStateProviderService>());

builder.Services.AddBlazoredSessionStorage();
builder.Services.AddAutoMapper(typeof(Program));

await builder.Build().RunAsync();

class MappingProfile : Profile
{
    public MappingProfile()
    {
        
        CreateMap<SignContract, SignViewModel>().ReverseMap();
    }
}