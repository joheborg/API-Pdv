using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class ItemVendaConfiguration : IEntityTypeConfiguration<ItemVenda>
{
    public void Configure(EntityTypeBuilder<ItemVenda> builder)
    {
        builder.ToTable("ItensVenda");

        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).ValueGeneratedOnAdd();

        builder.Property(i => i.VendaId).IsRequired();
        builder.Property(i => i.ProdutoId).IsRequired();
        builder.Property(i => i.Quantidade).IsRequired();
        builder.Property(i => i.PrecoUnitario).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(i => i.Subtotal).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(i => i.Desconto).HasColumnType("decimal(10,2)");
        builder.Property(i => i.Total).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(i => i.CreatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(i => i.Venda)
            .WithMany(v => v.ItensVenda)
            .HasForeignKey(i => i.VendaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(i => i.Produto)
            .WithMany()
            .HasForeignKey(i => i.ProdutoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(i => i.VendaId).HasDatabaseName("idx_item_venda_venda");
        builder.HasIndex(i => i.ProdutoId).HasDatabaseName("idx_item_venda_produto");
    }
} 