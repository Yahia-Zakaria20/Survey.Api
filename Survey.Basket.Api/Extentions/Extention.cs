using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Survey.Basket.Api.Servises;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace Survey.Basket.Api.Extentions
{
    public static class Extention
    {
        public static void AddMapster(this IServiceCollection services) 
        {
            var GlobalSettings = TypeAdapterConfig.GlobalSettings;

              GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(GlobalSettings));

        }
        public static void AddApplicationServices(this IServiceCollection services) 
        {
            services.AddScoped<IPollService, PollService>();

           services.AddMapster(); //User Defined 

         services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
                .AddFluentValidationAutoValidation();
           
        }

    }
}
