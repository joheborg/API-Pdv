using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("Transacoes");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedOnAdd();

        builder.Property(t => t.Tipo).IsRequired().HasMaxLength(20);
        builder.Property(t => t.Descricao).IsRequired().HasMaxLength(200);
        builder.Property(t => t.Valor).HasColumnType("decimal(10,2)").IsRequired();
        builder.Property(t => t.Categoria).IsRequired().HasMaxLength(100);
        builder.Property(t => t.FormaPagamento).IsRequired().HasMaxLength(50);
        builder.Property(t => t.DataHora).IsRequired();
        builder.Property(t => t.Observacoes).HasMaxLength(500);
        builder.Property(t => t.EmpresaId);
        builder.Property(t => t.CategoriaId);
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.UpdatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(t => t.Empresa)
            .WithMany()
            .HasForeignKey(t => t.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.CategoriaEntidade)
            .WithMany()
            .HasForeignKey(t => t.CategoriaId)
            .OnDelete(DeleteBehavior.SetNull);

        // Índices
        builder.HasIndex(t => t.Tipo).HasDatabaseName("idx_transacao_tipo");
        builder.HasIndex(t => t.DataHora).HasDatabaseName("idx_transacao_data");
        builder.HasIndex(t => t.EmpresaId).HasDatabaseName("idx_transacao_empresa");
    }
} 