# Documentação de Autenticação - API PDV

## Visão Geral

A API PDV utiliza autenticação JWT (JSON Web Token) para proteger os endpoints. Todos os endpoints, exceto o login, requerem um token válido no header de autorização.

## Configuração JWT

### Parâmetros de Configuração

```json
{
  "Jwt": {
    "Key": "SuaChaveSecretaAqui123456789",
    "Issuer": "WebPdv",
    "Audience": "WebPdv"
  }
}
```

- **Key**: Chave secreta para assinar os tokens (mínimo 16 caracteres)
- **Issuer**: Emissor do token
- **Audience**: Audiência do token
- **Expiração**: 8 horas por padrão

## Endpoints de Autenticação

### 1. Login

**POST** `/api/v1/auth/login`

Autentica um usuário e retorna um token JWT.

#### Request Body

```json
{
  "email": "admin@empresa.com",
  "senha": "123456"
}
```

#### Response de Sucesso (200)

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

#### Response de Erro (401)

```json
{
  "sucesso": false,
  "mensagem": "Email ou senha inválidos"
}
```

#### Validações

- Email deve ser válido
- Senha deve ter pelo menos 6 caracteres
- Usuário deve estar ativo no sistema

### 2. Logout

**POST** `/api/v1/auth/logout`

Endpoint para logout (implementação básica).

#### Response (200)

```json
{
  "sucesso": true,
  "mensagem": "Logout realizado com sucesso"
}
```

## Como Usar o Token

### Header de Autorização

Para acessar endpoints protegidos, inclua o token no header:

```
Authorization: Bearer {seu_token_jwt}
```

### Exemplo de Uso

```bash
curl -X GET "https://api-pdv.com/api/v1/produtos" \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

## Claims do Token JWT

O token contém as seguintes informações do usuário:

- `nameidentifier`: ID do usuário
- `name`: Nome do usuário
- `email`: Email do usuário
- `perfil`: Perfil/role do usuário
- `empresaId`: ID da empresa associada
- `nomeEmpresa`: Nome da empresa

## Endpoints Protegidos

Todos os seguintes endpoints requerem autenticação:

### Produtos
- `GET /api/v1/produtos` - Listar produtos
- `GET /api/v1/produtos/{id}` - Buscar produto por ID
- `POST /api/v1/produtos` - Criar produto
- `PUT /api/v1/produtos/{id}` - Atualizar produto
- `DELETE /api/v1/produtos/{id}` - Deletar produto

### Pedidos
- `GET /api/v1/pedidos` - Listar pedidos
- `GET /api/v1/pedidos/{id}` - Buscar pedido por ID
- `POST /api/v1/pedidos` - Criar pedido
- `PUT /api/v1/pedidos/{id}` - Atualizar pedido
- `DELETE /api/v1/pedidos/{id}` - Deletar pedido
- `GET /api/v1/pedidos/buscar/{numeroComanda}` - Buscar por número da comanda
- `GET /api/v1/pedidos/abertos` - Listar pedidos abertos

### Categorias
- `GET /api/v1/categorias` - Listar categorias
- `GET /api/v1/categorias/{id}` - Buscar categoria por ID
- `POST /api/v1/categorias` - Criar categoria
- `PUT /api/v1/categorias/{id}` - Atualizar categoria
- `DELETE /api/v1/categorias/{id}` - Deletar categoria

### Usuários
- `GET /api/v1/usuarios` - Listar usuários
- `GET /api/v1/usuarios/{id}` - Buscar usuário por ID
- `POST /api/v1/usuarios` - Criar usuário
- `PUT /api/v1/usuarios/{id}` - Atualizar usuário
- `DELETE /api/v1/usuarios/{id}` - Deletar usuário

### Empresas
- `GET /api/v1/empresas` - Listar empresas
- `GET /api/v1/empresas/{id}` - Buscar empresa por ID
- `POST /api/v1/empresas` - Criar empresa
- `PUT /api/v1/empresas/{id}` - Atualizar empresa
- `DELETE /api/v1/empresas/{id}` - Deletar empresa

## Códigos de Status HTTP

### Autenticação
- `200 OK`: Login/logout bem-sucedido
- `400 Bad Request`: Dados de entrada inválidos
- `401 Unauthorized`: Credenciais inválidas ou token ausente
- `403 Forbidden`: Token válido mas sem permissão
- `500 Internal Server Error`: Erro interno do servidor

### Endpoints Protegidos
- `401 Unauthorized`: Token ausente ou inválido
- `403 Forbidden`: Token expirado ou sem permissão

## Tratamento de Erros

### Token Ausente
```json
{
  "error": "Unauthorized",
  "message": "No authorization header found"
}
```

### Token Inválido
```json
{
  "error": "Unauthorized",
  "message": "Invalid token"
}
```

### Token Expirado
```json
{
  "error": "Unauthorized",
  "message": "Token has expired"
}
```

## Exemplos de Implementação

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
    localStorage.setItem('usuario', JSON.stringify(data.usuario));
  }
  return data;
}

// Requisição autenticada
async function getProdutos() {
  const token = localStorage.getItem('token');
  const response = await fetch('/api/v1/produtos', {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  return await response.json();
}
```

