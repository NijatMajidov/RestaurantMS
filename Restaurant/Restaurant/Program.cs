using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RMS.Business;
using RMS.Business.Helpers.Email;
using RMS.Business.Mapping;
using RMS.Core.Entities;
using RMS.Data.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddServices(builder.Configuration);
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_@";
    opt.User.RequireUniqueEmail = true;
    opt.Lockout.MaxFailedAccessAttempts = 5;
    opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
    opt.Tokens.ProviderMap.Add("emailconfirmation", new TokenProviderDescriptor(typeof(DataProtectorTokenProvider<AppUser>)));
    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
    opt.Tokens.PasswordResetTokenProvider = "passwordreset";
    opt.Tokens.ProviderMap.Add("passwordreset", new TokenProviderDescriptor(typeof(DataProtectorTokenProvider<AppUser>)));
    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
}).AddEntityFrameworkStores<RMSAppContext>().AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
{
    opt.TokenLifespan = TimeSpan.FromMinutes(1);
});
builder.Services.Configure<DataProtectionTokenProviderOptions>("emailconfirmation", opt =>
{
    opt.TokenLifespan = TimeSpan.FromMinutes(1);
});

builder.Services.Configure<DataProtectionTokenProviderOptions>("passwordreset", opt =>
{
    opt.TokenLifespan = TimeSpan.FromMinutes(1);
});

builder.Services.AddDbContext<RMSAppContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
