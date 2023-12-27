using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Application.Register;
using ScheduleManager.DataAccess.Context;
using ScheduleManager.DataAccess.Models;
using ScheduleManager.DataAccess.Repositories;
using ScheduleManager.Domain.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<ScheduleManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ScheduleManagerContextConnection") ?? throw new InvalidOperationException("Connection string not found.")));

builder.Services.AddIdentity<User, Role>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<ScheduleManagerContext>();

builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddMediatR(typeof(IAuthRepository).Assembly);
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(RegisterHandler).Assembly);

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

app.Run();