### Python (Requests)

```python
import requests

# Login
def login(email, senha):
    response = requests.post('https://api-pdv.com/api/v1/auth/login', 
                           json={'email': email, 'senha': senha})
    data = response.json()
    if data['sucesso']:
        return data['token']
    return None

# Requisição autenticada
def get_produtos(token):
    headers = {'Authorization': f'Bearer {token}'}
    response = requests.get('https://api-pdv.com/api/v1/produtos', 
                          headers=headers)
    return response.json()
```

### cURL

```bash
# Login
curl -X POST "https://api-pdv.com/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@empresa.com", "senha": "123456"}'

# Usar token em requisições
curl -X GET "https://api-pdv.com/api/v1/produtos" \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"
```

## Segurança

### Boas Práticas

1. **Armazenamento Seguro**: Armazene tokens em localStorage (frontend) ou variáveis de ambiente (backend)
2. **Expiração**: Tokens expiram em 8 horas por padrão
3. **HTTPS**: Sempre use HTTPS em produção
4. **Validação**: Sempre valide tokens no servidor
5. **Logout**: Implemente logout para invalidar tokens quando necessário

### Configuração de Produção

```json
{
  "Jwt": {
    "Key": "ChaveSecretaMuitoLongaEComplexaParaProducao123456789",
    "Issuer": "https://api-pdv.com",
    "Audience": "https://api-pdv.com"
  }
}
```

## Usuários de Teste

### Administrador
- **Email**: admin@empresa.com
- **Senha**: 123456
- **Perfil**: Admin
- **Empresa**: Empresa Teste LTDA

### Usuário Padrão
- **Email**: usuario@empresa.com
- **Senha**: 123456
- **Perfil**: Usuario
- **Empresa**: Empresa Teste LTDA

## Swagger/OpenAPI

A documentação Swagger está configurada com suporte a JWT:

1. Acesse `/swagger` na sua API
2. Clique em "Authorize"
3. Digite: `Bearer {seu_token}`
4. Clique em "Authorize"
5. Agora você pode testar todos os endpoints protegidos

## Troubleshooting

### Problemas Comuns

1. **Token não enviado**: Verifique se o header `Authorization` está presente
2. **Token inválido**: Verifique se o token está correto e não expirou
3. **CORS**: Configure CORS adequadamente para seu domínio
4. **Expiração**: Implemente refresh token para tokens expirados

### Debug

Para debugar problemas de autenticação:

1. Verifique os logs do servidor
2. Use ferramentas como Postman para testar
3. Valide o token em jwt.io
4. Verifique a configuração JWT no appsettings.json 