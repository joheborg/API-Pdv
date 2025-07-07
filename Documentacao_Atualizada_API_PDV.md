# Documentação Atualizada - API PDV

## 📋 Visão Geral

A API PDV é uma solução completa para sistemas de Point of Sale (PDV) desenvolvida em ASP.NET Core com Entity Framework Core e MySQL. A API oferece funcionalidades completas para gestão de produtos, pedidos, usuários, empresas e muito mais.

### 🚀 Características Principais

- ✅ **Autenticação JWT** - Sistema seguro de login
- ✅ **Associação Automática de Empresa** - Empresa automaticamente vinculada ao usuário logado
- ✅ **CRUD Completo** - Todas as operações básicas implementadas
- ✅ **Filtros Automáticos** - Dados filtrados por empresa do usuário
- ✅ **Validações de Segurança** - Controle de acesso por empresa
- ✅ **Documentação Completa** - Guias detalhados de uso

## 🔐 Autenticação

### Login
```http
POST /api/v1/auth/login
Content-Type: application/json

{
  "email": "usuario@exemplo.com",
  "senha": "senha123"
}
```

**Resposta:**
```json
{
  "sucesso": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "usuario": {
    "id": 1,
    "nome": "João Silva",
    "email": "usuario@exemplo.com",
    "perfil": "Admin",
    "empresaId": 1,
    "nomeEmpresa": "Empresa Exemplo Ltda"
  }
}
```

### Uso do Token
Inclua o token no header Authorization:
```
Authorization: Bearer {token}
```

## 🏢 Associação Automática de Empresa

### Como Funciona
- A empresa é automaticamente extraída do token JWT do usuário logado
- Não é necessário especificar `empresaId` nos requests
- Todos os dados são automaticamente filtrados pela empresa do usuário
- Novos registros são automaticamente associados à empresa correta

### Exemplo de Uso
```http
POST /api/v1/produto
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Hambúrguer",
  "precoVenda": 25.90,
  "categoriaId": 1
}
```
A empresa será automaticamente associada ao produto baseada no token do usuário.

## 📡 Endpoints

### 🔐 Autenticação

#### Login
```http
POST /api/v1/auth/login
Content-Type: application/json

{
  "email": "usuario@exemplo.com",
  "senha": "senha123"
}
```

### 📦 Produtos

#### Listar Produtos da Empresa
```http
GET /api/v1/produto
Authorization: Bearer {token}
```

**Resposta:**
```json
[
  {
    "id": 1,
    "codigoProduto": "PROD001",
    "nome": "Hambúrguer",
    "descricao": "Hambúrguer artesanal",
    "precoVenda": 25.90,
    "categoriaId": 1,
    "empresaId": 1,
    "situacao": true
  }
]
```

#### Buscar Produto por Código
```http
GET /api/v1/produto/codigo/{codigoProduto}
Authorization: Bearer {token}
```

#### Buscar Produto por Código de Barras
```http
GET /api/v1/produto/barras/{codigoBarras}
Authorization: Bearer {token}
```

#### Buscar Produto por Código EAN
```http
GET /api/v1/produto/ean/{codigoEan}
Authorization: Bearer {token}
```

#### Buscar Produtos por Categoria
```http
GET /api/v1/produto/categoria/{categoriaId}
Authorization: Bearer {token}
```

#### Criar Produto
```http
POST /api/v1/produto
Authorization: Bearer {token}
Content-Type: application/json

{
  "codigoProduto": "PROD001",
  "nome": "Hambúrguer",
  "descricao": "Hambúrguer artesanal",
  "precoVenda": 25.90,
  "precoCusto": 15.00,
  "categoriaId": 1,
  "codigoBarras": "7891234567890",
  "unidadeVenda": "UN"
}
```

#### Atualizar Produto
```http
PUT /api/v1/produto/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": 1,
  "nome": "Hambúrguer Atualizado",
  "precoVenda": 29.90,
  "descricao": "Hambúrguer artesanal premium"
}
```

#### Deletar Produto
```http
DELETE /api/v1/produto/{id}
Authorization: Bearer {token}
```

### 🍽️ Pedidos

#### Listar Pedidos da Empresa
```http
GET /api/v1/pedido
Authorization: Bearer {token}
```

**Resposta:**
```json
[
  {
    "id": 1,
    "numeroComanda": "001",
    "dataPedido": "2024-01-15T10:30:00",
    "status": "Pendente",
    "observacoes": "Sem cebola",
    "empresaId": 1,
    "itensPedido": [
      {
        "id": 1,
        "produtoId": 1,
        "quantidade": 2,
        "precoUnitario": 25.90
      }
    ]
  }
]
```

