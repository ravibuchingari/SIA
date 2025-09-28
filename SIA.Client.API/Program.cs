using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SIA.Authentication;
using SIA.Client.API.Middlewares;
using SIA.Domain.Models;
using SIA.Infrastructure.Data;
using SIA.Infrastructure.Interfaces;
using SIA.Infrastructure.Repositories;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var jwtParameters = new JwtTokenParameter()
{
    JwtSecurityKey = builder.Configuration["JWTSettings:JWTKey"]!,
    IsValidateIssuer = Convert.ToBoolean(builder.Configuration["JWTSettings:IsValidIssuer"]),
    IsValidateAudience = Convert.ToBoolean(builder.Configuration["JWTSettings:IsValidAudience"]),
    ValidIssuer = builder.Configuration["JWTSettings:ValidIssuer"]!,
    ValidAudience = builder.Configuration["JWTSettings:ValidAudience"]!,
    TokenValidityInMinutes = Convert.ToDouble(builder.Configuration["JWTSettings:JWTTokenValidityInMinutes"]),
};

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmailRepository, EmailRepository>();
builder.Services.AddScoped<ISharedRepository, SharedRepository>();
builder.Services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();

builder.Services.AddSingleton<IGlobalConfigRepository, GlobalConfigRepository>();
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IApiCallerRepository, ApiCallerRepository>();


builder.Logging.ClearProviders();

builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger());

builder.Services.AddControllersWithViews().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase; });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(builder.Configuration.GetSection("CORS-Settings:Allow-Origins")!.Get<string[]>()!)
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
    });
});

builder.Services.AddControllers();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

IConfigurationSection googleAuthNSection = builder.Configuration.GetSection("Authentication:Google");

builder.Services.AddJwtAuthentication(jwtParameters);

if (!string.IsNullOrEmpty(googleAuthNSection["ClientId"]) && !string.IsNullOrEmpty(googleAuthNSection["ClientSecretKey"]))
    builder.Services.AddGoogleAuthentication(googleAuthNSection["ClientId"]!, googleAuthNSection["ClientSecretKey"]!);


builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
});

//builder.Services.AddHttpsRedirection(options =>
//{
//    options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
//    //options.HttpsPort = 44388;
//});

builder.Services.AddAutoMapper(options => { }, typeof(Program));



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

// Configure the HTTP request pipeline.

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.UseCors();

app.UseExceptionMiddleware();

app.UseHsts();

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
