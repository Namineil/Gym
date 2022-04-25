using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Gym.API.Persistence.Contexts;

namespace Gym.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    using (var context = services.GetRequiredService<AppDbContext>()) {
                        context.Database.EnsureCreated();
                    }
                }
                catch (System.Exception ex)
                {

                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while seeding the database");
                }
            }
            // using (var context = scope.ServiceProvider.GetService<AppDbContext>()) {
            //     context.Database.EnsureCreated();
            // }

            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                    .Build();
    }
}
