using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ItemPedidoEntity = API_Pdv.Entities.ItemPedido;
public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedidoEntity>
{
    public void Configure(EntityTypeBuilder<ItemPedidoEntity> builder)
    {
        builder.ToTable("itens_pedido");
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Id).HasColumnName("id");
        builder.Property(i => i.PedidoId).HasColumnName("pedido_id");
        builder.Property(i => i.ProdutoId).HasColumnName("produto_id");
        builder.Property(i => i.Quantidade).HasColumnName("quantidade");
        builder.Property(i => i.PrecoUnitario).HasColumnName("preco_unitario").HasColumnType("decimal(10,2)");
        builder.Property(i => i.Total).HasColumnName("total").HasColumnType("decimal(10,2)");
    }
} 