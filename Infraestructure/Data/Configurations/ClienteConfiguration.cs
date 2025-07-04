using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).ValueGeneratedOnAdd();

        builder.Property(c => c.Nome).IsRequired().HasMaxLength(100);
        builder.Property(c => c.Telefone).HasMaxLength(20);
        builder.Property(c => c.Email).HasMaxLength(100);
        builder.Property(c => c.CPFCNPJ).HasMaxLength(20);
        builder.Property(c => c.Endereco).HasMaxLength(255);
        builder.Property(c => c.Cidade).HasMaxLength(100);
        builder.Property(c => c.UF).HasMaxLength(2);
        builder.Property(c => c.CEP).HasMaxLength(10);
        builder.Property(c => c.Ativo).IsRequired();
        builder.Property(c => c.DataCadastro).IsRequired();
        builder.Property(c => c.EmpresaId);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(c => c.Empresa)
            .WithMany()
            .HasForeignKey(c => c.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(c => c.Nome).HasDatabaseName("idx_cliente_nome");
        builder.HasIndex(c => c.CPFCNPJ).HasDatabaseName("idx_cliente_cpfcnpj");
        builder.HasIndex(c => c.Email).HasDatabaseName("idx_cliente_email");
        builder.HasIndex(c => c.Ativo).HasDatabaseName("idx_cliente_ativo");
        builder.HasIndex(c => c.EmpresaId).HasDatabaseName("idx_cliente_empresa");
    }
} 