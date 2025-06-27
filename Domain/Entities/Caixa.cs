using System;
using System.Collections.Generic;
using API_Pdv.Entities;
namespace API_Pdv.Entities;
public class Caixa
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public Empresa Empresa { get; set; } = null!;

    public DateTime DataAbertura { get; set; }
    public DateTime? DataFechamento { get; set; }
    public decimal ValorAbertura { get; set; }
    public decimal ValorFechamento { get; set; }
    public decimal TrocoFinal { get; set; }
    public string Status { get; set; } = "aberto";
    public string? Observacao { get; set; }

    public decimal TotalDinheiro { get; set; }
    public decimal TotalCartaoCredito { get; set; }
    public decimal TotalCartaoDebito { get; set; }
    public decimal TotalPix { get; set; }
    public decimal TotalOutros { get; set; }
    public decimal TotalVendas { get; set; }

    public ICollection<PagamentoCaixa> Pagamentos { get; set; } = new List<PagamentoCaixa>();
    public ICollection<Pedido> Pedidos { get; set; } = new List<Pedido>();
} 