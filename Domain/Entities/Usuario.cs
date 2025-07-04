using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class Usuario
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Nome { get; set; } = "";
    
    [Required, StringLength(100)]
    public string Email { get; set; } = "";
    
    [Required, StringLength(255)]
    public string Senha { get; set; } = "";
    
    [StringLength(20)]
    public string? Perfil { get; set; } = ""; // Admin, Gerente, Operador
    
    public bool Ativo { get; set; } = true;
    
    public DateTime? UltimoAcesso { get; set; }
    
    public int? FuncionarioId { get; set; }
    public int? EmpresaId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(FuncionarioId))]
    public virtual Funcionario? Funcionario { get; set; }
    
    [ForeignKey(nameof(EmpresaId))]
    public virtual Empresa? Empresa { get; set; }
} 