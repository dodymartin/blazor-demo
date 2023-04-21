using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RushAg.Core.Entities;

namespace RushAg.Infrastructure.Data.Config
{
    public class TodoStepConfiguration : IEntityTypeConfiguration<TodoStep>
    {
        public void Configure(EntityTypeBuilder<TodoStep> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Name)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
