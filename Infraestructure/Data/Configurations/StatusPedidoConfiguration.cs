using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class StatusPedidoConfiguration : IEntityTypeConfiguration<StatusPedido>
{
    public void Configure(EntityTypeBuilder<StatusPedido> builder)
    {
        builder.ToTable("StatusPedidos");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedOnAdd();
        builder.Property(s => s.Descricao).IsRequired().HasMaxLength(100);
        builder.Property(s => s.CreatedAt).IsRequired();
        builder.Property(s => s.UpdatedAt).IsRequired();
    }
} 