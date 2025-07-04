using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class VendaConfiguration : IEntityTypeConfiguration<Venda>
{
    public void Configure(EntityTypeBuilder<Venda> builder)
    {
        builder.ToTable("Vendas");

        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).ValueGeneratedOnAdd();

        builder.Property(v => v.NumeroVenda).IsRequired().HasMaxLength(50);
        builder.Property(v => v.NomeCliente).IsRequired().HasMaxLength(100);
        builder.Property(v => v.TelefoneCliente).HasMaxLength(20);
        builder.Property(v => v.EmailCliente).HasMaxLength(100);
        builder.Property(v => v.CPFCNPJ).HasMaxLength(20);
        builder.Property(v => v.FormaPagamento).IsRequired().HasMaxLength(50);
        builder.Property(v => v.Total).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(v => v.Desconto).HasColumnType("decimal(10,2)");
        builder.Property(v => v.TotalFinal).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(v => v.DataVenda).IsRequired();
        builder.Property(v => v.Observacoes).HasMaxLength(500);
        builder.Property(v => v.SituacaoId);
        builder.Property(v => v.EmpresaId);
        builder.Property(v => v.CreatedAt).IsRequired();
        builder.Property(v => v.UpdatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(v => v.Situacao)
            .WithMany()
            .HasForeignKey(v => v.SituacaoId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(v => v.Empresa)
            .WithMany()
            .HasForeignKey(v => v.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(v => v.NumeroVenda).HasDatabaseName("idx_venda_numero");
        builder.HasIndex(v => v.DataVenda).HasDatabaseName("idx_venda_data");
        builder.HasIndex(v => v.EmpresaId).HasDatabaseName("idx_venda_empresa");
        builder.HasIndex(v => v.CPFCNPJ).HasDatabaseName("idx_venda_cpfcnpj");
    }
} 