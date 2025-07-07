# Documentação Completa - API PDV

## 📋 Visão Geral

A API PDV é uma solução completa para sistemas de Point of Sale (PDV) desenvolvida em ASP.NET Core com Entity Framework Core e MySQL. A API oferece funcionalidades completas para gestão de produtos, pedidos, usuários, empresas e muito mais. A API utiliza autenticação JWT e associa automaticamente todas as operações à empresa do usuário logado.

### 🚀 Características Principais

- **Autenticação JWT**: Sistema seguro de autenticação
- **RESTful API**: Endpoints padronizados REST
- **Swagger/OpenAPI**: Documentação interativa
- **Entity Framework Core**: ORM para MySQL
- **Validação de Dados**: Validação automática de entrada
- **Tratamento de Erros**: Respostas padronizadas de erro

## 🔐 Autenticação

### Sistema JWT

A API utiliza autenticação JWT (JSON Web Token) para proteger os endpoints. Todos os endpoints, exceto login, requerem um token válido.

#### Configuração JWT

```json
{
  "Jwt": {
    "Key": "SuaChaveSecretaAqui123456789",
    "Issuer": "WebPdv",
    "Audience": "WebPdv"
  }
}
```

#### Claims do Token

- `nameidentifier`: ID do usuário
- `name`: Nome do usuário
- `email`: Email do usuário
- `perfil`: Perfil/role do usuário
- `empresaId`: ID da empresa associada
- `nomeEmpresa`: Nome da empresa

### Como Usar

```bash
# Login
curl -X POST "https://api-pdv.com/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@empresa.com", "senha": "123456"}'

# Usar token
curl -X GET "https://api-pdv.com/api/v1/produtos" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"
```

## 📡 Endpoints da API

### 🔑 Autenticação

#### POST `/api/v1/auth/login`

**Descrição**: Autentica um usuário e retorna um token JWT.

**Request Body**:
```json
{
  "email": "admin@empresa.com",
  "senha": "123456"
}
```

**Response de Sucesso (200)**:
```json
{
  "sucesso": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "mensagem": "Login realizado com sucesso",
  "usuario": {
    "id": 1,
    "nome": "Administrador",
    "email": "admin@empresa.com",
    "perfil": "Admin",
    "empresaId": 1,
    "nomeEmpresa": "Empresa Teste LTDA"
  }
}
```

**Response de Erro (401)**:
```json
{
  "sucesso": false,
  "mensagem": "Email ou senha inválidos"
}
```

#### POST `/api/v1/auth/logout`

**Descrição**: Endpoint para logout (implementação básica).

**Response (200)**:
```json
{
  "sucesso": true,
  "mensagem": "Logout realizado com sucesso"
}
```

### 🛍️ Produtos

#### GET `/api/v1/produtos`

**Descrição**: Lista todos os produtos.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
[
  {
    "id": 1,
    "nome": "Produto Teste",
    "descricao": "Descrição do produto",
    "preco": 10.50,
    "codigoProduto": "PROD001",
    "codigoBarras": "7891234567890",
    "codigoEan": "7891234567890",
    "categoriaId": 1,
    "empresaId": 1,
    "ativo": true,
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-01T00:00:00"
  }
]
```

#### GET `/api/v1/produtos/{id}`

**Descrição**: Busca um produto por ID.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
{
  "id": 1,
  "nome": "Produto Teste",
  "descricao": "Descrição do produto",
  "preco": 10.50,
  "codigoProduto": "PROD001",
  "codigoBarras": "7891234567890",
  "codigoEan": "7891234567890",
  "categoriaId": 1,
  "empresaId": 1,
  "ativo": true,
  "createdAt": "2024-01-01T00:00:00",
  "updatedAt": "2024-01-01T00:00:00"
}
```

#### GET `/api/v1/produtos/empresa/{empresaId}`

**Descrição**: Lista produtos por empresa.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/produtos/codigo/{empresaId}/{codigoProduto}`

**Descrição**: Busca produto por código na empresa.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/produtos/barras/{codigoBarras}`

**Descrição**: Busca produto por código de barras.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/produtos/ean/{codigoEan}`

**Descrição**: Busca produto por código EAN.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/produtos/categoria/{categoriaId}`

