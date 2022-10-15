using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RushAg.Core.Entities;

namespace RushAg.Infrastructure.Data.Config
{
    public class TodoItemConfiguration : IEntityTypeConfiguration<TodoItem>
    {
        public void Configure(EntityTypeBuilder<TodoItem> builder)
        {
            builder.HasKey(p => p.TodoItemId);
            builder.Property(p => p.TodoItemId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(p => p.Notes)
                .HasMaxLength(500);


                
        }
    }
}