#### Buscar Pedido por ID
```http
GET /api/v1/pedido/{id}
Authorization: Bearer {token}
```

#### Buscar Pedido por Comanda
```http
GET /api/v1/pedido/comanda/{numeroComanda}
Authorization: Bearer {token}
```

#### Listar Pedidos Abertos
```http
GET /api/v1/pedido/abertos
Authorization: Bearer {token}
```

#### Criar Pedido
```http
POST /api/v1/pedido
Authorization: Bearer {token}
Content-Type: application/json

{
  "numeroComanda": "001",
  "observacoes": "Sem cebola",
  "itensPedido": [
    {
      "produtoId": 1,
      "quantidade": 2,
      "precoUnitario": 25.90
    }
  ]
}
```

#### Atualizar Pedido
```http
PUT /api/v1/pedido/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": 1,
  "status": "Pronto",
  "observacoes": "Pedido finalizado"
}
```

#### Deletar Pedido
```http
DELETE /api/v1/pedido/{id}
Authorization: Bearer {token}
```

### 📂 Categorias

#### Listar Categorias
```http
GET /api/v1/categoria
Authorization: Bearer {token}
```

#### Buscar Categoria por ID
```http
GET /api/v1/categoria/{id}
Authorization: Bearer {token}
```

#### Criar Categoria
```http
POST /api/v1/categoria
Authorization: Bearer {token}
Content-Type: application/json

{
  "descricao": "Bebidas"
}
```

#### Atualizar Categoria
```http
PUT /api/v1/categoria/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": 1,
  "descricao": "Bebidas Alcoólicas"
}
```

#### Deletar Categoria
```http
DELETE /api/v1/categoria/{id}
Authorization: Bearer {token}
```

### 💰 Caixas

#### Listar Caixas da Empresa
```http
GET /api/v1/caixa
Authorization: Bearer {token}
```

**Resposta:**
```json
[
  {
    "id": 1,
    "empresaId": 1,
    "dataAbertura": "2024-01-15T08:00:00",
    "dataFechamento": null,
    "valorAbertura": 100.00,
    "valorFechamento": 0.00,
    "status": "aberto",
    "observacao": "Abertura do caixa"
  }
]
```

#### Buscar Caixa por ID
```http
GET /api/v1/caixa/{id}
Authorization: Bearer {token}
```

#### Criar Caixa
```http
POST /api/v1/caixa
Authorization: Bearer {token}
Content-Type: application/json

{
  "valorAbertura": 100.00,
  "observacao": "Abertura do caixa"
}
```

#### Atualizar Caixa
```http
PUT /api/v1/caixa/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": 1,
  "valorFechamento": 1500.00,
  "status": "fechado",
  "dataFechamento": "2024-01-15T18:00:00"
}
```

#### Deletar Caixa
```http
DELETE /api/v1/caixa/{id}
Authorization: Bearer {token}
```

### 🛵 Motoboys

#### Listar Motoboys da Empresa
```http
GET /api/v1/motoboy
Authorization: Bearer {token}
```

**Resposta:**
```json
[
  {
    "id": 1,
    "empresaId": 1,
    "nome": "João Silva",
    "documento": "12345678901",
    "telefone": "11999999999",
    "veiculo": "Moto",
    "placa": "ABC1234",
    "status": "ativo"
  }
]
```

#### Buscar Motoboy por ID
```http
GET /api/v1/motoboy/{id}
Authorization: Bearer {token}
```

#### Criar Motoboy
```http
POST /api/v1/motoboy
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "João Silva",
  "documento": "12345678901",
  "telefone": "11999999999",
  "veiculo": "Moto",
  "placa": "ABC1234"
}
```

#### Atualizar Motoboy
```http
PUT /api/v1/motoboy/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": 1,
  "nome": "João Silva Santos",
  "status": "inativo"
}
```

#### Deletar Motoboy
```http
DELETE /api/v1/motoboy/{id}
Authorization: Bearer {token}
```

### 👥 Usuários

#### Listar Usuários
```http
GET /api/v1/usuario
Authorization: Bearer {token}
```

#### Buscar Usuário por ID
```http
GET /api/v1/usuario/{id}
Authorization: Bearer {token}
```

#### Buscar Usuários por Empresa
```http
GET /api/v1/usuario/empresa/{empresaId}
Authorization: Bearer {token}
```

