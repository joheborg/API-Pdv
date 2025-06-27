using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutoEntities = API_Pdv.Entities.Produto;
using System;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class ProdutoConfiguration : IEntityTypeConfiguration<ProdutoEntities>
{
    public void Configure(EntityTypeBuilder<ProdutoEntities> builder)
    {
        builder.ToTable("produtos");
        
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnName("id").ValueGeneratedOnAdd();
               
        builder.Property(p => p.EmpresaId).HasColumnName("empresa_id");
        
        // Dados Gerais
        builder.Property(p => p.CodigoProduto).HasColumnName("codigo_produto").HasMaxLength(50);
        builder.Property(p => p.ImagemUrl).HasColumnName("imagem_url").HasMaxLength(255);
        
        // Campos de imagem
        builder.Property(p => p.ImagemBase64)
            .HasColumnName("imagem_base64")
            .HasColumnType("LONGBLOB")
            .HasConversion(
                v => v == null ? null : Convert.FromBase64String(v),
                v => v == null ? null : Convert.ToBase64String(v)
            );
        
        builder.Property(p => p.ImagemNome).HasColumnName("imagem_nome").HasMaxLength(255);
        builder.Property(p => p.ImagemMimeType).HasColumnName("imagem_mime_type").HasMaxLength(100);
        
        builder.Property(p => p.Nome).HasColumnName("nome").IsRequired().HasMaxLength(100);
        builder.Property(p => p.NomeAlternativo).HasColumnName("nome_alternativo").HasMaxLength(100);
        builder.Property(p => p.Descricao).HasColumnName("descricao").HasColumnType("TEXT");
        builder.Property(p => p.Categoria).HasColumnName("categoria").HasMaxLength(100);
        builder.Property(p => p.Ingredientes).HasColumnName("ingredientes").HasColumnType("TEXT");
        
        // Campo Situacao
        builder.Property(p => p.Situacao).HasColumnName("situacao").HasDefaultValue(true);
        
        // Dados Comerciais
        builder.Property(p => p.PrecoVenda).HasColumnName("preco_venda").HasColumnType("decimal(10,2)");
        builder.Property(p => p.PrecoCusto).HasColumnName("preco_custo").HasColumnType("decimal(10,2)");
        builder.Property(p => p.QuantidadePadrao).HasColumnName("quantidade_padrao");
        builder.Property(p => p.Peso).HasColumnName("peso").HasMaxLength(50);
        builder.Property(p => p.ServePessoas).HasColumnName("serve_pessoas").HasMaxLength(50);
        builder.Property(p => p.CodigoBarras).HasColumnName("codigo_barras").HasMaxLength(50);
        builder.Property(p => p.UnidadeVenda).HasColumnName("unidade_venda").HasMaxLength(10).HasDefaultValue("UN");
        
        // Dados Fiscais Básicos
        builder.Property(p => p.NCM).HasColumnName("ncm").HasMaxLength(20);
        builder.Property(p => p.CEST).HasColumnName("cest").HasMaxLength(20);
        builder.Property(p => p.CFOP).HasColumnName("cfop").HasMaxLength(20);
        builder.Property(p => p.CSOSN_CST).HasColumnName("csosn_cst").HasMaxLength(20);
        builder.Property(p => p.OrigemProduto).HasColumnName("origem_produto");
        
        // Dados Fiscais ICMS
        builder.Property(p => p.CstIcms).HasColumnName("cst_icms").HasMaxLength(3).HasDefaultValue("00");
        builder.Property(p => p.BaseCalculoIcms).HasColumnName("base_calculo_icms").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        builder.Property(p => p.AliquotaIcms).HasColumnName("aliquota_icms").HasColumnType("decimal(5,2)").HasDefaultValue(0.00m);
        builder.Property(p => p.ValorIcms).HasColumnName("valor_icms").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        
        // Dados Fiscais IPI
        builder.Property(p => p.CstIpi).HasColumnName("cst_ipi").HasMaxLength(3).HasDefaultValue("50");
        builder.Property(p => p.BaseCalculoIpi).HasColumnName("base_calculo_ipi").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        builder.Property(p => p.AliquotaIpi).HasColumnName("aliquota_ipi").HasColumnType("decimal(5,2)").HasDefaultValue(0.00m);
        builder.Property(p => p.ValorIpi).HasColumnName("valor_ipi").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        
        // Dados Fiscais PIS
        builder.Property(p => p.CstPis).HasColumnName("cst_pis").HasMaxLength(3).HasDefaultValue("01");
        builder.Property(p => p.BaseCalculoPis).HasColumnName("base_calculo_pis").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        builder.Property(p => p.AliquotaPis).HasColumnName("aliquota_pis").HasColumnType("decimal(5,2)").HasDefaultValue(1.65m);
        builder.Property(p => p.ValorPis).HasColumnName("valor_pis").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        
        // Dados Fiscais COFINS
        builder.Property(p => p.CstCofins).HasColumnName("cst_cofins").HasMaxLength(3).HasDefaultValue("01");
        builder.Property(p => p.BaseCalculoCofins).HasColumnName("base_calculo_cofins").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        builder.Property(p => p.AliquotaCofins).HasColumnName("aliquota_cofins").HasColumnType("decimal(5,2)").HasDefaultValue(7.60m);
        builder.Property(p => p.ValorCofins).HasColumnName("valor_cofins").HasColumnType("decimal(10,2)").HasDefaultValue(0.00m);
        
        // Códigos adicionais
        builder.Property(p => p.CodigoEan).HasColumnName("codigo_ean").HasMaxLength(14);
        builder.Property(p => p.InformacoesAdicionais).HasColumnName("informacoes_adicionais").HasColumnType("TEXT");
        
        // Datas
        builder.Property(p => p.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updated_at").IsRequired();
        
        // Índices
        builder.HasIndex(p => new { p.EmpresaId, p.CodigoProduto }).HasDatabaseName("uk_empresa_codigo_produto").IsUnique();
        builder.HasIndex(p => p.NCM).HasDatabaseName("idx_ncm");
        builder.HasIndex(p => p.Categoria).HasDatabaseName("idx_categoria");
        builder.HasIndex(p => p.CodigoEan).HasDatabaseName("idx_codigo_ean");
        builder.HasIndex(p => p.CodigoBarras).HasDatabaseName("idx_codigo_barras");
        builder.HasIndex(p => p.Situacao).HasDatabaseName("idx_situacao");
        
        // Controle de Estoque
        builder.Property(p => p.EstoqueAtual).HasColumnName("estoque_atual").HasDefaultValue(0);
        builder.Property(p => p.EstoqueMinimo).HasColumnName("estoque_minimo").HasDefaultValue(0);
        builder.Property(p => p.EstoqueMaximo).HasColumnName("estoque_maximo").HasDefaultValue(0);
        builder.Property(p => p.UltimaMovimentacao).HasColumnName("ultima_movimentacao");
        builder.Property(p => p.LocalizacaoEstoque).HasColumnName("localizacao_estoque");
        builder.Property(p => p.ControlaEstoque).HasColumnName("controla_estoque").HasDefaultValue(true);
    }
} 