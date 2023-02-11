using Microsoft.EntityFrameworkCore;
using RushAg.Core.Entities;
using System.Reflection;

namespace RushAg.Infrastructure.Data
{
    public class RushAgDbContext : DbContext
    {
        public RushAgDbContext(DbContextOptions<RushAgDbContext> options)
            : base(options)
        { }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<TodoStep> TodoSteps { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
