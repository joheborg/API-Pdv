using API_Pdv.Entities;
namespace API_Pdv.Entities;
public class ItemPedido
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;

    public int ProdutoId { get; set; }

    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Total { get; set; }
} 