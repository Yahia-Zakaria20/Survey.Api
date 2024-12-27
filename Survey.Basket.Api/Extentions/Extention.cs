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
using Survey.Basket.Api.Helper;
using Survey.Basket.Api.Errors;

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
        public static void AddApplicationServices(this IServiceCollection services , IConfiguration configuration) 
        {
            services.AddScoped<IPollService, PollService>();

           services.AddMapsterServices(); //User Defined 

            services.AddAuthServices(configuration); 



            var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
            services.AddCors(options => {    //Config   TO Cors 
                options.AddPolicy("ClientPolicy1", config =>   //user defined policy 
                {
                    config.WithOrigins(allowedOrigins!)
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                });

                //options.AddDefaultPolicy(config =>   //defult policy 
                //{ 
                //    config.WithOrigins(allowedOrigins!)
                //    .AllowAnyHeader()
                //    .AllowAnyMethod();
                //});
            });



           services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                  .AddFluentValidationAutoValidation();

            services.AddExceptionHandler<GlobalExeptionHandler>();
            services.AddProblemDetails();

        }

        public static void AddAuthServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbcontext>();

            services.AddScoped<IAuthServices, AuthServices>();

            services.AddSingleton<IJwtServices, JwtServices>();

            //  services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName)); //configre to use optionspattern to section  Without Validation on values from AppsettingsFile

            services.AddOptions<JwtOptions>()
                .BindConfiguration(JwtOptions.SectionName)
               .ValidateDataAnnotations()
               .ValidateOnStart();  // With Validation on values from AppsettingsFile


            var jwtoptions = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>(); //GetGroupe "Jwt" And Bind It in 'JwtOptions(UserDefined)'
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtoptions!.SecurityKey)),
                    ValidIssuer = jwtoptions!.issuer,
                    ValidAudience = jwtoptions!.audience,
                    ClockSkew = TimeSpan.FromMinutes(jwtoptions.expires)
                };
            });

        }

    }
}
