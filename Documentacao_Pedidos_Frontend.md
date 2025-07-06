# 📋 Documentação dos Endpoints de Pedidos - Frontend

## 🎯 Visão Geral
Esta documentação contém todos os endpoints relacionados a **Pedidos** da API PDV para integração frontend.

---

## 🔗 Base URL
```
https://jonborges.com.br/api_pdv/api/v1/pedido
```

## 🔐 Autenticação

### Endpoint de Login
**POST** `https://jonborges.com.br/api_pdv/api/v1/auth/login`

**Descrição:** Realiza login do usuário e retorna o token JWT necessário para acessar os endpoints.

**Headers:**
```
Content-Type: application/json
```

**Request Body:**
```json
{
  "email": "admin@empresa.com",
  "senha": "123456"
}
```

**Response (200):**
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
    "nomeEmpresa": "Empresa LTDA"
  }
}
```

**Response (401):**
```json
{
  "sucesso": false,
  "mensagem": "Credenciais inválidas"
}
```

### Como Usar o Token
Após fazer login, use o token retornado em todos os endpoints de pedidos:

```
Authorization: Bearer {seu_token_jwt}
```

**Exemplo de Header:**
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
Content-Type: application/json
```

### Informações Importantes sobre o Token

1. **Validade:** O token JWT tem validade limitada (geralmente 24 horas)
2. **Armazenamento:** Guarde o token de forma segura (localStorage, sessionStorage, etc.)
3. **Renovação:** Quando o token expirar, faça login novamente
4. **Segurança:** Não compartilhe o token ou o exponha em logs

### Credenciais de Teste
Para testar a API, use estas credenciais:
- **Email:** admin@empresa.com
- **Senha:** 123456

### Tratamento de Erro 401
Se receber erro 401 (Unauthorized), significa que:
- Token não foi enviado
- Token expirou
- Token é inválido

Neste caso, faça login novamente para obter um novo token.

---

## 📊 Estrutura de Dados do Pedido

### Pedido Completo
```json
{
  "id": 1,
  "numeroPedido": "PED001",
  "numeroComanda": "C001",
  "nomeCliente": "João Silva",
  "telefoneCliente": "11999999999",
  "emailCliente": "joao@email.com",
  "enderecoCliente": "Rua das Flores, 123",
  "quantidadeItens": 2,
  "total": 31.80,
  "status": "Pendente",
  "dataPedido": "2024-01-15T10:30:00",
  "dataConclusao": null,
  "observacoes": "Sem cebola",
  "situacaoId": 1,
  "empresaId": 1,
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-15T10:30:00",
  "itensPedido": [
    {
      "id": 1,
      "pedidoId": 1,
      "produtoId": 1,
      "quantidade": 2,
      "precoUnitario": 15.90,
      "subtotal": 31.80,
      "observacoes": "Sem cebola",
      "createdAt": "2024-01-15T10:30:00",
      "produto": {
        "id": 1,
        "nome": "Hambúrguer",
        "precoVenda": 15.90,
        "codigoBarras": "7891234567890"
      }
    }
  ],
  "situacao": {
    "id": 1,
    "descricao": "Pendente"
  },
  "empresa": {
    "id": 1,
    "razaoSocial": "Empresa LTDA"
  }
}
```

### Status Possíveis
- `"Pendente"` - Pedido criado, aguardando preparo
- `"Em Preparo"` - Pedido sendo preparado
- `"Pronto"` - Pedido pronto para entrega
- `"Em Entrega"` - Pedido sendo entregue
- `"Entregue"` - Pedido finalizado
- `"Cancelado"` - Pedido cancelado

---

## 🚀 Endpoints Disponíveis

### 1. 📋 Listar Todos os Pedidos
**GET** `/api/v1/pedido`

