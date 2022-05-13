using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence;
using Persistence.Context;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using(var scope = host.Services.CreateScope()) {
                var services = scope.ServiceProvider;
                try
                {
                    var readContext = services.GetRequiredService<DataReadContext>();
                    var writeContext = services.GetRequiredService<DataWriteContext>();
                    var userManager = services.GetRequiredService<UserManager<User>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    readContext.Database.Migrate();
                    writeContext.Database.Migrate();
                    Seed.SeedData(readContext, userManager, roleManager).Wait();
                    Seed.SeedData(writeContext, userManager, roleManager).Wait();
                } catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "An error occurred during migration");
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}