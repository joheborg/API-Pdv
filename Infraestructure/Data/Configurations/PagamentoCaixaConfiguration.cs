using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PagamentoCaixaEntity = API_Pdv.Entities.PagamentoCaixa;


public class PagamentoCaixaConfiguration : IEntityTypeConfiguration<PagamentoCaixaEntity>
{
    public void Configure(EntityTypeBuilder<PagamentoCaixaEntity> builder)
    {
        builder.ToTable("pagamentos_caixa");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id");
        builder.Property(p => p.CaixaId).HasColumnName("caixa_id");
        builder.Property(p => p.FormaPagamento).HasColumnName("forma_pagamento").HasMaxLength(30).IsRequired();
        builder.Property(p => p.Valor).HasColumnName("valor").HasColumnType("decimal(10,2)");
        builder.Property(p => p.DataHora).HasColumnName("data_hora");
        builder.Property(p => p.Observacao).HasColumnName("observacao").HasColumnType("TEXT");
    }
} 