#### Criar Usuário
```http
POST /api/v1/usuario
Authorization: Bearer {token}
Content-Type: application/json

{
  "nome": "Maria Silva",
  "email": "maria@exemplo.com",
  "senha": "senha123",
  "perfil": "Operador",
  "empresaId": 1
}
```

#### Atualizar Usuário
```http
PUT /api/v1/usuario/{id}
Authorization: Bearer {token}
Content-Type: application/json

{
  "id": 1,
  "nome": "Maria Silva Santos",
  "perfil": "Gerente"
}
```

#### Deletar Usuário
```http
DELETE /api/v1/usuario/{id}
Authorization: Bearer {token}
```

## ⚙️ Configuração

### appsettings.json
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=pdv_db;Uid=root;Pwd=sua_senha;"
  },
  "Jwt": {
    "Key": "SuaChaveSecretaAqui123456789",
    "Issuer": "WebPdv",
    "Audience": "WebPdv"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## 🗄️ Estrutura do Banco de Dados

### Tabelas Principais

#### Usuarios
- `id` (PK)
- `nome`
- `email`
- `senha`
- `perfil`
- `ativo`
- `empresaId` (FK) - **Associação automática**
- `funcionarioId` (FK)
- `createdAt`
- `updatedAt`

#### Empresas
- `id` (PK)
- `cnpj`
- `razaoSocial`
- `nomeFantasia`
- `inscricaoEstadual`
- `crt`
- `logoBase64`
- `logoNome`
- `logoMimeType`
- `endereco` (propriedade complexa)
- `createdAt`
- `updatedAt`

#### Produtos
- `id` (PK)
- `codigoProduto`
- `nome`
- `descricao`
- `precoVenda`
- `precoCusto`
- `categoriaId` (FK)
- `empresaId` (FK) - **Associação automática**
- `situacao`
- `createdAt`
- `updatedAt`

#### Pedidos
- `id` (PK)
- `numeroComanda`
- `dataPedido`
- `status`
- `observacoes`
- `empresaId` (FK) - **Associação automática**
- `clienteId` (FK)
- `situacaoId` (FK)
- `createdAt`
- `updatedAt`

#### Caixas
- `id` (PK)
- `empresaId` (FK) - **Associação automática**
- `dataAbertura`
- `dataFechamento`
- `valorAbertura`
- `valorFechamento`
- `status`
- `observacao`

#### Motoboys
- `id` (PK)
- `empresaId` (FK) - **Associação automática**
- `nome`
- `documento`
- `telefone`
- `veiculo`
- `placa`
- `status`
- `observacao`
- `createdAt`
- `updatedAt`

## 🔒 Segurança

### Autenticação JWT
- Tokens válidos por 8 horas
- Claims incluem: ID do usuário, nome, email, perfil, empresaId
- Todos os endpoints (exceto login) requerem autenticação

### Associação Automática de Empresa
- Todos os dados são automaticamente associados à empresa do usuário logado
- Não é necessário especificar empresaId nos requests
- A API valida se o usuário possui empresa associada
- Filtros automáticos por empresa em todas as consultas

### Validações
- Validação de modelo em todos os endpoints
- Verificação de existência de entidades relacionadas
- Tratamento de erros com mensagens descritivas
- Validação de empresa associada ao usuário

## 💻 Exemplos de Uso

### JavaScript (Fetch)
```javascript
// Login
const loginResponse = await fetch('/api/v1/auth/login', {
  method: 'POST',
  headers: { 'Content-Type': 'application/json' },
  body: JSON.stringify({
    email: 'usuario@exemplo.com',
    senha: 'senha123'
  })
});

const { token } = await loginResponse.json();

// Criar produto (empresa automática)
const produtoResponse = await fetch('/api/v1/produto', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
    'Authorization': `Bearer ${token}`
  },
  body: JSON.stringify({
    codigoProduto: 'PROD001',
    nome: 'Hambúrguer',
    precoVenda: 25.90,
    categoriaId: 1
  })
});

// Listar produtos (filtrado por empresa)
const produtosResponse = await fetch('/api/v1/produto', {
  headers: { 'Authorization': `Bearer ${token}` }
});
```

### Python (requests)
```python
import requests

# Login
login_data = {
    'email': 'usuario@exemplo.com',
    'senha': 'senha123'
}
response = requests.post('http://localhost:5000/api/v1/auth/login', json=login_data)
token = response.json()['token']

# Criar produto (empresa automática)
headers = {'Authorization': f'Bearer {token}'}
produto_data = {
    'codigoProduto': 'PROD001',
    'nome': 'Hambúrguer',
    'precoVenda': 25.90,
    'categoriaId': 1
}
response = requests.post('http://localhost:5000/api/v1/produto', 
                        json=produto_data, headers=headers)

# Listar produtos (filtrado por empresa)
produtos = requests.get('http://localhost:5000/api/v1/produto', headers=headers)
```

### cURL
```bash
# Login
curl -X POST http://localhost:5000/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"usuario@exemplo.com","senha":"senha123"}'

# Criar produto (empresa automática)
curl -X POST http://localhost:5000/api/v1/produto \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {token}" \
  -d '{"codigoProduto":"PROD001","nome":"Hambúrguer","precoVenda":25.90}'

# Listar produtos (filtrado por empresa)
curl -X GET http://localhost:5000/api/v1/produto \
  -H "Authorization: Bearer {token}"
```

## 🚨 Troubleshooting

### Erros Comuns

#### 401 Unauthorized
- Token expirado ou inválido
- Header Authorization ausente ou mal formatado

#### 400 Bad Request
- Dados de entrada inválidos
- Usuário sem empresa associada
- Validações de modelo falharam

#### 404 Not Found
- Recurso não encontrado
- ID inválido fornecido

#### 500 Internal Server Error
- Erro interno do servidor
- Problemas de conexão com banco de dados

### Logs
- Verifique os logs da aplicação para detalhes de erros
- Configure logging apropriado em appsettings.json

## 🚀 Deploy

### Requisitos
- .NET 9.0
- MySQL 8.0+
- Configuração adequada de CORS
- Variáveis de ambiente para produção

### Docker
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY bin/Release/net9.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "API-Pdv.dll"]
```

### Variáveis de Ambiente
```bash
ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection=Server=db;Database=pdv_db;Uid=user;Pwd=password;
Jwt__Key=SuaChaveSecretaMuitoSeguraAqui
```

## 📝 Changelog

### v2.0.0 - Associação Automática de Empresa
- ✅ Implementação da associação automática de empresa
- ✅ Criação do UserHelper para extrair dados do token JWT
- ✅ Atualização de todos os controllers principais
- ✅ Adição de métodos GetByEmpresaAsync nos repositórios
- ✅ Validações de segurança por empresa
- ✅ Documentação completa atualizada

### v1.0.0 - Implementação Inicial
- ✅ Autenticação JWT
- ✅ CRUD completo para todas as entidades
- ✅ Documentação inicial

## 🔄 Migração

### Para Frontend
1. **Remover parâmetros empresaId** dos requests
2. **Atualizar URLs** que incluíam empresaId
3. **Garantir** que o token JWT está sendo enviado corretamente

### Exemplo de Migração

**Antes:**
```javascript
// Buscar produtos
fetch(`/api/v1/produto/empresa/${empresaId}`)

