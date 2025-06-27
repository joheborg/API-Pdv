using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_Pdv.Entities;

namespace API_Pdv.Entities;
public class Produto
{
    public int Id { get; set; }

    // Foreign key para empresa
    public int EmpresaId { get; set; }

    [ForeignKey(nameof(EmpresaId))]

    // Dados gerais
    [StringLength(50)]
    public string? CodigoProduto { get; set; }

    [StringLength(255)]
    public string? ImagemUrl { get; set; }

    // Campos de imagem
    public string? ImagemBase64 { get; set; }
    
    [StringLength(255)]
    public string? ImagemNome { get; set; }
    
    [StringLength(100)]
    public string? ImagemMimeType { get; set; }

    [Required, StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    public string? NomeAlternativo { get; set; }

    public string? Descricao { get; set; }

    [StringLength(100)]
    public string? Categoria { get; set; }

    public string? Ingredientes { get; set; }

    // Dados Comerciais
    [Column(TypeName = "decimal(10,2)")]
    public decimal? PrecoVenda { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal? PrecoCusto { get; set; }

    public int? QuantidadePadrao { get; set; }

    [StringLength(50)]
    public string? Peso { get; set; }

    [StringLength(50)]
    public string? ServePessoas { get; set; }

    [StringLength(50)]
    public string? CodigoBarras { get; set; }
    public bool Situacao { get; set; }

    [StringLength(10)]
    public string? UnidadeVenda { get; set; } = "UN";

    // Dados fiscais básicos
    [StringLength(20)]
    public string? NCM { get; set; }

    [StringLength(20)]
    public string? CEST { get; set; }

    [StringLength(20)]
    public string? CFOP { get; set; }

    [StringLength(20)]
    public string? CSOSN_CST { get; set; }

    public byte? OrigemProduto { get; set; }

    // Dados Fiscais ICMS
    [StringLength(3)]
    public string? CstIcms { get; set; } = "00";

    [Column(TypeName = "decimal(10,2)")]
    public decimal? BaseCalculoIcms { get; set; } = 0.00m;

    [Column(TypeName = "decimal(5,2)")]
    public decimal? AliquotaIcms { get; set; } = 0.00m;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? ValorIcms { get; set; } = 0.00m;

    // Dados Fiscais IPI
    [StringLength(3)]
    public string? CstIpi { get; set; } = "50";

    [Column(TypeName = "decimal(10,2)")]
    public decimal? BaseCalculoIpi { get; set; } = 0.00m;

    [Column(TypeName = "decimal(5,2)")]
    public decimal? AliquotaIpi { get; set; } = 0.00m;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? ValorIpi { get; set; } = 0.00m;

    // Dados Fiscais PIS
    [StringLength(3)]
    public string? CstPis { get; set; } = "01";

    [Column(TypeName = "decimal(10,2)")]
    public decimal? BaseCalculoPis { get; set; } = 0.00m;

    [Column(TypeName = "decimal(5,2)")]
    public decimal? AliquotaPis { get; set; } = 1.65m;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? ValorPis { get; set; } = 0.00m;

    // Dados Fiscais COFINS
    [StringLength(3)]
    public string? CstCofins { get; set; } = "01";

    [Column(TypeName = "decimal(10,2)")]
    public decimal? BaseCalculoCofins { get; set; } = 0.00m;

    [Column(TypeName = "decimal(5,2)")]
    public decimal? AliquotaCofins { get; set; } = 7.60m;

    [Column(TypeName = "decimal(10,2)")]
    public decimal? ValorCofins { get; set; } = 0.00m;

    // Códigos adicionais
    [StringLength(14)]
    public string? CodigoEan { get; set; }

    public string? InformacoesAdicionais { get; set; }

    // Controle de datas
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    // Controle de Estoque
    public int EstoqueAtual { get; set; } = 0;
    public int EstoqueMinimo { get; set; } = 0;
    public int EstoqueMaximo { get; set; } = 0;
    public DateTime? UltimaMovimentacao { get; set; }
    public string? LocalizacaoEstoque { get; set; }
    public bool ControlaEstoque { get; set; } = true;
}