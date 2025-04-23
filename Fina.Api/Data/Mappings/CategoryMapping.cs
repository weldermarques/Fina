using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(p => p.Title)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80)
            .IsRequired();
        
        builder.Property(p => p.Description)
            .HasColumnType("VARCHAR")
            .HasMaxLength(255);
        
        builder.Property(p => p.UserId)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
    }
}
