using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class Funcionario
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Nome { get; set; } = "";
    
    [StringLength(14)]
    public string? CPF { get; set; }
    
    [StringLength(20)]
    public string? RG { get; set; }
    
    [StringLength(20)]
    public string? Telefone { get; set; }
    
    [StringLength(100)]
    public string? Email { get; set; }
    
    [StringLength(50)]
    public string? Cargo { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Salario { get; set; }
    
    public DateTime? DataAdmissao { get; set; }
    public DateTime? DataDemissao { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    [StringLength(255)]
    public string? Endereco { get; set; }
    
    [StringLength(100)]
    public string? Cidade { get; set; }
    
    [StringLength(2)]
    public string? UF { get; set; }
    
    [StringLength(10)]
    public string? CEP { get; set; }
    
    public int? EmpresaId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(EmpresaId))]
    public virtual Empresa? Empresa { get; set; }
} 