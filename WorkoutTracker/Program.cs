using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Radzen;
using WorkoutTracker;
using WorkoutTracker.Components;
using WorkoutTracker.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAntiforgery().AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddRadzenComponents();
builder.Services.AddScoped<HttpClient>();
var jwtToken = builder.Configuration.GetValue<string>("jwt:Token");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        if (jwtToken != null)
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = "WorkoutTracker",
                ValidAudience = "WorkoutTracker",
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtToken))
            };
        }
        else
        {
            throw new InvalidOperationException("JWT Token is missing.");
        }
    });

builder.Services.AddCascadingAuthenticationState();
builder.Setup();

var app = builder.Build();
await app.AddDb();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    var urlConfig = builder.Configuration
        .GetSection(UrlSettings.ConfigSelection)
        .Get<UrlSettings>();
    if (urlConfig?.Urls?.Any() == true)
    {
        foreach (var url in urlConfig.Urls)
        {
            app.Urls.Add(url);
        }
    }
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthentication();
app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();