// Criar produto
fetch('/api/v1/produto', {
  body: JSON.stringify({
    ...produto,
    empresaId: empresaId
  })
})
```

**Depois:**
```javascript
// Buscar produtos
fetch('/api/v1/produto')

// Criar produto
fetch('/api/v1/produto', {
  body: JSON.stringify(produto)
})
```

## 🧪 Testes

### Cenários de Teste

1. **Usuário com empresa válida**
   - Deve conseguir criar/consultar dados
   - Dados devem ser associados à empresa correta

2. **Usuário sem empresa**
   - Deve receber erro 400 "Usuário não possui empresa associada"

3. **Token inválido**
   - Deve receber erro 401 "Unauthorized"

4. **Token expirado**
   - Deve receber erro 401 "Unauthorized"

### Exemplos de Teste

```bash
# Login
curl -X POST http://localhost:5000/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{"email":"usuario@exemplo.com","senha":"senha123"}'

# Criar produto (empresa automática)
curl -X POST http://localhost:5000/api/v1/produto \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer {token}" \
  -d '{"nome":"Produto Teste","precoVenda":10.00}'

# Listar produtos (filtrado por empresa)
curl -X GET http://localhost:5000/api/v1/produto \
  -H "Authorization: Bearer {token}"
```

## 📞 Suporte

Para dúvidas ou problemas:
1. Verifique a documentação completa
2. Consulte os logs da aplicação
3. Teste os endpoints com exemplos fornecidos
4. Verifique a configuração do banco de dados

---

**Última atualização:** Janeiro 2024  
**Versão:** 2.0.0  
**Status:** ✅ Produção 