**Descrição**: Lista produtos por categoria.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/produtos`

**Descrição**: Cria um novo produto.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "nome": "Novo Produto",
  "descricao": "Descrição do novo produto",
  "preco": 15.99,
  "codigoProduto": "PROD002",
  "codigoBarras": "7891234567891",
  "codigoEan": "7891234567891",
  "categoriaId": 1,
  "empresaId": 1,
  "ativo": true
}
```

**Response (201)**:
```json
{
  "id": 2,
  "nome": "Novo Produto",
  "descricao": "Descrição do novo produto",
  "preco": 15.99,
  "codigoProduto": "PROD002",
  "codigoBarras": "7891234567891",
  "codigoEan": "7891234567891",
  "categoriaId": 1,
  "empresaId": 1,
  "ativo": true,
  "createdAt": "2024-01-01T00:00:00",
  "updatedAt": "2024-01-01T00:00:00"
}
```

#### PUT `/api/v1/produtos/{id}`

**Descrição**: Atualiza um produto existente.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "id": 1,
  "nome": "Produto Atualizado",
  "descricao": "Descrição atualizada",
  "preco": 12.50,
  "codigoProduto": "PROD001",
  "codigoBarras": "7891234567890",
  "codigoEan": "7891234567890",
  "categoriaId": 1,
  "empresaId": 1,
  "ativo": true
}
```

#### DELETE `/api/v1/produtos/{id}`

**Descrição**: Remove um produto.

**Headers**: `Authorization: Bearer {token}`

**Response (204)**: No Content

### 📋 Pedidos

#### GET `/api/v1/pedidos`

**Descrição**: Lista todos os pedidos.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
[
  {
    "id": 1,
    "numeroComanda": "CMD001",
    "clienteId": 1,
    "empresaId": 1,
    "statusId": 1,
    "valorTotal": 25.50,
    "observacoes": "Sem cebola",
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-01T00:00:00"
  }
]
```

#### GET `/api/v1/pedidos/{id}`

**Descrição**: Busca um pedido por ID.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/pedidos/comanda/{empresaId}/{numeroComanda}`

**Descrição**: Busca pedido por número da comanda na empresa.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/pedidos/abertos/{empresaId}`

**Descrição**: Lista pedidos abertos da empresa.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/pedidos`

**Descrição**: Cria um novo pedido.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "numeroComanda": "CMD002",
  "clienteId": 1,
  "empresaId": 1,
  "statusId": 1,
  "valorTotal": 30.00,
  "observacoes": "Pedido especial"
}
```

#### PUT `/api/v1/pedidos/{id}`

**Descrição**: Atualiza um pedido existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/pedidos/{id}`

**Descrição**: Remove um pedido.

**Headers**: `Authorization: Bearer {token}`

### 👥 Usuários

#### GET `/api/v1/usuarios`

**Descrição**: Lista todos os usuários.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
[
  {
    "id": 1,
    "nome": "Administrador",
    "email": "admin@empresa.com",
    "senha": "hash_da_senha",
    "perfil": "Admin",
    "empresaId": 1,
    "ativo": true,
    "ultimoAcesso": "2024-01-01T00:00:00",
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-01T00:00:00"
  }
]
```

#### GET `/api/v1/usuarios/{id}`

**Descrição**: Busca um usuário por ID.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/usuarios/empresa/{empresaId}`

**Descrição**: Lista usuários por empresa.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/usuarios/ativos`

**Descrição**: Lista apenas usuários ativos.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/usuarios/email/{email}`

**Descrição**: Busca usuário por email.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/usuarios`

**Descrição**: Cria um novo usuário.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "nome": "Novo Usuário",
  "email": "usuario@empresa.com",
  "senha": "123456",
  "perfil": "Usuario",
  "empresaId": 1,
  "ativo": true
}
```

#### PUT `/api/v1/usuarios/{id}`

**Descrição**: Atualiza um usuário existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/usuarios/{id}`

**Descrição**: Remove um usuário.

**Headers**: `Authorization: Bearer {token}`

#### PUT `/api/v1/usuarios/{id}/ativar`

**Descrição**: Ativa um usuário.

**Headers**: `Authorization: Bearer {token}`

#### PUT `/api/v1/usuarios/{id}/desativar`

