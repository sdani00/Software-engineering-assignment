using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RazorEngineCore;
using ScheduleManager.Application.GenerateToken;
using ScheduleManager.Application.Register;
using ScheduleManager.Application.Services;
using ScheduleManager.DataAccess;
using ScheduleManager.DataAccess.Context;
using ScheduleManager.DataAccess.Models;
using ScheduleManager.DataAccess.Repositories;
using ScheduleManager.Domain.Repositories;
using ScheduleManager.Domain.Services;
using ScheduleManager.Notification.EmailSender;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ScheduleManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ScheduleManagerContextConnection") ?? throw new InvalidOperationException("Connection string not found.")));

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<User>, AppClaimsPrincipalFactory>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<ScheduleManager.Domain.Services.IAuthenticationService, ScheduleManager.Application.Services.AuthenticationService>();
builder.Services.AddTransient<IRazorEngine, RazorEngine>();
builder.Services.AddTransient<ITemplateFillerService, TemplateFillerService>();
builder.Services.Configure<AuthMessageSenderOptions>(options => builder.Configuration.GetSection("AuthMessageSenderOptions").Bind(options));
builder.Configuration.AddUserSecrets(typeof(Program).Assembly);
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ScheduleManagerContext>().AddDefaultTokenProviders();


builder.Services.AddMediatR(typeof(IAuthRepository).Assembly);
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(RegisterHandler).Assembly);
builder.Services.AddMediatR(typeof(GenerateTokenHandler).Assembly);
builder.Services.AddTransient<ITripRequestRepository, TripRequestRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(    name: "ConfirmEmail",
    pattern: "{controller=Email}/{action=ConfirmEmail}/{userEmail}");

app.Run();
