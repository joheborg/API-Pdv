using System;
using System.ComponentModel.DataAnnotations;

namespace API_Pdv.Entities;

public class StatusPedido
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Descricao { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
} 