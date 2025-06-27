# Mapeamento de Entidades C# para Colunas SQL Snake_Case

## Visão Geral

Este documento mostra como as entidades C# (PascalCase) são mapeadas para as colunas SQL (snake_case) usando Entity Framework Core.

## Padrão Adotado

- **C# (Entidades)**: PascalCase (ex: `RazaoSocial`, `PrecoVenda`)
- **SQL (Colunas)**: snake_case (ex: `razao_social`, `preco_venda`)
- **Tabelas**: snake_case (ex: `empresas`, `produtos`)

## Mapeamento da Entidade Empresa

### C# → SQL

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `Id` | `id` | `INT` | Primary Key, Auto Increment |
| `CNPJ` | `cnpj` | `VARCHAR(14)` | Unique, Not Null |
| `RazaoSocial` | `razao_social` | `VARCHAR(200)` | Not Null |
| `NomeFantasia` | `nome_fantasia` | `VARCHAR(200)` | Nullable |
| `InscricaoEstadual` | `inscricao_estadual` | `VARCHAR(50)` | Nullable |
| `CRT` | `crt` | `VARCHAR(5)` | Nullable |
| `LogoBase64` | `logo_base64` | `LONGBLOB` | Conversão automática base64 ↔ bytes |
| `LogoNome` | `logo_nome` | `VARCHAR(255)` | Nullable |
| `LogoMimeType` | `logo_mime_type` | `VARCHAR(100)` | Nullable |
| `CreatedAt` | `created_at` | `DATETIME` | Default: CURRENT_TIMESTAMP |
| `UpdatedAt` | `updated_at` | `DATETIME` | Auto Update |

### Endereço (Owned Entity)

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `Endereco.Logradouro` | `endereco_logradouro` | `VARCHAR(255)` | Nullable |
| `Endereco.Numero` | `endereco_numero` | `VARCHAR(20)` | Nullable |
| `Endereco.Complemento` | `endereco_complemento` | `VARCHAR(100)` | Nullable |
| `Endereco.Bairro` | `endereco_bairro` | `VARCHAR(100)` | Nullable |
| `Endereco.CodigoMunicipio` | `endereco_codigo_municipio` | `VARCHAR(10)` | Nullable |
| `Endereco.NomeMunicipio` | `endereco_nome_municipio` | `VARCHAR(100)` | Nullable |
| `Endereco.UF` | `endereco_uf` | `VARCHAR(2)` | Nullable |
| `Endereco.CEP` | `endereco_cep` | `VARCHAR(10)` | Nullable |
| `Endereco.CodigoPais` | `endereco_codigo_pais` | `VARCHAR(10)` | Default: '1058' |
| `Endereco.NomePais` | `endereco_nome_pais` | `VARCHAR(50)` | Default: 'Brasil' |

## Mapeamento da Entidade Produto

### C# → SQL

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `Id` | `id` | `INT` | Primary Key, Auto Increment |
| `EmpresaId` | `empresa_id` | `INT` | Foreign Key, Not Null |
| `CodigoProduto` | `codigo_produto` | `VARCHAR(50)` | Nullable |
| `ImagemUrl` | `imagem_url` | `VARCHAR(255)` | Nullable |
| `ImagemBase64` | `imagem_base64` | `LONGBLOB` | Conversão automática base64 ↔ bytes |
| `ImagemNome` | `imagem_nome` | `VARCHAR(255)` | Nullable |
| `ImagemMimeType` | `imagem_mime_type` | `VARCHAR(100)` | Nullable |
| `Nome` | `nome` | `VARCHAR(100)` | Not Null |
| `NomeAlternativo` | `nome_alternativo` | `VARCHAR(100)` | Nullable |
| `Descricao` | `descricao` | `TEXT` | Nullable |
| `Categoria` | `categoria` | `VARCHAR(100)` | Nullable |
| `Ingredientes` | `ingredientes` | `TEXT` | Nullable |
| `Situacao` | `situacao` | `TINYINT` | Default: 1 (true) |

### Dados Comerciais

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `PrecoVenda` | `preco_venda` | `DECIMAL(10,2)` | Nullable |
| `PrecoCusto` | `preco_custo` | `DECIMAL(10,2)` | Nullable |
| `QuantidadePadrao` | `quantidade_padrao` | `INT` | Nullable |
| `Peso` | `peso` | `VARCHAR(50)` | Nullable |
| `ServePessoas` | `serve_pessoas` | `VARCHAR(50)` | Nullable |
| `CodigoBarras` | `codigo_barras` | `VARCHAR(50)` | Nullable |
| `UnidadeVenda` | `unidade_venda` | `VARCHAR(10)` | Default: 'UN' |

### Dados Fiscais Básicos

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `NCM` | `ncm` | `VARCHAR(20)` | Nullable |
| `CEST` | `cest` | `VARCHAR(20)` | Nullable |
| `CFOP` | `cfop` | `VARCHAR(20)` | Nullable |
| `CSOSN_CST` | `csosn_cst` | `VARCHAR(20)` | Nullable |
| `OrigemProduto` | `origem_produto` | `TINYINT` | Nullable |

