using E_Commerce.APIResponseLibrary.Constant;
using E_Commerce.AuthAPI.Data;
using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Repository;
using E_Commerce.AuthAPI.Repository.AuthConfig;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("ApiSetting:JwtAuthentication"));
builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasherRepository>();
builder.Services.AddScoped<PasswordHasher<ApplicationUser>>();
builder.Services.AddScoped<IUserManagerService<ApplicationUser>, UserManagerService>();
builder.Services.AddTransient<IUserLoginRepository, UserLoginRepository>();
builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();

builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
