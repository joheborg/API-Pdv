using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CaixaEntity = API_Pdv.Entities.Caixa;
public class CaixaConfiguration : IEntityTypeConfiguration<CaixaEntity>
{
    public void Configure(EntityTypeBuilder<CaixaEntity> builder)
    {
        builder.ToTable("caixas");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnName("id");
        builder.Property(c => c.EmpresaId).HasColumnName("empresa_id");
        builder.Property(c => c.DataAbertura).HasColumnName("data_abertura");
        builder.Property(c => c.DataFechamento).HasColumnName("data_fechamento");
        builder.Property(c => c.ValorAbertura).HasColumnName("valor_abertura").HasColumnType("decimal(10,2)");
        builder.Property(c => c.ValorFechamento).HasColumnName("valor_fechamento").HasColumnType("decimal(10,2)");
        builder.Property(c => c.TrocoFinal).HasColumnName("troco_final").HasColumnType("decimal(10,2)");
        builder.Property(c => c.Status).HasColumnName("status").HasMaxLength(20);
        builder.Property(c => c.Observacao).HasColumnName("observacao").HasColumnType("TEXT");
        builder.Property(c => c.TotalDinheiro).HasColumnName("total_dinheiro").HasColumnType("decimal(10,2)");
        builder.Property(c => c.TotalCartaoCredito).HasColumnName("total_cartao_credito").HasColumnType("decimal(10,2)");
        builder.Property(c => c.TotalCartaoDebito).HasColumnName("total_cartao_debito").HasColumnType("decimal(10,2)");
        builder.Property(c => c.TotalPix).HasColumnName("total_pix").HasColumnType("decimal(10,2)");
        builder.Property(c => c.TotalOutros).HasColumnName("total_outros").HasColumnType("decimal(10,2)");
        builder.Property(c => c.TotalVendas).HasColumnName("total_vendas").HasColumnType("decimal(10,2)");
    }
} 