using FunyMovieBackend.DbContexts;
using FunyMovieBackend.DbContexts.Entities;
using FunyMovieBackend.IdentityServers;
using FunyMovieBackend.Services;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();


void ConfigureServices(IServiceCollection services)
{
    services
       .AddIdentity<AppUser, IdentityRole>(options =>
       {
           // Password settings.
           options.Password.RequireDigit = true;
           options.Password.RequireLowercase = true;
           options.Password.RequireNonAlphanumeric = true;
           options.Password.RequireUppercase = true;
           options.Password.RequiredLength = 6;
           options.Password.RequiredUniqueChars = 1;

           // Lockout settings.
           options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
           options.Lockout.MaxFailedAccessAttempts = 5;
           options.Lockout.AllowedForNewUsers = true;

           // User settings.
           options.User.AllowedUserNameCharacters =
           "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
           options.User.RequireUniqueEmail = false;
       })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
    services
    .AddIdentityServer(options =>
    {
        options.Events.RaiseSuccessEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseErrorEvents = true;
    })
    .AddInMemoryClients(IdentityConfiguration.Clients)
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
    .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
    .AddDeveloperSigningCredential(); 
    services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
    services.AddTransient<IMovieService, YoutubeMovieService>();
}