**Descrição**: Desativa um usuário.

**Headers**: `Authorization: Bearer {token}`

#### PUT `/api/v1/usuarios/{id}/alterar-senha`

**Descrição**: Altera a senha de um usuário.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "senhaAtual": "123456",
  "novaSenha": "654321"
}
```

### 🏢 Empresas

#### GET `/api/v1/empresas`

**Descrição**: Lista todas as empresas.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
[
  {
    "id": 1,
    "razaoSocial": "Empresa Teste LTDA",
    "nomeFantasia": "Empresa Teste",
    "cnpj": "12.345.678/0001-90",
    "inscricaoEstadual": "123456789",
    "endereco": "Rua Teste, 123",
    "bairro": "Centro",
    "cidade": "São Paulo",
    "estado": "SP",
    "cep": "01234-567",
    "telefone": "(11) 1234-5678",
    "email": "contato@empresa.com",
    "ativo": true,
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-01T00:00:00"
  }
]
```

#### GET `/api/v1/empresas/{id}`

**Descrição**: Busca uma empresa por ID.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/empresas/cnpj/{cnpj}`

**Descrição**: Busca empresa por CNPJ.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/empresas`

**Descrição**: Cria uma nova empresa.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "razaoSocial": "Nova Empresa LTDA",
  "nomeFantasia": "Nova Empresa",
  "cnpj": "98.765.432/0001-10",
  "inscricaoEstadual": "987654321",
  "endereco": "Rua Nova, 456",
  "bairro": "Vila Nova",
  "cidade": "Rio de Janeiro",
  "estado": "RJ",
  "cep": "20000-000",
  "telefone": "(21) 9876-5432",
  "email": "contato@novaempresa.com",
  "ativo": true
}
```

#### PUT `/api/v1/empresas/{id}`

**Descrição**: Atualiza uma empresa existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/empresas/{id}`

**Descrição**: Remove uma empresa.

**Headers**: `Authorization: Bearer {token}`

### 📂 Categorias

#### GET `/api/v1/categorias`

**Descrição**: Lista todas as categorias.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
[
  {
    "id": 1,
    "nome": "Bebidas",
    "descricao": "Categoria de bebidas",
    "empresaId": 1,
    "ativo": true,
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-01T00:00:00"
  }
]
```

#### GET `/api/v1/categorias/{id}`

**Descrição**: Busca uma categoria por ID.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/categorias`

**Descrição**: Cria uma nova categoria.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "nome": "Sobremesas",
  "descricao": "Categoria de sobremesas",
  "empresaId": 1,
  "ativo": true
}
```

#### PUT `/api/v1/categorias/{id}`

**Descrição**: Atualiza uma categoria existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/categorias/{id}`

**Descrição**: Remove uma categoria.

**Headers**: `Authorization: Bearer {token}`

### 📊 Status de Pedidos

#### GET `/api/v1/statuspedidos`

**Descrição**: Lista todos os status de pedidos.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
[
  {
    "id": 1,
    "nome": "Aberto",
    "descricao": "Pedido aberto",
    "cor": "#FF0000",
    "empresaId": 1,
    "ativo": true,
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-01T00:00:00"
  }
]
```

#### GET `/api/v1/statuspedidos/{id}`

**Descrição**: Busca um status por ID.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/statuspedidos`

**Descrição**: Cria um novo status.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "nome": "Em Preparação",
  "descricao": "Pedido em preparação",
  "cor": "#FFA500",
  "empresaId": 1,
  "ativo": true
}
```

#### PUT `/api/v1/statuspedidos/{id}`

**Descrição**: Atualiza um status existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/statuspedidos/{id}`

**Descrição**: Remove um status.

**Headers**: `Authorization: Bearer {token}`

### 🛒 Itens de Pedido

#### GET `/api/v1/itempedidos`

**Descrição**: Lista todos os itens de pedido.

**Headers**: `Authorization: Bearer {token}`

**Response (200)**:
```json
[
  {
    "id": 1,
    "pedidoId": 1,
    "produtoId": 1,
    "quantidade": 2,
    "precoUnitario": 10.50,
    "precoTotal": 21.00,
    "observacoes": "Sem cebola",
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-01T00:00:00"
  }
]
```

