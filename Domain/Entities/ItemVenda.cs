using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API_Pdv.Entities;

public class ItemVenda
{
    public int Id { get; set; }
    
    public int VendaId { get; set; }
    
    public int ProdutoId { get; set; }
    
    public int Quantidade { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoUnitario { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal? Desconto { get; set; }
    
    [Column(TypeName = "decimal(10,2)")]
    public decimal Total { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
    // Relacionamentos
    [ForeignKey(nameof(VendaId))]
    public virtual Venda? Venda { get; set; }
    
    [ForeignKey(nameof(ProdutoId))]
    public virtual Produto? Produto { get; set; }
} 