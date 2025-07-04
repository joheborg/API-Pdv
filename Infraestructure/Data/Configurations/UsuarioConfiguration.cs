using API_Pdv.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).ValueGeneratedOnAdd();

        builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Email).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Senha).IsRequired().HasMaxLength(255);
        builder.Property(u => u.Perfil).HasMaxLength(20);
        builder.Property(u => u.Ativo).IsRequired();
        builder.Property(u => u.UltimoAcesso);
        builder.Property(u => u.FuncionarioId);
        builder.Property(u => u.EmpresaId);
        builder.Property(u => u.CreatedAt).IsRequired();
        builder.Property(u => u.UpdatedAt).IsRequired();

        // Relacionamentos
        builder.HasOne(u => u.Funcionario)
            .WithMany()
            .HasForeignKey(u => u.FuncionarioId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(u => u.Empresa)
            .WithMany()
            .HasForeignKey(u => u.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        // Índices
        builder.HasIndex(u => u.Email).HasDatabaseName("idx_usuario_email").IsUnique();
        builder.HasIndex(u => u.Ativo).HasDatabaseName("idx_usuario_ativo");
        builder.HasIndex(u => u.EmpresaId).HasDatabaseName("idx_usuario_empresa");
    }
} 