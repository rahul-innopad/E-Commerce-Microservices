using E_Commerce.APIResponseLibrary.Constant;
using E_Commerce.APIResponseLibrary.Constant.APIConstants;
using E_Commerce.CategoryAPI.Models;
using E_Commerce.CategoryAPI.Repository;
using E_Commerce.CategoryAPI.Repository.Infrasturcture;
using E_Commerce.DAL.Models;
using E_Commerce.MasterInterfaces.CURDInterfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace E_Commerce.CategoryAPI.Extentions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder)
        {
            var settingsSection = builder.Configuration.GetSection("ApiSetting");

            var secret = settingsSection.GetValue<string>("JwtAuthentication:Secret");
            var issuer = settingsSection.GetValue<string>("JwtAuthentication:Issuer");
            var audience = settingsSection.GetValue<string>("JwtAuthentication:Audience");

            var key = Encoding.ASCII.GetBytes(secret);


            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };
            });


            return builder;
        }

        public static WebApplicationBuilder AddSwaggerCustomization(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce.CategoryAPI", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return builder;
        }
        public static WebApplicationBuilder AddAppSettingConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("ApiSetting:JwtAuthentication"));
            return builder;
        }
        public static WebApplicationBuilder AddScopedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            return builder;
        }

        public static WebApplicationBuilder AddSingletonServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<ECommerceDbContext>();
            return builder;
        }
        public static WebApplicationBuilder AddTransientServices(this WebApplicationBuilder builder)
        {

            return builder;
        }
    }
}
