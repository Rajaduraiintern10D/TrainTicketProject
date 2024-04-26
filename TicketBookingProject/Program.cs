using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using TicketBookingProject.Data.ApplicationDbContext;
using TicketBookingProject.IRepositry;
using TicketBookingProject.Repositry;
using TicketBookingProject.IRepository;
using TicketBookingProject.Repository;
using TicketBookingProject.repository;


var builder = WebApplication.CreateBuilder(args);

// Configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables()
    .Build();

builder.Services.AddSingleton(configuration);

// Entity Framework
//builder..AddDbContext<TrainTicketProDbContext>(options =>
//    options.UseSqlServer(configuration.GetConnectionString("Cons")));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));


// Automapper ConfigurationServices
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false, 
            ValidateIssuerSigningKey = true
        };
    });

// Authorization
builder.Services.AddAuthorization();

// Controllers
builder.Services.AddControllers(); // Add this line to register controller services

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Train Ticket Booking", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please Insert Token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    }); 
});
//Repositry Register

builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<ITrainDetailsRepository , TrainDetailsRepository>();
builder.Services.AddScoped<IUserCredentialRepository , UserCredentialRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();


