using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RushAg.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<RushAgDbContext>(options =>
                options.UseSqlServer(connectionString));
    }
}
