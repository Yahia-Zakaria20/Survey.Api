
using Microsoft.EntityFrameworkCore;
using Survey.Basket.Api.Data;
using Survey.Basket.Api.Data.Entites;
using Survey.Basket.Api.Extentions;
using Survey.Basket.Api.Helper;
using Survey.Basket.Api.Servises;
using System.Configuration;

using System.Reflection;


namespace Survey.Basket.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            webApplicationBuilder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            webApplicationBuilder.Services.AddEndpointsApiExplorer();
            webApplicationBuilder.Services.AddSwaggerGen();

         
            var connection = webApplicationBuilder.Configuration.GetConnectionString("DefultConnection") ??
                     throw new InvalidOperationException("Connection String 'DefultConnection' Not Found");
            webApplicationBuilder.Services.AddDbContext<ApplicationDbcontext>(options => 
            {
                options.UseSqlServer(connection);
            });
            webApplicationBuilder.Services.AddApplicationServices(webApplicationBuilder.Configuration); //User Defined 

          
            var app = webApplicationBuilder.Build();

            var scope = app.Services.CreateScope();
            var servises = scope.ServiceProvider;

      using var dbcontext =   servises.GetRequiredService<ApplicationDbcontext>();

            try
            {
                dbcontext.Database.Migrate();

            }
            catch (Exception ex )
            {

              var Logger =  app.Logger;

                Logger.LogError(string.Empty, ex.Message);
            }
           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("ClientPolicy");

            app.UseAuthorization();
         
            app.MapControllers();

            app.Run();
        }
    }
}
