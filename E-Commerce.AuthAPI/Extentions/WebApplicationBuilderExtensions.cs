using E_Commerce.AuthAPI.Models;
using E_Commerce.AuthAPI.Repository.Infrasturcture;
using E_Commerce.AuthAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using E_Commerce.APIResponseLibrary.Constant;
using E_Commerce.AuthAPI.Data;
using Microsoft.EntityFrameworkCore;
using E_Commerce.AuthAPI.Repository.AuthConfig;
using E_Commerce.AuthAPI.Utility.FilterAttributeHandler;

namespace E_Commerce.AuthAPI.Extentions
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
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "E-Commerce.AuthAPI", Version = "v1" });
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

        public static WebApplicationBuilder AddScopedServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IPasswordHasher<ApplicationUser>, PasswordHasherRepository>();
            builder.Services.AddScoped<PasswordHasher<ApplicationUser>>();
            builder.Services.AddScoped<IUserManagerService<ApplicationUser>, UserManagerService>();
            builder.Services.AddScoped<IJWTTokenGenerator, JWTTokenGenerator>();
            builder.Services.AddScoped<IUserLoginRepository, UserLoginRepository>();
            return builder;
        }

        public static WebApplicationBuilder AddTransientServices(this WebApplicationBuilder builder)
        {
           
            return builder;
        }

        public static WebApplicationBuilder AddAppSettingConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<JwtOption>(builder.Configuration.GetSection("ApiSetting:JwtAuthentication"));
            return builder;
        }
        public static WebApplicationBuilder AddDbContextAppSettingConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
            return builder;
        }

        public static WebApplicationBuilder AddIdentityServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            return builder;
        }
    }
}
