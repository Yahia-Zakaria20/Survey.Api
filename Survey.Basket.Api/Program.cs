
using Survey.Basket.Api.Middelwares;
using Survey.Basket.Api.test;

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

            webApplicationBuilder.Services.AddKeyedScoped<IoperationOS, Windos>("Win");
            webApplicationBuilder.Services.AddKeyedScoped<IoperationOS, IOS>("ios");

            var app = webApplicationBuilder.Build();

           

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

     
            app.MapControllers();

            app.Run();
        }
    }
}
