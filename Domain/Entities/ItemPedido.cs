using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using API_Pdv.Entities;
namespace API_Pdv.Entities;
public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    [ForeignKey(nameof(PedidoId))]
    public virtual Pedido? Pedido { get; set; }

    public int ProdutoId { get; set; }
    [ForeignKey(nameof(ProdutoId))]
    public virtual Produto? Produto { get; set; }

    public int Quantidade { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal PrecoUnitario { get; set; }
    [Column(TypeName = "decimal(10,2)")]
    public decimal Subtotal { get; set; }

    [StringLength(500)]
    public string? Observacoes { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
} 