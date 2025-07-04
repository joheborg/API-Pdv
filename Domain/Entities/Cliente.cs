using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class Cliente
{
    public int Id { get; set; }
    
    [Required, StringLength(100)]
    public string Nome { get; set; } = "";
    
    [StringLength(20)]
    public string? Telefone { get; set; }
    
    [StringLength(100)]
    public string? Email { get; set; }
    
    [StringLength(20)]
    public string? CPFCNPJ { get; set; }
    
    [StringLength(255)]
    public string? Endereco { get; set; }
    
    [StringLength(100)]
    public string? Cidade { get; set; }
    
    [StringLength(2)]
    public string? UF { get; set; }
    
    [StringLength(10)]
    public string? CEP { get; set; }
    
    public bool Ativo { get; set; } = true;
    
    public DateTime DataCadastro { get; set; } = DateTime.Now;
    
    public int? EmpresaId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(EmpresaId))]
    public virtual Empresa? Empresa { get; set; }
    
    public virtual ICollection<Pedido>? Pedidos { get; set; }
    public virtual ICollection<Venda>? Vendas { get; set; }
} 