#### GET `/api/v1/itempedidos/{id}`

**Descrição**: Busca um item de pedido por ID.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/itempedidos`

**Descrição**: Cria um novo item de pedido.

**Headers**: `Authorization: Bearer {token}`

**Request Body**:
```json
{
  "pedidoId": 1,
  "produtoId": 1,
  "quantidade": 3,
  "precoUnitario": 10.50,
  "precoTotal": 31.50,
  "observacoes": "Bem passado"
}
```

#### PUT `/api/v1/itempedidos/{id}`

**Descrição**: Atualiza um item de pedido existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/itempedidos/{id}`

**Descrição**: Remove um item de pedido.

**Headers**: `Authorization: Bearer {token}`

### 💰 Caixas

#### GET `/api/v1/caixas`

**Descrição**: Lista todos os caixas.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/caixas/{id}`

**Descrição**: Busca um caixa por ID.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/caixas`

**Descrição**: Cria um novo caixa.

**Headers**: `Authorization: Bearer {token}`

#### PUT `/api/v1/caixas/{id}`

**Descrição**: Atualiza um caixa existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/caixas/{id}`

**Descrição**: Remove um caixa.

**Headers**: `Authorization: Bearer {token}`

### 🚚 Motoboys

#### GET `/api/v1/motoboys`

**Descrição**: Lista todos os motoboys.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/motoboys/{id}`

**Descrição**: Busca um motoboy por ID.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/motoboys`

**Descrição**: Cria um novo motoboy.

**Headers**: `Authorization: Bearer {token}`

#### PUT `/api/v1/motoboys/{id}`

**Descrição**: Atualiza um motoboy existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/motoboys/{id}`

**Descrição**: Remove um motoboy.

**Headers**: `Authorization: Bearer {token}`

### 💳 Pagamentos de Caixa

#### GET `/api/v1/pagamentocaixas`

**Descrição**: Lista todos os pagamentos de caixa.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/pagamentocaixas/{id}`

**Descrição**: Busca um pagamento de caixa por ID.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/pagamentocaixas`

**Descrição**: Cria um novo pagamento de caixa.

**Headers**: `Authorization: Bearer {token}`

#### PUT `/api/v1/pagamentocaixas/{id}`

**Descrição**: Atualiza um pagamento de caixa existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/pagamentocaixas/{id}`

**Descrição**: Remove um pagamento de caixa.

**Headers**: `Authorization: Bearer {token}`

### 👨‍💼 Funcionários

#### GET `/api/v1/funcionarios`

**Descrição**: Lista todos os funcionários.

**Headers**: `Authorization: Bearer {token}`

#### GET `/api/v1/funcionarios/{id}`

**Descrição**: Busca um funcionário por ID.

**Headers**: `Authorization: Bearer {token}`

#### POST `/api/v1/funcionarios`

**Descrição**: Cria um novo funcionário.

**Headers**: `Authorization: Bearer {token}`

#### PUT `/api/v1/funcionarios/{id}`

**Descrição**: Atualiza um funcionário existente.

**Headers**: `Authorization: Bearer {token}`

#### DELETE `/api/v1/funcionarios/{id}`

**Descrição**: Remove um funcionário.

**Headers**: `Authorization: Bearer {token}`

## 📊 Códigos de Status HTTP

### Sucesso
- `200 OK`: Requisição bem-sucedida
- `201 Created`: Recurso criado com sucesso
- `204 No Content`: Requisição bem-sucedida sem conteúdo

### Erro do Cliente
- `400 Bad Request`: Dados de entrada inválidos
- `401 Unauthorized`: Não autorizado (token ausente ou inválido)
- `403 Forbidden`: Proibido (token válido mas sem permissão)
- `404 Not Found`: Recurso não encontrado

### Erro do Servidor
- `500 Internal Server Error`: Erro interno do servidor

## 🔧 Configuração

### Arquivo appsettings.json

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

### Dependências

```xml
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="8.0.0" />
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="8.0.0" />
<PackageReference Include="Swashbuckle.AspNetCore" Version="6.5.0" />
```

## 🚀 Como Executar

### Pré-requisitos

- .NET 9.0 SDK
- MySQL Server
- Visual Studio 2022 ou VS Code

### Passos

1. **Clone o repositório**
```bash
git clone https://github.com/seu-usuario/api-pdv.git
cd api-pdv
```

2. **Configure o banco de dados**
```bash
# Atualize a connection string no appsettings.json
# Execute as migrações
dotnet ef database update
```

3. **Execute a aplicação**
```bash
dotnet run
```

4. **Acesse a documentação**
```
http://localhost:5193/swagger
```

## 📝 Exemplos de Uso

### JavaScript (Fetch API)

```javascript
// Login
async function login(email, senha) {
  const response = await fetch('/api/v1/auth/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({ email, senha })
  });
  
  const data = await response.json();
  if (data.sucesso) {
    localStorage.setItem('token', data.token);
    return data;
  }
  throw new Error(data.mensagem);
}

