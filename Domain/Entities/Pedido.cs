using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class Pedido
{
    public int Id { get; set; }
    
    [Required, StringLength(50)]
    public string NumeroPedido { get; set; } = "";
    
    [Required, StringLength(100)]
    public string NomeCliente { get; set; } = "";
    
    [Required, StringLength(20)]
    public string TelefoneCliente { get; set; } = "";
    
    [StringLength(100)]
    public string? EmailCliente { get; set; }
    
    [StringLength(255)]
    public string? EnderecoCliente { get; set; }
    
    public int QuantidadeItens { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }
    
    [Required, StringLength(50)]
    public string Status { get; set; } = ""; // Pendente, Em Preparo, Concluído, Cancelado
    
    public DateTime DataPedido { get; set; }
    public DateTime? DataConclusao { get; set; }
    
    [StringLength(500)]
    public string? Observacoes { get; set; }
    
    public int? SituacaoId { get; set; }
    public int? EmpresaId { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(SituacaoId))]
    public virtual StatusPedido? Situacao { get; set; }
    
    [ForeignKey(nameof(EmpresaId))]
    public virtual Empresa? Empresa { get; set; }
    
    public virtual ICollection<ItemPedido>? ItensPedido { get; set; }
} 