using Microsoft.EntityFrameworkCore;
using RushAg.Shared;

namespace RushAg.Server.Data
{
    public class RushAgDbContext : DbContext
    {
        public RushAgDbContext(DbContextOptions<RushAgDbContext> options)
            : base(options)
        { }
        public DbSet<TodoItem> TodoItems { get; set; }
    }

    
}
