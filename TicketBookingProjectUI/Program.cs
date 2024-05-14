using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;
using TicketBookingProjectUI.ConstantFile;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Session service
builder.Services.AddSession();

builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

// Check if the secret key is present in the configuration
var secretKey = builder.Configuration["AppSettings:SecretKey"];
if (string.IsNullOrEmpty(secretKey))
{
    // Generate secret key if not present
    secretKey = GenerateSecretKey();
}

// Configure JWT authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "https://localhost:7172/",
            ValidAudience = "TicketBookingProjectUI",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    });

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Sign/Index"; // Set the login path
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Set the expiration time
        options.SlidingExpiration = true; // Enable sliding expiration
    });

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ApiService>();
// Register IHttpClientFactory
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Use session middleware
app.UseSession();

// Use authentication middleware
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Sign}/{action=Index}/{id?}");
});

// If none of the endpoints match, return 404 page
app.Use(async (context, next) =>
{
    await next();  
    if (context.Response.StatusCode == 404 && !context.Response.HasStarted)
    {
        // Handle 404 errors
        context.Request.Path = "/Home/Error";
        await next();
    }
});
string GenerateSecretKey()
{
    var randomBytes = new byte[32]; // 256 bits
    using (var rng = new RNGCryptoServiceProvider())
    {
        rng.GetBytes(randomBytes);
    }
    return Convert.ToBase64String(randomBytes);
}
app.Run();