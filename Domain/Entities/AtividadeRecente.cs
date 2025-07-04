using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class AtividadeRecente
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Titulo { get; set; } = "";
    
    [Required, StringLength(500)]
    public string Descricao { get; set; } = "";
    
    [Required, StringLength(20)]
    public string Tipo { get; set; } = ""; // success, warning, info, danger
    
    [Required, StringLength(50)]
    public string Status { get; set; } = "";
    
    public DateTime DataHora { get; set; }
    
    public int? UsuarioId { get; set; }
    public int? EmpresaId { get; set; }
    
    [StringLength(50)]
    public string? Entidade { get; set; } // Pedido, Venda, Produto, etc.
    
    public int? EntidadeId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(UsuarioId))]
    public virtual Usuario? Usuario { get; set; }
    
    [ForeignKey(nameof(EmpresaId))]
    public virtual Empresa? Empresa { get; set; }
} 