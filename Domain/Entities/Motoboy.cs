using System;
using API_Pdv.Entities;

namespace API_Pdv.Entities;
public class Motoboy
{
    public int Id { get; set; }
    public int EmpresaId { get; set; }
    public string Nome { get; set; } = null!;
    public string Documento { get; set; } = null!;
    public string Telefone { get; set; } = null!;
    public string Veiculo { get; set; } = null!;
    public string Placa { get; set; } = null!;
    public string Status { get; set; } = "ativo";
    public string? Observacao { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
} 