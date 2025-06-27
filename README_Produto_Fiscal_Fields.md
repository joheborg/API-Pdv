# Implementação dos Novos Campos Fiscais - Entidade Produto

## Visão Geral

Esta documentação explica a implementação dos novos campos fiscais na entidade `Produto` baseada no arquivo `dbStruct.sql` fornecido. Os novos campos permitem o suporte completo à emissão de NFe (Nota Fiscal Eletrônica).

## Novos Campos Implementados

### 1. Dados Fiscais ICMS
```csharp
// Dados Fiscais ICMS
[StringLength(3)]
public string? CstIcms { get; set; } = "00";

[Column(TypeName = "decimal(10,2)")]
public decimal? BaseCalculoIcms { get; set; } = 0.00m;

[Column(TypeName = "decimal(5,2)")]
public decimal? AliquotaIcms { get; set; } = 0.00m;

[Column(TypeName = "decimal(10,2)")]
public decimal? ValorIcms { get; set; } = 0.00m;
```

### 2. Dados Fiscais IPI
```csharp
// Dados Fiscais IPI
[StringLength(3)]
public string? CstIpi { get; set; } = "50";

[Column(TypeName = "decimal(10,2)")]
public decimal? BaseCalculoIpi { get; set; } = 0.00m;

[Column(TypeName = "decimal(5,2)")]
public decimal? AliquotaIpi { get; set; } = 0.00m;

[Column(TypeName = "decimal(10,2)")]
public decimal? ValorIpi { get; set; } = 0.00m;
```

### 3. Dados Fiscais PIS
```csharp
// Dados Fiscais PIS
[StringLength(3)]
public string? CstPis { get; set; } = "01";

[Column(TypeName = "decimal(10,2)")]
public decimal? BaseCalculoPis { get; set; } = 0.00m;

[Column(TypeName = "decimal(5,2)")]
public decimal? AliquotaPis { get; set; } = 1.65m;

[Column(TypeName = "decimal(10,2)")]
public decimal? ValorPis { get; set; } = 0.00m;
```

### 4. Dados Fiscais COFINS
```csharp
// Dados Fiscais COFINS
[StringLength(3)]
public string? CstCofins { get; set; } = "01";

[Column(TypeName = "decimal(10,2)")]
public decimal? BaseCalculoCofins { get; set; } = 0.00m;

[Column(TypeName = "decimal(5,2)")]
public decimal? AliquotaCofins { get; set; } = 7.60m;

[Column(TypeName = "decimal(10,2)")]
public decimal? ValorCofins { get; set; } = 0.00m;
```

### 5. Códigos Adicionais
```csharp
// Códigos adicionais
[StringLength(14)]
public string? CodigoEan { get; set; }

public string? InformacoesAdicionais { get; set; }
```

## Valores Padrão dos Campos Fiscais

| Campo | Valor Padrão | Descrição |
|-------|--------------|-----------|
| `CstIcms` | "00" | CST ICMS padrão |
| `CstIpi` | "50" | CST IPI padrão |
| `CstPis` | "01" | CST PIS padrão |
| `CstCofins` | "01" | CST COFINS padrão |
| `AliquotaPis` | 1.65% | Alíquota PIS padrão |
| `AliquotaCofins` | 7.60% | Alíquota COFINS padrão |
| `UnidadeVenda` | "UN" | Unidade de venda padrão |

## Arquivos Atualizados

### 1. Entidade (`Domain/Entities/Produto.cs`)
- Adicionados todos os novos campos fiscais
- Configuração de tipos de dados apropriados
- Valores padrão conforme legislação

### 2. Configuração EF (`Infraestructure/Data/Configurations/ProdutoConfiguration.cs`)
- Configuração completa dos novos campos
- Índices para otimização de consultas
- Relacionamentos e constraints

### 3. Interface (`Interfaces/Repositories/IProduto.cs`)
- Métodos CRUD completos
- Busca por código, código de barras e EAN
- Busca por empresa

### 4. Repositório (`Infraestructure/Repositories/Produto.cs`)
- Implementação completa dos métodos
- Valores padrão automáticos
- Tratamento de relacionamentos

### 5. Controller (`Controller/V1/Produto.cs`)
- Endpoints RESTful completos
- Busca por diferentes critérios
- Tratamento de erros

### 6. Script SQL (`Utils/produto_table_updated.sql`)
- Estrutura completa da tabela
- Índices para performance
- Foreign key para Empresa

## Endpoints da API

### GET `/api/v1/produto`
- Lista todos os produtos

### GET `/api/v1/produto/empresa/{empresaId}`
- Lista produtos por empresa

### GET `/api/v1/produto/{id}`
- Busca produto por ID

### GET `/api/v1/produto/codigo/{empresaId}/{codigoProduto}`
- Busca produto por código na empresa

### GET `/api/v1/produto/barras/{codigoBarras}`
- Busca produto por código de barras

### GET `/api/v1/produto/ean/{codigoEan}`
- Busca produto por código EAN

### POST `/api/v1/produto`
- Cria novo produto

### PUT `/api/v1/produto/{id}`
- Atualiza produto existente

### DELETE `/api/v1/produto/{id}`
- Remove produto

## Exemplo de Criação de Produto com Dados Fiscais

```json
POST /api/v1/produto
{
  "empresaId": 1,
  "codigoProduto": "001",
  "nome": "PRODUTO TESTE PARA HOMOLOGACAO",
  "descricao": "Produto para teste",
  "categoria": "Teste",
  "precoVenda": 100.00,
  "unidadeVenda": "UN",
  "ncm": "94035000",
  "cfop": "5102",
  "cstIcms": "00",
  "aliquotaIcms": 18.00,
  "cstIpi": "50",
  "aliquotaIpi": 5.00,
  "cstPis": "01",
  "aliquotaPis": 1.65,
  "cstCofins": "01",
  "aliquotaCofins": 7.60,
  "codigoEan": "7898089225512",
  "informacoesAdicionais": "Produto para teste de homologacao"
}
```

## Funcionalidades Implementadas

1. **Campos Fiscais Completos**: ICMS, IPI, PIS, COFINS
2. **Valores Padrão**: Conforme legislação brasileira
3. **Validação de Dados**: Data Annotations
4. **Índices de Performance**: NCM, categoria, códigos
5. **Relacionamentos**: Com Empresa
6. **CRUD Completo**: Create, Read, Update, Delete
7. **Busca Avançada**: Por código, barras, EAN
8. **Auditoria**: Datas de criação e atualização

## Próximos Passos

1. Executar o script SQL para atualizar a tabela
2. Testar os endpoints via Swagger
3. Implementar validações específicas de NFe
4. Adicionar cálculos automáticos de impostos
5. Implementar integração com SEFAZ
6. Adicionar logs de auditoria fiscal 