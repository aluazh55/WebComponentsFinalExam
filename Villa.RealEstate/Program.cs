using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.MSSqlServer;
using Serilog;
using Villa.RealEstate.Models;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Localization;
using Villa.RealEstate.AppMiddleware;
using Villa.RealEstate.AppFilter;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddEndpointsApiExplorer();






builder.Services.AddControllers(options =>
{
    options.Filters.Add<IEFilterAttribute>();
});





builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCulture = new[]
    {
        new CultureInfo("ru-RU"),
        new CultureInfo("kk-KZ"),
        new CultureInfo("en-US")
    };

    options.DefaultRequestCulture = new RequestCulture(culture: "kk-KZ", uiCulture: "kk-KZ");
    options.SupportedCultures = supportedCulture;
    options.SupportedUICultures = supportedCulture;
});


builder.Services.AddControllersWithViews()
    .AddViewLocalization() // Локализация представлений
    .AddDataAnnotationsLocalization();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<TokenService>();
builder.Services.AddDbContext<AppIdentityDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"))
);
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<AppIdentityDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.ConfigureApplicationCookie(option =>
option.LoginPath = "/Account/Login"
);




Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.Seq("http://localhost:5341/")
    .WriteTo.File("Logs/WebApplication2.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.MSSqlServer(
    connectionString: builder.Configuration.GetConnectionString("DefaultConnections"),
    sinkOptions: new MSSqlServerSinkOptions()
    {
        TableName = "Log",
        AutoCreateSqlDatabase = true
    }
    )



    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

var supportedCulture = new[] { "en-US", "kk-KZ", "ru-RU" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("kk-KZ")
    .AddSupportedCultures(supportedCulture)
    .AddSupportedUICultures(supportedCulture);

app.UseRequestLocalization(localizationOptions);






// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseRequestLocalization(localizationOptions);


app.UseAuthorization();
app.UseAuthentication();

//app.UseMiddleware<UseloggingMiddleware>();
app.UseMiddleware<UseTimeElapsed>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
