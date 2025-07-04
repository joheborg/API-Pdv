using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
{
    public void Configure(EntityTypeBuilder<ItemPedido> builder)
    {
        builder.ToTable("ItensPedido");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();

        builder.Property(i => i.PedidoId).IsRequired();
        builder.Property(i => i.ProdutoId).IsRequired();
        builder.Property(i => i.Quantidade).IsRequired();
        builder.Property(i => i.PrecoUnitario).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(i => i.Subtotal).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(i => i.Observacoes).HasMaxLength(500);
        builder.Property(i => i.CreatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(i => i.Pedido)
            .WithMany(p => p.ItensPedido)
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Produto)
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(i => i.PedidoId).HasDatabaseName("idx_item_pedido_pedido");
        builder.HasIndex(i => i.ProdutoId).HasDatabaseName("idx_item_pedido_produto");
    }
} 