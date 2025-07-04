using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.NumeroPedido).IsRequired().HasMaxLength(50);
        builder.Property(p => p.NomeCliente).IsRequired().HasMaxLength(100);
        builder.Property(p => p.TelefoneCliente).IsRequired().HasMaxLength(20);
        builder.Property(p => p.EmailCliente).HasMaxLength(100);
        builder.Property(p => p.EnderecoCliente).HasMaxLength(255);
        builder.Property(p => p.QuantidadeItens).IsRequired();
        builder.Property(p => p.Total).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(p => p.Status).IsRequired().HasMaxLength(50);
        builder.Property(p => p.DataPedido).IsRequired();
        builder.Property(p => p.DataConclusao);
        builder.Property(p => p.Observacoes).HasMaxLength(500);
        builder.Property(p => p.SituacaoId);
        builder.Property(p => p.EmpresaId);
        builder.Property(p => p.CreatedAt).IsRequired();
        builder.Property(p => p.UpdatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(p => p.Situacao)
            .WithMany()
            .HasForeignKey(p => p.SituacaoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(p => p.Empresa)
            .WithMany()
            .HasForeignKey(p => p.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(p => p.NumeroPedido).HasDatabaseName("idx_pedido_numero");
        builder.HasIndex(p => p.Status).HasDatabaseName("idx_pedido_status");
        builder.HasIndex(p => p.DataPedido).HasDatabaseName("idx_pedido_data");
        builder.HasIndex(p => p.EmpresaId).HasDatabaseName("idx_pedido_empresa");
    }
} 