using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using API_Pdv.Interfaces.Repositories;

public class Empresa : IEmpresaUseCase
{
    public int Id { get; set; }

    [Required, StringLength(14)]
    public string CNPJ { get; set; } = null!;

    [Required, StringLength(200)]
    public string RazaoSocial { get; set; } = null!;

    [StringLength(200)]
    public string? NomeFantasia { get; set; }

    [StringLength(50)]
    public string? InscricaoEstadual { get; set; }

    [StringLength(5)]
    public string? CRT { get; set; }

    // Endereço como objeto complexo
    public Endereco Endereco { get; set; } = new Endereco();

    // Navegação: Uma empresa tem muitos produtos
    public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

    // Datas
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}

public class Endereco
{
    [StringLength(255)]
    public string? Logradouro { get; set; }

    [StringLength(20)]
    public string? Numero { get; set; }

    [StringLength(100)]
    public string? Bairro { get; set; }

    [StringLength(10)]
    public string? CodigoMunicipio { get; set; }

    [StringLength(100)]
    public string? NomeMunicipio { get; set; }

    [StringLength(2)]
    public string? UF { get; set; }

    [StringLength(10)]
    public string? CEP { get; set; }
}