// Buscar produtos
async function getProdutos() {
  const token = localStorage.getItem('token');
  const response = await fetch('/api/v1/produtos', {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return await response.json();
}

// Criar produto
async function createProduto(produto) {
  const token = localStorage.getItem('token');
  const response = await fetch('/api/v1/produtos', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(produto)
  });
  return await response.json();
}
```

### Python (Requests)

```python
import requests

class APIPDV:
    def __init__(self, base_url):
        self.base_url = base_url
        self.token = None
    
    def login(self, email, senha):
        response = requests.post(f'{self.base_url}/api/v1/auth/login', 
                               json={'email': email, 'senha': senha})
        data = response.json()
        if data['sucesso']:
            self.token = data['token']
            return data
        raise Exception(data['mensagem'])
    
    def get_produtos(self):
        headers = {'Authorization': f'Bearer {self.token}'}
        response = requests.get(f'{self.base_url}/api/v1/produtos', 
                              headers=headers)
        return response.json()
    
    def create_produto(self, produto):
        headers = {
            'Content-Type': 'application/json',
            'Authorization': f'Bearer {self.token}'
        }
        response = requests.post(f'{self.base_url}/api/v1/produtos', 
                               json=produto, headers=headers)
        return response.json()

# Uso
api = APIPDV('https://api-pdv.com')
api.login('admin@empresa.com', '123456')
produtos = api.get_produtos()
```

### cURL

```bash
# Login
curl -X POST "https://api-pdv.com/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@empresa.com", "senha": "123456"}'

# Buscar produtos
curl -X GET "https://api-pdv.com/api/v1/produtos" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"

# Criar produto
curl -X POST "https://api-pdv.com/api/v1/produtos" \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI" \
  -d '{
    "nome": "Novo Produto",
    "descricao": "Descrição do produto",
    "preco": 15.99,
    "codigoProduto": "PROD002",
    "categoriaId": 1,
    "empresaId": 1,
    "ativo": true
  }'
```

## 🔒 Segurança

### Boas Práticas

1. **HTTPS**: Sempre use HTTPS em produção
2. **Tokens**: Armazene tokens de forma segura
3. **Validação**: Sempre valide dados de entrada
4. **Logs**: Mantenha logs de auditoria
5. **Backup**: Faça backup regular do banco de dados

### Configuração de Produção

```json
{
  "Jwt": {
    "Key": "ChaveSecretaMuitoLongaEComplexaParaProducao123456789",
    "Issuer": "https://api-pdv.com",
    "Audience": "https://api-pdv.com"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=prod-server;Database=pdv_prod;Uid=prod_user;Pwd=prod_password;"
  }
}
```

## 🐛 Troubleshooting

### Problemas Comuns

1. **Erro de conexão com banco**
   - Verifique a connection string
   - Confirme se o MySQL está rodando
   - Execute `dotnet ef database update`

2. **Erro de autenticação**
   - Verifique se o token está correto
   - Confirme se o token não expirou
   - Valide a configuração JWT

3. **Erro de CORS**
   - Configure CORS adequadamente
   - Verifique os headers de requisição

### Logs

Para debugar problemas, verifique os logs da aplicação:

```bash
dotnet run --environment Development
```

## 📞 Suporte

Para suporte técnico ou dúvidas sobre a API:

- **Email**: suporte@api-pdv.com
- **Documentação**: https://docs.api-pdv.com
- **GitHub**: https://github.com/seu-usuario/api-pdv

## 📄 Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo LICENSE para mais detalhes. 