using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_Pdv.Interfaces.Repositories;

public class Produto : IProdutoUseCase
{
    public int Id { get; set; }

    // Foreign key para empresa
    public int EmpresaId { get; set; }

    [ForeignKey(nameof(EmpresaId))]
    public Empresa Empresa { get; set; } = null!;

    // Dados gerais
    [StringLength(50)]
    public string? CodigoProduto { get; set; }

    [StringLength(255)]
    public string? ImagemUrl { get; set; }

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

    [StringLength(10)]
    public string? UnidadeVenda { get; set; }

    // Dados fiscais
    [StringLength(20)]
    public string? NCM { get; set; }

    [StringLength(20)]
    public string? CEST { get; set; }

    [StringLength(20)]
    public string? CFOP { get; set; }

    [StringLength(20)]
    public string? CSOSN_CST { get; set; }

    public byte? OrigemProduto { get; set; } // pode ser enum depois

    // Controle de datas
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}