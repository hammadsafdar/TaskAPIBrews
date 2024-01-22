using BrewTask.DBCore.AppContext;
using BrewTask.Models.GenericResponse;
using BrewTask.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace BrewTask.Extensions
{
    public static class ApplicationServiceExtension
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration configuration)
        {

           
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<iBrewsService, BrewsService>();
            services.AddScoped<ResponseModel>();
            services.AddScoped<AppDbContext>();

            services.AddCors();

            //services.AddDbContext<AppDbContext>(options =>
            //options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDbContext>(
                o =>
                {
                    o.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
                    o.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

           // services.AddControllers()
           //.AddJsonOptions(options =>
           //{
           //    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
           //});


            return services;
        }
    }
}
