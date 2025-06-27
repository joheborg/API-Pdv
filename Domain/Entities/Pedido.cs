using System;
using System.Collections.Generic;
using API_Pdv.Entities;


namespace API_Pdv.Entities;


public class Pedido
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }

    public int? CaixaId { get; set; }
    public Caixa? Caixa { get; set; }

    public DateTime DataHora { get; set; } = DateTime.Now;
    public decimal ValorTotal { get; set; }
    public string? Observacao { get; set; }
    public string Status { get; set; } = "aberto";

    public ICollection<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
} 