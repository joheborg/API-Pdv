using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotoBoyEntity = API_Pdv.Entities.Motoboy;

public class MotoboyConfiguration : IEntityTypeConfiguration<MotoBoyEntity>
{
    public void Configure(EntityTypeBuilder<MotoBoyEntity> builder)
    {
        builder.ToTable("motoboy");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).HasColumnName("id");
        builder.Property(m => m.EmpresaId).HasColumnName("empresa_id");
        builder.Property(m => m.Nome).HasColumnName("nome").HasMaxLength(100).IsRequired();
        builder.Property(m => m.Documento).HasColumnName("documento").HasMaxLength(20).IsRequired();
        builder.Property(m => m.Telefone).HasColumnName("telefone").HasMaxLength(20).IsRequired();
        builder.Property(m => m.Veiculo).HasColumnName("veiculo").HasMaxLength(50).IsRequired();
        builder.Property(m => m.Placa).HasColumnName("placa").HasMaxLength(10).IsRequired();
        builder.Property(m => m.Status).HasColumnName("status").HasMaxLength(20).IsRequired();
        builder.Property(m => m.Observacao).HasColumnName("observacao").HasMaxLength(255);
        builder.Property(m => m.CreatedAt).HasColumnName("created_at");
        builder.Property(m => m.UpdatedAt).HasColumnName("updated_at");
    }
} 