**Descrição:** Retorna todos os pedidos cadastrados no sistema.

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Response (200):**
```json
[
  {
    "id": 1,
    "numeroPedido": "PED001",
    "numeroComanda": "C001",
    "nomeCliente": "João Silva",
    "telefoneCliente": "11999999999",
    "emailCliente": "joao@email.com",
    "enderecoCliente": "Rua das Flores, 123",
    "quantidadeItens": 2,
    "total": 31.80,
    "status": "Pendente",
    "dataPedido": "2024-01-15T10:30:00",
    "dataConclusao": null,
    "observacoes": "Sem cebola",
    "situacaoId": 1,
    "empresaId": 1,
    "createdAt": "2024-01-15T10:30:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

---

### 2. 🔍 Buscar Pedido por ID
**GET** `/api/v1/pedido/{id}`

**Descrição:** Busca um pedido específico pelo seu ID.

**Parâmetros:**
- `id` (number) - ID do pedido

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Response (200):**
```json
{
  "id": 1,
  "numeroPedido": "PED001",
  "numeroComanda": "C001",
  "nomeCliente": "João Silva",
  "telefoneCliente": "11999999999",
  "emailCliente": "joao@email.com",
  "enderecoCliente": "Rua das Flores, 123",
  "quantidadeItens": 2,
  "total": 31.80,
  "status": "Pendente",
  "dataPedido": "2024-01-15T10:30:00",
  "dataConclusao": null,
  "observacoes": "Sem cebola",
  "situacaoId": 1,
  "empresaId": 1,
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-15T10:30:00",
  "itensPedido": [
    {
      "id": 1,
      "pedidoId": 1,
      "produtoId": 1,
      "quantidade": 2,
      "precoUnitario": 15.90,
      "subtotal": 31.80,
      "observacoes": "Sem cebola",
      "createdAt": "2024-01-15T10:30:00",
      "produto": {
        "id": 1,
        "nome": "Hambúrguer",
        "precoVenda": 15.90
      }
    }
  ],
  "situacao": {
    "id": 1,
    "descricao": "Pendente"
  },
  "empresa": {
    "id": 1,
    "razaoSocial": "Empresa LTDA"
  }
}
```

**Response (404):**
```json
{
  "message": "Pedido não encontrado"
}
```

---

### 3. 🏷️ Buscar Pedido por Número da Comanda
**GET** `/api/v1/pedido/comanda/{empresaId}/{numeroComanda}`

**Descrição:** Busca um pedido pelo número da comanda física (útil para restaurantes/bares).

**Parâmetros:**
- `empresaId` (number) - ID da empresa
- `numeroComanda` (string) - Número da comanda (ex: "C001", "MESA5")

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Response (200):**
```json
{
  "id": 1,
  "numeroPedido": "PED001",
  "numeroComanda": "C001",
  "nomeCliente": "João Silva",
  "telefoneCliente": "11999999999",
  "emailCliente": "joao@email.com",
  "enderecoCliente": "Rua das Flores, 123",
  "quantidadeItens": 2,
  "total": 31.80,
  "status": "Pendente",
  "dataPedido": "2024-01-15T10:30:00",
  "dataConclusao": null,
  "observacoes": "Sem cebola",
  "situacaoId": 1,
  "empresaId": 1,
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-15T10:30:00",
  "itensPedido": [
    {
      "id": 1,
      "pedidoId": 1,
      "produtoId": 1,
      "quantidade": 2,
      "precoUnitario": 15.90,
      "subtotal": 31.80,
      "observacoes": "Sem cebola",
      "createdAt": "2024-01-15T10:30:00",
      "produto": {
        "id": 1,
        "nome": "Hambúrguer",
        "precoVenda": 15.90
      }
    }
  ],
  "situacao": {
    "id": 1,
    "descricao": "Pendente"
  },
  "empresa": {
    "id": 1,
    "razaoSocial": "Empresa LTDA"
  }
}
```

**Response (404):**
```json
{
  "message": "Pedido com comanda C001 não encontrado na empresa 1"
}
```

---

### 4. 📋 Listar Pedidos Abertos
**GET** `/api/v1/pedido/abertos/{empresaId}`

**Descrição:** Lista todos os pedidos que ainda estão abertos (Pendente, Em Preparo, Pronto) de uma empresa específica.

**Parâmetros:**
- `empresaId` (number) - ID da empresa

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Response (200):**
```json
[
  {
    "id": 1,
    "numeroPedido": "PED001",
    "numeroComanda": "C001",
    "nomeCliente": "João Silva",
    "telefoneCliente": "11999999999",
    "emailCliente": "joao@email.com",
    "enderecoCliente": "Rua das Flores, 123",
    "quantidadeItens": 2,
    "total": 31.80,
    "status": "Pendente",
    "dataPedido": "2024-01-15T10:30:00",
    "dataConclusao": null,
    "observacoes": "Sem cebola",
    "situacaoId": 1,
    "empresaId": 1,
    "createdAt": "2024-01-15T10:30:00",
    "updatedAt": "2024-01-15T10:30:00",
    "itensPedido": [
      {
        "id": 1,
        "pedidoId": 1,
        "produtoId": 1,
        "quantidade": 2,
        "precoUnitario": 15.90,
        "subtotal": 31.80,
        "observacoes": "Sem cebola",
        "createdAt": "2024-01-15T10:30:00",
        "produto": {
          "id": 1,
          "nome": "Hambúrguer",
          "precoVenda": 15.90
        }
      }
    ],
    "situacao": {
      "id": 1,
      "descricao": "Pendente"
    },
    "empresa": {
      "id": 1,
      "razaoSocial": "Empresa LTDA"
    }
  }
]
```

---

### 5. ➕ Criar Novo Pedido
**POST** `/api/v1/pedido`

**Descrição:** Cria um novo pedido no sistema.

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "numeroPedido": "PED001",
  "numeroComanda": "C001",
  "nomeCliente": "João Silva",
  "telefoneCliente": "11999999999",
  "emailCliente": "joao@email.com",
  "enderecoCliente": "Rua das Flores, 123",
  "quantidadeItens": 2,
  "total": 31.80,
  "status": "Pendente",
  "observacoes": "Sem cebola",
  "situacaoId": 1,
  "empresaId": 1
}
```

