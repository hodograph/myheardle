using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyHeardle;
using MudBlazor.Services;
using Caerostris.Services.Spotify;
using Microsoft.Extensions.Configuration;
using Blazored.Modal;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();
builder.Services.AddBlazoredModal();

builder.Services.AddSpotify(new() // <-- 
{
    // If you supply a non-null value, the Authorization Code Grant workflow will be used.
    // (Use https!)
    // Otherwise, the Implicit Grant workflow will be used instead.
    AuthServerApiBase = null, //"https://localhost:44363/auth",
    PlayerDeviceName = "MyHeardle",
    // Issued by Spotify, register your app and view its ID at
    // https://developer.spotify.com/dashboard/
    ClientId = builder.Configuration.GetValue<string>("SPOTIFY_CLIENT_ID"),
    // All permissions SpotifyService currently uses
    PermissionScopes = new[]
    {
        "user-read-private",
        "user-read-email",
        "user-read-playback-state",
        "user-modify-playback-state",
        "user-library-read",
        "user-library-modify",
        "user-read-currently-playing",
        "playlist-read-private",
        "playlist-read-collaborative",
        "playlist-modify-private",
        "playlist-modify-public",
        "streaming"
    }
});

var host = builder.Build();

await host.RunAsync();

await host.Services.InitializeSpotify();
