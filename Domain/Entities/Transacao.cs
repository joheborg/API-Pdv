using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class Transacao
{
    public int Id { get; set; }
    
    [Required, StringLength(20)]
    public string Tipo { get; set; } = ""; // Receita, Despesa
    
    [Required, StringLength(200)]
    public string Descricao { get; set; } = "";
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Valor { get; set; }
    
    [Required, StringLength(100)]
    public string Categoria { get; set; } = "";
    
    [Required, StringLength(50)]
    public string FormaPagamento { get; set; } = "";
    
    public DateTime DataHora { get; set; }
    
    [StringLength(500)]
    public string? Observacoes { get; set; }
    
    public int? EmpresaId { get; set; }
    public int? CategoriaId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(EmpresaId))]
    public virtual Empresa? Empresa { get; set; }
    
    [ForeignKey(nameof(CategoriaId))]
    public virtual Categoria? CategoriaEntidade { get; set; }
} 