**Campos Obrigatórios:**
- `numeroPedido` (string) - Número único do pedido
- `nomeCliente` (string) - Nome do cliente
- `telefoneCliente` (string) - Telefone do cliente
- `status` (string) - Status inicial do pedido
- `empresaId` (number) - ID da empresa

**Campos Opcionais:**
- `numeroComanda` (string) - Número da comanda física
- `emailCliente` (string) - Email do cliente
- `enderecoCliente` (string) - Endereço do cliente
- `quantidadeItens` (number) - Quantidade total de itens
- `total` (number) - Valor total do pedido
- `observacoes` (string) - Observações do pedido
- `situacaoId` (number) - ID do status do pedido

**Response (201):**
```json
{
  "id": 1,
  "numeroPedido": "PED001",
  "numeroComanda": "C001",
  "nomeCliente": "João Silva",
  "telefoneCliente": "11999999999",
  "emailCliente": "joao@email.com",
  "enderecoCliente": "Rua das Flores, 123",
  "quantidadeItens": 2,
  "total": 31.80,
  "status": "Pendente",
  "dataPedido": "2024-01-15T10:30:00",
  "dataConclusao": null,
  "observacoes": "Sem cebola",
  "situacaoId": 1,
  "empresaId": 1,
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-15T10:30:00"
}
```

**Response (400):**
```json
{
  "errors": {
    "nomeCliente": ["Nome do cliente é obrigatório"],
    "telefoneCliente": ["Telefone do cliente é obrigatório"]
  }
}
```

---

### 6. ✏️ Atualizar Pedido
**PUT** `/api/v1/pedido/{id}`

**Descrição:** Atualiza um pedido existente (útil para mudar status, adicionar observações, etc.).

**Parâmetros:**
- `id` (number) - ID do pedido

**Headers:**
```
Authorization: Bearer {token}
Content-Type: application/json
```

**Request Body:**
```json
{
  "id": 1,
  "numeroPedido": "PED001",
  "numeroComanda": "C001",
  "nomeCliente": "João Silva",
  "telefoneCliente": "11999999999",
  "emailCliente": "joao@email.com",
  "enderecoCliente": "Rua das Flores, 123",
  "quantidadeItens": 2,
  "total": 31.80,
  "status": "Em Preparo",
  "observacoes": "Sem cebola, urgente!",
  "situacaoId": 1,
  "empresaId": 1
}
```

**Response (200):**
```json
{
  "id": 1,
  "numeroPedido": "PED001",
  "numeroComanda": "C001",
  "nomeCliente": "João Silva",
  "telefoneCliente": "11999999999",
  "emailCliente": "joao@email.com",
  "enderecoCliente": "Rua das Flores, 123",
  "quantidadeItens": 2,
  "total": 31.80,
  "status": "Em Preparo",
  "dataPedido": "2024-01-15T10:30:00",
  "dataConclusao": null,
  "observacoes": "Sem cebola, urgente!",
  "situacaoId": 1,
  "empresaId": 1,
  "createdAt": "2024-01-15T10:30:00",
  "updatedAt": "2024-01-15T10:35:00"
}
```

**Response (404):**
```json
{
  "message": "Pedido não encontrado"
}
```

---

### 7. 🗑️ Deletar Pedido
**DELETE** `/api/v1/pedido/{id}`

**Descrição:** Remove um pedido do sistema.

**Parâmetros:**
- `id` (number) - ID do pedido

**Headers:**
```
Authorization: Bearer {token}
```

**Response (204):**
```
No Content
```

**Response (404):**
```json
{
  "message": "Pedido não encontrado"
}
```

---

## 🚨 Códigos de Status HTTP

- **200** - Sucesso
- **201** - Criado com sucesso
- **204** - Deletado com sucesso
- **400** - Dados inválidos
- **401** - Não autorizado (token inválido ou expirado)
- **404** - Pedido não encontrado
- **500** - Erro interno do servidor

---

## 📝 Notas Importantes

1. **Token JWT**: Sempre inclua o token de autenticação
2. **Empresa ID**: Muitos endpoints precisam do ID da empresa
3. **Status**: Use os status corretos para controle de fluxo
4. **Comanda**: Campo opcional, útil para restaurantes/bares
5. **Relacionamentos**: Os endpoints retornam dados completos com itens, produtos, etc.
6. **Ordenação**: Pedidos abertos vêm ordenados por data (mais recentes primeiro)

---

**📞 Suporte:** Em caso de dúvidas, consulte a documentação completa ou entre em contato com o desenvolvedor backend. 