using System;
namespace API_Pdv.Entities;


public class PagamentoCaixa
{
    public int Id { get; set; }
    public int CaixaId { get; set; }
    public Caixa Caixa { get; set; } = null!;

    public string FormaPagamento { get; set; } = null!;
    public decimal Valor { get; set; }
    public DateTime DataHora { get; set; } = DateTime.Now;
    public string? Observacao { get; set; }
} 