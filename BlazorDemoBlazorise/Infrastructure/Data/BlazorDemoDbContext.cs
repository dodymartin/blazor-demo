using Microsoft.EntityFrameworkCore;
using BlazorDemo.Core.Entities;
using System.Reflection;

namespace BlazorDemo.Infrastructure.Data
{
    public class BlazorDemoDbContext : DbContext
    {
        public BlazorDemoDbContext(DbContextOptions<BlazorDemoDbContext> options)
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
