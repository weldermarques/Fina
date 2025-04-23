using Fina.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fina.Api.Data.Mappings;

public class TransactionMapping : IEntityTypeConfiguration<Transaction>
{

    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        
        builder.HasKey(c => c.Id);
        
        builder.Property(p => p.Title)
            .HasColumnType("VARCHAR")
            .HasMaxLength(80)
            .IsRequired();
        
        builder.Property(p => p.Type)
            .HasColumnType("SMALLINT")
            .IsRequired();
        
        builder.Property(p => p.Ammount)
            .HasColumnType("MONEY")
            .IsRequired();
        
        builder.Property(p => p.CreatedAt)
            .IsRequired();
        
        builder.Property(p => p.PaidOrReceivedAt);
        
        builder.Property(p => p.UserId)
            .HasColumnType("VARCHAR")
            .HasMaxLength(160)
            .IsRequired();
    }
}
