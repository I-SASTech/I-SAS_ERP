using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using Focus.Business.Users;
using Focus.Domain.Interface;
using Focus.Persistence;
using System.Security.Principal;
using System.Text;
using Focus.Business;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ScheduledTasks;
using Noble.Api.Middlewares;
using Focus.Business.Infrastructure.AutoMapper;
using Microsoft.AspNetCore.Http;
using NLog.Web;
using Rotativa.AspNetCore;
using IHttpContextAccessor = Microsoft.AspNetCore.Http.IHttpContextAccessor;

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

//WebApplicationBuilder builder ;
var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder1 =>
        {

            //builder.WithOrigins(Configuration.GetSection("frontend:IpAndServerAddress").Value, "app://.").AllowAnyMethod()
            //.AllowAnyHeader();

            builder1.WithOrigins(
                builder.Configuration.GetSection("frontend:IpAndServerAddress").Value
                //builder.Configuration.GetSection("frontend:IpAndServerAddress1").Value,
                //builder.Configuration.GetSection("frontend:IpAndServerAddress2").Value,
                //builder.Configuration.GetSection("frontend:IpAndServerAddress3").Value
            ).AllowAnyHeader();
        });
});

builder.Services.Configure<IdentityOptions>(opts => {
    opts.Password.RequiredLength = 3;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
});


//builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).GetTypeInfo().Assembly);
//  services.AddSingleton<IHttpContextAccessor, tHtpContextAccessor>();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
//builder.Services.AddInfrastructure();
builder.Services.AddScheduledTasks();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
//services.AddDefaultIdentity<ApplicationUser, ApplicationRole>()
//    .AddDefaultUI(UIFramework.Bootstrap4)
//    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); 
builder.Services.AddTransient<IUserHttpContextProvider, UserHttpContextProvider>();
builder.Services.AddTransient<IPrincipal>(provider => provider.GetService<IHttpContextAccessor>().HttpContext.User);
builder.Services.AddHttpClient();

// ===== Add Jwt Authentication ========
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;


    })
    .AddJwtBearer(cfg =>
    {
        cfg.RequireHttpsMetadata = false;
        cfg.SaveToken = true;
        cfg.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration.GetSection("jwt:JwtIssuer").Value,
            ValidAudience = builder.Configuration.GetSection("jwt:JwtIssuer").Value,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwt:JwtKey").Value)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero // remove delay of token when expire
        };
    });


builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromHours(1);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});



var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
try
{
    logger.Debug("init main");
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(LogLevel.Trace);
    builder.Host.UseNLog();
}
catch (Exception ex)
{
    //NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}


var app = builder.Build();
// Configure the HTTP request pipeline.

app.Use(async (ctx, next) =>
{
    await next();
    if (ctx.Response.StatusCode == 204)
    {
        ctx.Response.ContentLength = 0;
    }
});
if (!app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRequestLocalization();
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();
app.UseCustomMiddleware();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

RotativaConfiguration.Setup(builder.Environment.WebRootPath);
app.Run();
