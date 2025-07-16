using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class Avaliacao
{
    public int Id { get; set; }
    
    [Required, StringLength(20)]
    public string NumeroComanda { get; set; } = "";
    
    [Required, Range(1, 5)]
    public int Nota { get; set; }
    
    [StringLength(500)]
    public string? Descricao { get; set; }
    
    public int? EmpresaId { get; set; }
    
    public DateTime DataAvaliacao { get; set; } = DateTime.Now;
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(EmpresaId))]
    public virtual Empresa? Empresa { get; set; }
} 