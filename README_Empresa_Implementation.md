# Implementação da Conexão com Banco de Dados - Entidade Empresa

## Visão Geral

Esta documentação explica como foi implementada a conexão com o banco de dados para a entidade `Empresa` na camada de infraestrutura do projeto API-Pdv.

## Estrutura Implementada

### 1. Interface do Repositório (`Interfaces/Repositories/IEmpresa.cs`)
```csharp
public interface IEmpresa
{
    Task<Empresa> GetByIdAsync(int id);
    Task<IEnumerable<Empresa>> GetAllAsync();
    Task<Empresa> CreateAsync(Empresa empresa);
    Task<Empresa> UpdateAsync(Empresa empresa);
    Task DeleteAsync(int id);
    Task<Empresa> GetByCnpjAsync(string cnpj);
}
```

### 2. Implementação do Repositório (`Infraestructure/Repositories/Empresa.cs`)
- Implementa todos os métodos CRUD
- Usa Entity Framework Core para acesso ao banco
- Inclui relacionamentos (Endereco e Produtos)
- Gerencia automaticamente as datas de criação/atualização

### 3. Configuração do Entity Framework (`Infraestructure/Data/Configurations/EmpresaConfiguration.cs`)
- Define a estrutura da tabela no banco
- Configura propriedades complexas (Endereco)
- Define índices e constraints
- Configura relacionamentos

### 4. Controller (`Controller/V1/Empresa.cs`)
- Endpoints RESTful completos
- Tratamento de erros
- Validação de modelo
- Respostas HTTP apropriadas

## Configuração do Banco de Dados

### String de Conexão (`appsettings.json`)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=jonborges.com.br;port=3306;database=pdv;user=prod;password=98143318"
  }
}
```

### Script SQL (`Utils/empresa_table.sql`)
Script para criar a tabela `Empresas` no MySQL com:
- Campos principais da empresa
- Campos de endereço
- Datas de auditoria
- Índices para performance

## Endpoints da API

### GET `/api/v1/empresa`
- Lista todas as empresas
- Inclui endereço e produtos relacionados

### GET `/api/v1/empresa/{id}`
- Busca empresa por ID
- Retorna 404 se não encontrada

### GET `/api/v1/empresa/cnpj/{cnpj}`
- Busca empresa por CNPJ
- Retorna 404 se não encontrada

### POST `/api/v1/empresa`
- Cria nova empresa
- Valida dados obrigatórios
- Retorna empresa criada com ID

### PUT `/api/v1/empresa/{id}`
- Atualiza empresa existente
- Valida ID da URL
- Atualiza data de modificação

### DELETE `/api/v1/empresa/{id}`
- Remove empresa por ID
- Retorna 204 (No Content)

## Exemplo de Uso

### Criar uma Empresa
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
    "complemento": "Sala 101",
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

## Novos Campos de Endereço Adicionados

### Campos Implementados:
- **`Complemento`**: Complemento do endereço (apartamento, sala, etc.)
- **`CodigoPais`**: Código do país conforme tabela da SEFAZ (padrão: "1058" = Brasil)
- **`NomePais`**: Nome do país (padrão: "Brasil")

### Configuração no Banco:
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

## Funcionalidades Implementadas

1. **CRUD Completo**: Create, Read, Update, Delete
2. **Validação de Dados**: Usando Data Annotations
3. **Relacionamentos**: Endereco (owned entity) e Produtos
4. **Auditoria**: Datas de criação e atualização automáticas
5. **Índices**: CNPJ único, índice na razão social
6. **Tratamento de Erros**: Respostas HTTP apropriadas
7. **Async/Await**: Operações assíncronas para melhor performance

## Próximos Passos

1. Executar o script SQL para criar a tabela
2. Testar os endpoints via Swagger
3. Implementar validações de negócio adicionais
4. Adicionar logging para auditoria
5. Implementar paginação para listagem
6. Adicionar filtros de busca 