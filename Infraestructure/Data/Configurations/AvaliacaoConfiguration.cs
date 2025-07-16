using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AvaliacaoEntities = API_Pdv.Entities.Avaliacao;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class AvaliacaoConfiguration : IEntityTypeConfiguration<AvaliacaoEntities>
{
    public void Configure(EntityTypeBuilder<AvaliacaoEntities> builder)
    {
        builder.HasKey(a => a.Id);
        
        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();
        
        builder.Property(a => a.NumeroComanda)
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(a => a.Nota)
            .IsRequired();
        
        builder.Property(a => a.Descricao)
            .HasMaxLength(500);
        
        builder.Property(a => a.DataAvaliacao)
            .IsRequired();
        
        builder.Property(a => a.CreatedAt)
            .IsRequired();
        
        builder.Property(a => a.UpdatedAt)
            .IsRequired();
        
        // Relacionamento com Empresa
        builder.HasOne(a => a.Empresa)
            .WithMany()
            .HasForeignKey(a => a.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Índice para busca por número da comanda e empresa
        builder.HasIndex(a => new { a.NumeroComanda, a.EmpresaId })
            .IsUnique();
    }
} 