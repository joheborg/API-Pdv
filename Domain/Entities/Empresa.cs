using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProdutoEntities = API_Pdv.Entities.Produto;

namespace API_Pdv.Entities;


public class Empresa
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

    // Logo da empresa
    public string? LogoBase64 { get; set; }
    
    [StringLength(255)]
    public string? LogoNome { get; set; }
    
    [StringLength(100)]
    public string? LogoMimeType { get; set; }

    // Endereço como objeto complexo
    public Endereco Endereco { get; set; } = new Endereco();

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
    public string? Complemento { get; set; }

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

    [StringLength(10)]
    public string? CodigoPais { get; set; } = "1058";

    [StringLength(50)]
    public string? NomePais { get; set; } = "Brasil";
}