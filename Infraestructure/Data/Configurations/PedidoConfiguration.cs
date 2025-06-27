using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PedidoEntity = API_Pdv.Entities.Pedido;

public class PedidoConfiguration : IEntityTypeConfiguration<PedidoEntity>
{
    public void Configure(EntityTypeBuilder<PedidoEntity> builder)
    {
        builder.ToTable("pedidos");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.EmpresaId).HasColumnName("empresa_id");
        builder.Property(p => p.CaixaId).HasColumnName("caixa_id");
        builder.Property(p => p.DataHora).HasColumnName("data_hora");
        builder.Property(p => p.ValorTotal).HasColumnName("valor_total").HasColumnType("decimal(10,2)");
        builder.Property(p => p.Observacao).HasColumnName("observacao").HasColumnType("TEXT");
        builder.Property(p => p.Status).HasColumnName("status").HasMaxLength(20);
    }
} 