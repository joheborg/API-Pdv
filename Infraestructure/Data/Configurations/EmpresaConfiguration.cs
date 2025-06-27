using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EmpresaEntities = API_Pdv.Entities.Empresa;
using ProdutoEntities = API_Pdv.Entities.Produto;
using System;

namespace API_Pdv.Infraestructure.Data.Configurations;

public class EmpresaConfiguration : IEntityTypeConfiguration<EmpresaEntities>
{
    public void Configure(EntityTypeBuilder<EmpresaEntities> builder)
    {
        builder.ToTable("empresas");
        
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").ValueGeneratedOnAdd();
        
        // Mapeamento das propriedades para colunas snake_case
        builder.Property(e => e.CNPJ).HasColumnName("cnpj").IsRequired().HasMaxLength(14);
        builder.Property(e => e.RazaoSocial).HasColumnName("razao_social").IsRequired().HasMaxLength(200);
        builder.Property(e => e.NomeFantasia).HasColumnName("nome_fantasia").HasMaxLength(200);
        builder.Property(e => e.InscricaoEstadual).HasColumnName("inscricao_estadual").HasMaxLength(50);
        builder.Property(e => e.CRT).HasColumnName("crt").HasMaxLength(5);
        
        // Logo da empresa
        builder.Property(e => e.LogoBase64)
            .HasColumnName("logo_base64")
            .HasColumnType("LONGBLOB")
            .HasConversion(
                v => v == null ? null : Convert.FromBase64String(v),
                v => v == null ? null : Convert.ToBase64String(v)
            );
        
        builder.Property(e => e.LogoNome).HasColumnName("logo_nome").HasMaxLength(255);
        builder.Property(e => e.LogoMimeType).HasColumnName("logo_mime_type").HasMaxLength(100);
        
        // Configuração do endereço como propriedade complexa
        builder.OwnsOne(e => e.Endereco, endereco =>
        {
            endereco.Property(e => e.Logradouro).HasColumnName("endereco_logradouro").HasMaxLength(255);
            endereco.Property(e => e.Numero).HasColumnName("endereco_numero").HasMaxLength(20);
            endereco.Property(e => e.Complemento).HasColumnName("endereco_complemento").HasMaxLength(100);
            endereco.Property(e => e.Bairro).HasColumnName("endereco_bairro").HasMaxLength(100);
            endereco.Property(e => e.CodigoMunicipio).HasColumnName("endereco_codigo_municipio").HasMaxLength(10);
            endereco.Property(e => e.NomeMunicipio).HasColumnName("endereco_nome_municipio").HasMaxLength(100);
            endereco.Property(e => e.UF).HasColumnName("endereco_uf").HasMaxLength(2);
            endereco.Property(e => e.CEP).HasColumnName("endereco_cep").HasMaxLength(10);
            endereco.Property(e => e.CodigoPais).HasColumnName("endereco_codigo_pais").HasMaxLength(10).HasDefaultValue("1058");
            endereco.Property(e => e.NomePais).HasColumnName("endereco_nome_pais").HasMaxLength(50).HasDefaultValue("Brasil");
        });
        
        // Datas
        builder.Property(e => e.CreatedAt).HasColumnName("created_at").IsRequired();
        builder.Property(e => e.UpdatedAt).HasColumnName("updated_at").IsRequired();
        
        // Índices
        builder.HasIndex(e => e.CNPJ).HasDatabaseName("idx_cnpj").IsUnique();
        builder.HasIndex(e => e.RazaoSocial).HasDatabaseName("idx_razao_social");
    }
} 