### Dados Fiscais ICMS

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `CstIcms` | `cst_icms` | `VARCHAR(3)` | Default: '00' |
| `BaseCalculoIcms` | `base_calculo_icms` | `DECIMAL(10,2)` | Default: 0.00 |
| `AliquotaIcms` | `aliquota_icms` | `DECIMAL(5,2)` | Default: 0.00 |
| `ValorIcms` | `valor_icms` | `DECIMAL(10,2)` | Default: 0.00 |

### Dados Fiscais IPI

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `CstIpi` | `cst_ipi` | `VARCHAR(3)` | Default: '50' |
| `BaseCalculoIpi` | `base_calculo_ipi` | `DECIMAL(10,2)` | Default: 0.00 |
| `AliquotaIpi` | `aliquota_ipi` | `DECIMAL(5,2)` | Default: 0.00 |
| `ValorIpi` | `valor_ipi` | `DECIMAL(10,2)` | Default: 0.00 |

### Dados Fiscais PIS

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `CstPis` | `cst_pis` | `VARCHAR(3)` | Default: '01' |
| `BaseCalculoPis` | `base_calculo_pis` | `DECIMAL(10,2)` | Default: 0.00 |
| `AliquotaPis` | `aliquota_pis` | `DECIMAL(5,2)` | Default: 1.65 |
| `ValorPis` | `valor_pis` | `DECIMAL(10,2)` | Default: 0.00 |

### Dados Fiscais COFINS

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `CstCofins` | `cst_cofins` | `VARCHAR(3)` | Default: '01' |
| `BaseCalculoCofins` | `base_calculo_cofins` | `DECIMAL(10,2)` | Default: 0.00 |
| `AliquotaCofins` | `aliquota_cofins` | `DECIMAL(5,2)` | Default: 7.60 |
| `ValorCofins` | `valor_cofins` | `DECIMAL(10,2)` | Default: 0.00 |

### Códigos Adicionais

| Propriedade C# | Coluna SQL | Tipo SQL | Observações |
|----------------|------------|----------|-------------|
| `CodigoEan` | `codigo_ean` | `VARCHAR(14)` | Nullable |
| `InformacoesAdicionais` | `informacoes_adicionais` | `TEXT` | Nullable |
| `CreatedAt` | `created_at` | `DATETIME` | Default: CURRENT_TIMESTAMP |
| `UpdatedAt` | `updated_at` | `DATETIME` | Auto Update |

## Configuração no Entity Framework

### Exemplo de Mapeamento

```csharp
// Configuração da Empresa
builder.ToTable("empresas");
builder.Property(e => e.RazaoSocial).HasColumnName("razao_social");
builder.Property(e => e.NomeFantasia).HasColumnName("nome_fantasia");

// Configuração do Produto
builder.ToTable("produtos");
builder.Property(p => p.PrecoVenda).HasColumnName("preco_venda");
builder.Property(p => p.CodigoBarras).HasColumnName("codigo_barras");
```

### Relacionamentos

```csharp
// Foreign Key com nome personalizado
builder.HasOne(p => p.Empresa)
    .WithMany(e => e.Produtos)
    .HasForeignKey(p => p.EmpresaId)
    .HasConstraintName("fk_produtos_empresa")
    .OnDelete(DeleteBehavior.Cascade);
```

### Índices

```csharp
// Índices com nomes personalizados
builder.HasIndex(e => e.CNPJ).HasDatabaseName("idx_cnpj").IsUnique();
builder.HasIndex(p => p.Categoria).HasDatabaseName("idx_categoria");
```

## Vantagens do Padrão Snake_Case

1. **Consistência**: Padrão universal em bancos de dados
2. **Legibilidade**: Fácil leitura em consultas SQL
3. **Compatibilidade**: Funciona bem com diferentes SGBDs
4. **Convenção**: Segue convenções SQL padrão
5. **Manutenibilidade**: Código mais organizado e previsível

## Exemplo de Consulta SQL

```sql
-- Consulta com nomes de colunas snake_case
SELECT 
    e.id,
    e.razao_social,
    e.nome_fantasia,
    e.endereco_logradouro,
    e.endereco_numero,
    e.endereco_complemento,
    e.endereco_bairro,
    e.endereco_nome_municipio,
    e.endereco_uf,
    e.endereco_cep,
    e.created_at,
    e.updated_at
FROM empresas e
WHERE e.situacao = 1
ORDER BY e.razao_social;
```

## Scripts SQL Gerados

Os scripts SQL seguem o padrão snake_case:

```sql
CREATE TABLE empresas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cnpj VARCHAR(14) NOT NULL UNIQUE,
    razao_social VARCHAR(200) NOT NULL,
    nome_fantasia VARCHAR(200),
    -- ... outros campos
);

CREATE TABLE produtos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    empresa_id INT NOT NULL,
    codigo_produto VARCHAR(50),
    preco_venda DECIMAL(10,2),
    -- ... outros campos
    FOREIGN KEY (empresa_id) REFERENCES empresas(id)
);
``` 