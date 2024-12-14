using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using Survey.Basket.Api.Servises.Polls;
using Microsoft.AspNetCore.Identity;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Servises.Auth;
using Survey.Basket.Api.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Survey.Basket.Api.Services.Jwt;

namespace Survey.Basket.Api.Extentions
{
    public static class Extention
    {
        public static void AddMapsterServices(this IServiceCollection services) 
        {
            var GlobalSettings = TypeAdapterConfig.GlobalSettings;

              GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(GlobalSettings));

        }
        public static void AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IPollService, PollService>();

           services.AddMapsterServices(); //User Defined 

            services.AddAuthServices();

           services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                  .AddFluentValidationAutoValidation();
        }

        public static void AddAuthServices(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbcontext>();

            services.AddScoped<IAuthServices, AuthServices>();

            services.AddSingleton<IJwtServices, JwtServices>();


            services.AddAuthentication(options =>  //JWT Configration 
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StroNGKAutHENTICATIONKEy")),
                    ValidIssuer = "SurveyBasket",
                    ValidAudience = "SurveyBasket-Users",
                    ClockSkew = TimeSpan.FromMinutes(30)
                };
            });

        }

    }
}
