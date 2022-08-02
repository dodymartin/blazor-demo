using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushAg.Infrastructure.Data.Config
{
    public class TodoStepConfiguration : IEntityTypeConfiguration<TodoStep>
    {
        public void Configure(EntityTypeBuilder<TodoStep> builder)
        {
            builder.HasKey(p => p.TodoStepId);
            builder.Property(p => p.TodoStepId)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.StepName)
                .HasMaxLength(150)
                .IsRequired();
        }
    }
}
