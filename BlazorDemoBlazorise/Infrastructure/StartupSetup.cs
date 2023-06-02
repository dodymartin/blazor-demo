using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BlazorDemo.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorDemo.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<BlazorDemoDbContext>(options =>
                options.UseSqlServer(connectionString));
    }
}
