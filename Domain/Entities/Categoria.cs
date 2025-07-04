using System;
using System.ComponentModel.DataAnnotations;

namespace API_Pdv.Entities;

public class Categoria
{
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Descricao { get; set; } = null!;

    // Controle de datas
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
} 