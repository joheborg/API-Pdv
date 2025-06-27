# Novos Campos de Endereço - Entidade Empresa

## Visão Geral

Foram adicionados três novos campos na classe `Endereco` da entidade `Empresa` para completar as informações de endereço conforme padrões da SEFAZ.

## Novos Campos Implementados

### 1. Complemento
```csharp
[StringLength(100)]
public string? Complemento { get; set; }
```
- **Descrição**: Complemento do endereço (apartamento, sala, andar, etc.)
- **Tipo**: String opcional
- **Tamanho**: Máximo 100 caracteres
- **Exemplo**: "Sala 101", "Apto 302", "2º andar"

### 2. Código do País
```csharp
[StringLength(10)]
public string? CodigoPais { get; set; } = "1058";
```
- **Descrição**: Código do país conforme tabela da SEFAZ
- **Tipo**: String opcional
- **Valor Padrão**: "1058" (Brasil)
- **Tamanho**: Máximo 10 caracteres
- **Referência**: Tabela de países da SEFAZ

### 3. Nome do País
```csharp
[StringLength(50)]
public string? NomePais { get; set; } = "Brasil";
```
- **Descrição**: Nome do país
- **Tipo**: String opcional
- **Valor Padrão**: "Brasil"
- **Tamanho**: Máximo 50 caracteres

## Arquivos Atualizados

### 1. Entidade (`Domain/Entities/Empresa.cs`)
- ✅ Adicionados os três novos campos na classe `Endereco`
- ✅ Configuração de Data Annotations
- ✅ Valores padrão definidos

### 2. Configuração EF (`Infraestructure/Data/Configurations/EmpresaConfiguration.cs`)
- ✅ Configuração dos novos campos no Entity Framework
- ✅ Valores padrão configurados
- ✅ Tamanhos máximos definidos

### 3. Script SQL (`Utils/empresa_table.sql`)
- ✅ Estrutura atualizada da tabela
- ✅ Novos campos incluídos
- ✅ Valores padrão definidos

### 4. Script de Migração (`Utils/empresa_add_endereco_fields.sql`)
- ✅ Script para adicionar campos em tabela existente
- ✅ Comandos ALTER TABLE específicos
- ✅ Comentários explicativos

## Script SQL para Tabela Existente

Se você já tem a tabela `Empresas` criada, execute este script para adicionar os novos campos:

```sql
-- Adicionar campo Complemento
ALTER TABLE Empresas 
ADD COLUMN Endereco_Complemento VARCHAR(100) AFTER Endereco_Numero;

-- Adicionar campo Código do País
ALTER TABLE Empresas 
ADD COLUMN Endereco_CodigoPais VARCHAR(10) DEFAULT '1058' AFTER Endereco_CEP;

-- Adicionar campo Nome do País
ALTER TABLE Empresas 
ADD COLUMN Endereco_NomePais VARCHAR(50) DEFAULT 'Brasil' AFTER Endereco_CodigoPais;
```

## Exemplo de Uso na API

### Criar Empresa com Endereço Completo
```json
POST /api/v1/empresa
{
  "cnpj": "12345678000199",
  "razaoSocial": "Empresa Exemplo LTDA",
  "nomeFantasia": "Empresa Exemplo",
  "inscricaoEstadual": "123456789",
  "crt": "1",
  "endereco": {
    "logradouro": "Rua das Flores",
    "numero": "123",
    "complemento": "Sala 101, 1º andar",
    "bairro": "Centro",
    "codigoMunicipio": "12345",
    "nomeMunicipio": "São Paulo",
    "uf": "SP",
    "cep": "01234-567",
    "codigoPais": "1058",
    "nomePais": "Brasil"
  }
}
```

### Atualizar Endereço de Empresa Existente
```json
PUT /api/v1/empresa/1
{
  "id": 1,
  "cnpj": "12345678000199",
  "razaoSocial": "Empresa Exemplo LTDA",
  "endereco": {
    "logradouro": "Avenida Paulista",
    "numero": "1000",
    "complemento": "Conjunto 1501",
    "bairro": "Bela Vista",
    "codigoMunicipio": "12345",
    "nomeMunicipio": "São Paulo",
    "uf": "SP",
    "cep": "01310-100",
    "codigoPais": "1058",
    "nomePais": "Brasil"
  }
}
```

## Estrutura Completa do Endereço

```csharp
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
```

## Benefícios dos Novos Campos

1. **Completude de Endereço**: Endereços mais detalhados e completos
2. **Conformidade SEFAZ**: Códigos de país conforme padrões fiscais
3. **Flexibilidade**: Complemento permite especificar detalhes adicionais
4. **Padronização**: Valores padrão para Brasil facilitam o uso
5. **Compatibilidade**: Campos opcionais não quebram funcionalidades existentes

## Próximos Passos

1. Execute o script SQL para adicionar os campos no banco
2. Teste a criação de empresas com endereços completos
3. Verifique se os valores padrão estão sendo aplicados
4. Atualize documentação da API se necessário 