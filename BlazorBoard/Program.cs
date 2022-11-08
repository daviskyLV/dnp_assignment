using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorBoard;
using HttpCllients.ClientInterfaces;
using HttpCllients.Implementations;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7173;http://localhost:5166") });

await builder.Build().RunAsync();
builder.Services.AddScoped<IPostsService, PostsHttpClient>();
builder.Services.AddScoped<IUserService, UserHttpClient>();