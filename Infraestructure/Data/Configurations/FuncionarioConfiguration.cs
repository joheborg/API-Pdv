using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class FuncionarioConfiguration : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("Funcionarios");

        builder.HasKey(f => f.Id);
        builder.Property(f => f.Id).ValueGeneratedOnAdd();

        builder.Property(f => f.Nome).IsRequired().HasMaxLength(100);
        builder.Property(f => f.CPF).HasMaxLength(14);
        builder.Property(f => f.RG).HasMaxLength(20);
        builder.Property(f => f.Telefone).HasMaxLength(20);
        builder.Property(f => f.Email).HasMaxLength(100);
        builder.Property(f => f.Cargo).HasMaxLength(50);
        builder.Property(f => f.Salario).HasColumnType("decimal(10,2)");
        builder.Property(f => f.DataAdmissao);
        builder.Property(f => f.DataDemissao);
        builder.Property(f => f.Ativo).IsRequired();
        builder.Property(f => f.Endereco).HasMaxLength(255);
        builder.Property(f => f.Cidade).HasMaxLength(100);
        builder.Property(f => f.UF).HasMaxLength(2);
        builder.Property(f => f.CEP).HasMaxLength(10);
        builder.Property(f => f.EmpresaId);
        builder.Property(f => f.CreatedAt).IsRequired();
        builder.Property(f => f.UpdatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(f => f.Empresa)
            .WithMany()
            .HasForeignKey(f => f.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(f => f.Nome).HasDatabaseName("idx_funcionario_nome");
        builder.HasIndex(f => f.CPF).HasDatabaseName("idx_funcionario_cpf");
        builder.HasIndex(f => f.Email).HasDatabaseName("idx_funcionario_email");
        builder.HasIndex(f => f.Ativo).HasDatabaseName("idx_funcionario_ativo");
        builder.HasIndex(f => f.EmpresaId).HasDatabaseName("idx_funcionario_empresa");
    }
} 