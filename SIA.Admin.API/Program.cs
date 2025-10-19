using Microsoft.AspNetCore.Authentication.Cookies;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
           .AddCookie(cookieOptions =>
           {
               cookieOptions.LoginPath = "/Home/Index";
               cookieOptions.AccessDeniedPath = new PathString("/Error/AccessDenied");
               cookieOptions.Cookie.SameSite = SameSiteMode.Strict;
               //cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.Always;
               cookieOptions.Cookie.IsEssential = true;
               cookieOptions.Cookie.HttpOnly = true;
           });

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger());

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = null; });

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.UseStatusCodePagesWithRedirects("/Error/{0}");

app.Use(async (context, next) =>
{
    context.Response.Headers.Remove("X-Powered-By");
    context.Response.Headers.Remove("Server");
    context.Response.Headers.Remove("X-AspNet-Version");
    context.Response.Headers.Remove("X-AspNetMvc-Version");
    await next();
});

// Configure the HTTP request pipeline.

app.UseCookiePolicy();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
