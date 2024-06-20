using E_Commerce.APIResponseLibrary.Constant;
using E_Commerce.AuthAPI.Data;
using E_Commerce.AuthAPI.Extentions;
using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Repository;
using E_Commerce.AuthAPI.Repository.AuthConfig;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using E_Commerce.AuthAPI.Utility.FilterAttributeHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddSwaggerCustomization();
builder.AddAppSettingConfigureServices();
builder.AddDbContextAppSettingConfigureServices();
builder.AddIdentityServices();
builder.AddScopedServices();
builder.AddTransientServices();
builder.AddAppAuthentication();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
