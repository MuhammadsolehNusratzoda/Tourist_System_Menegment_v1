using BlazorClient.Components;
using BlazorClient.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// LocalStorage for JWT token persistence
builder.Services.AddBlazoredLocalStorage();

// Authorization
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();

// HttpClient pointing to WebApi
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "http://localhost:5032/")
});

// Register all client services
builder.Services.AddScoped<ClientAuthService>();
builder.Services.AddScoped<HotelClientService>();
builder.Services.AddScoped<PlaceClientService>();
builder.Services.AddScoped<BookingClientService>();
builder.Services.AddScoped<GuideClientService>();
builder.Services.AddScoped<RestaurantClientService>();
builder.Services.AddScoped<TransportClientService>();
builder.Services.AddScoped<ReviewClientService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
