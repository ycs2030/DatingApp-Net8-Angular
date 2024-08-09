using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;

namespace API.Extensions;
public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
    IConfiguration config)
    {
        // Add services to the container.
        services.AddControllers();

        // adding db context to the services container
         services.AddDbContext<DataContext>(opt =>
            {
              opt.UseSqlite(config
               .GetConnectionString("DefaultConnection"));
            });
       return services;
    }
}
