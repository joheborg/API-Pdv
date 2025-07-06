# Documentação dos Endpoints - API PDV

## Base URL
```
https://jonborges.com.br/api_pdv/api/v1
```

## Autenticação
A API utiliza autenticação JWT. Para endpoints protegidos, inclua o token no header:
```
Authorization: Bearer {token}
```

---

## 1. Autenticação (Auth)

### POST /auth/login
**Descrição:** Realiza login do usuário

**Request Body:**
```json
{
  "email": "string",
  "senha": "string"
}
```

**Response (200):**
```json
{
  "sucesso": true,
  "token": "jwt_token_here",
  "mensagem": "Login realizado com sucesso",
  "usuario": {
    "id": 1,
    "nome": "João Silva",
    "email": "joao@empresa.com",
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

### POST /auth/logout
**Descrição:** Realiza logout do usuário

**Response (200):**
```json
{
  "sucesso": true,
  "mensagem": "Logout realizado com sucesso"
}
```

---

## 2. Usuários (Usuario)

### GET /usuario
**Descrição:** Lista todos os usuários

**Response (200):**
```json
[
  {
    "id": 1,
    "nome": "João Silva",
    "email": "joao@empresa.com",
    "perfil": "Admin",
    "ativo": true,
    "ultimoAcesso": "2024-01-15T10:30:00",
    "funcionarioId": 1,
    "empresaId": 1,
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

### GET /usuario/{id}
**Descrição:** Busca usuário por ID

**Response (200):**
```json
{
  "id": 1,
  "nome": "João Silva",
  "email": "joao@empresa.com",
  "perfil": "Admin",
  "ativo": true,
  "ultimoAcesso": "2024-01-15T10:30:00",
  "funcionarioId": 1,
  "empresaId": 1,
  "createdAt": "2024-01-01T00:00:00",
  "updatedAt": "2024-01-15T10:30:00"
}
```

### GET /usuario/empresa/{empresaId}
**Descrição:** Lista usuários por empresa

### GET /usuario/ativos
**Descrição:** Lista apenas usuários ativos

### GET /usuario/email/{email}
**Descrição:** Busca usuário por email

### POST /usuario
**Descrição:** Cria novo usuário

**Request Body:**
```json
{
  "nome": "João Silva",
  "email": "joao@empresa.com",
  "senha": "123456",
  "perfil": "Admin",
  "ativo": true,
  "funcionarioId": 1,
  "empresaId": 1
}
```

### PUT /usuario/{id}
**Descrição:** Atualiza usuário

### DELETE /usuario/{id}
**Descrição:** Remove usuário

### PUT /usuario/{id}/ativar
**Descrição:** Ativa usuário

### PUT /usuario/{id}/desativar
**Descrição:** Desativa usuário

### PUT /usuario/{id}/alterar-senha
**Descrição:** Altera senha do usuário

**Request Body:**
```json
{
  "novaSenha": "nova123456"
}
```

---

## 3. Produtos (Produto)

### GET /produto
**Descrição:** Lista todos os produtos

**Response (200):**
```json
[
  {
    "id": 1,
    "empresaId": 1,
    "categoriaId": 1,
    "codigoProduto": "PROD001",
    "imagemUrl": "https://exemplo.com/imagem.jpg",
    "imagemBase64": "data:image/jpeg;base64,...",
    "imagemNome": "produto.jpg",
    "imagemMimeType": "image/jpeg",
    "nome": "Hambúrguer",
    "nomeAlternativo": "X-Burger",
    "descricao": "Hambúrguer artesanal",
    "ingredientes": "Pão, carne, alface, tomate",
    "precoVenda": 15.90,
    "precoCusto": 8.50,
    "quantidadePadrao": 1,
    "peso": "200g",
    "servePessoas": "1",
    "codigoBarras": "7891234567890",
    "situacao": true,
    "unidadeVenda": "UN",
    "ncm": "21069090",
    "cest": "28.038.00",
    "cfop": "5102",
    "csosnCst": "102",
    "origemProduto": 0,
    "cstIcms": "00",
    "baseCalculoIcms": 15.90,
    "aliquotaIcms": 18.00,
    "valorIcms": 2.86,
    "cstIpi": "50",
    "baseCalculoIpi": 0.00,
    "aliquotaIpi": 0.00,
    "valorIpi": 0.00,
    "cstPis": "01",
    "baseCalculoPis": 15.90,
    "aliquotaPis": 1.65,
    "valorPis": 0.26,
    "cstCofins": "01",
    "baseCalculoCofins": 15.90,
    "aliquotaCofins": 7.60,
    "valorCofins": 1.21,
    "codigoEan": "7891234567890",
    "informacoesAdicionais": "Produto artesanal",
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-15T10:30:00",
    "estoqueAtual": 50,
    "estoqueMinimo": 10,
    "estoqueMaximo": 100,
    "ultimaMovimentacao": "2024-01-15T10:30:00",
    "localizacaoEstoque": "Prateleira A1",
    "controlaEstoque": true
  }
]
```

### GET /produto/empresa/{empresaId}
**Descrição:** Lista produtos por empresa

### GET /produto/{id}
**Descrição:** Busca produto por ID

### GET /produto/codigo/{empresaId}/{codigoProduto}
**Descrição:** Busca produto por código na empresa

### GET /produto/barras/{codigoBarras}
**Descrição:** Busca produto por código de barras

### GET /produto/ean/{codigoEan}
**Descrição:** Busca produto por código EAN

### GET /produto/categoria/{categoriaId}
**Descrição:** Lista produtos por categoria

### POST /produto
**Descrição:** Cria novo produto

**Request Body:**
```json
{
  "empresaId": 1,
  "categoriaId": 1,
  "codigoProduto": "PROD001",
  "nome": "Hambúrguer",
  "descricao": "Hambúrguer artesanal",
  "precoVenda": 15.90,
  "precoCusto": 8.50,
  "codigoBarras": "7891234567890",
  "situacao": true,
  "estoqueAtual": 50,
  "estoqueMinimo": 10,
  "estoqueMaximo": 100
}
```

### PUT /produto/{id}
**Descrição:** Atualiza produto

### DELETE /produto/{id}
**Descrição:** Remove produto

---

## 4. Categorias (Categoria)

### GET /categoria
**Descrição:** Lista todas as categorias

**Response (200):**
```json
[
  {
    "id": 1,
    "descricao": "Lanches",
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

### GET /categoria/{id}
**Descrição:** Busca categoria por ID

### POST /categoria
**Descrição:** Cria nova categoria

**Request Body:**
```json
{
  "descricao": "Lanches"
}
```

### PUT /categoria/{id}
**Descrição:** Atualiza categoria

### DELETE /categoria/{id}
**Descrição:** Remove categoria

---

## 5. Empresas (Empresa)

### GET /empresa
**Descrição:** Lista todas as empresas

**Response (200):**
```json
[
  {
    "id": 1,
    "cnpj": "12345678000199",
    "razaoSocial": "Empresa LTDA",
    "nomeFantasia": "Empresa",
    "inscricaoEstadual": "123456789",
    "crt": "1",
    "logoBase64": "data:image/jpeg;base64,...",
    "logoNome": "logo.jpg",
    "logoMimeType": "image/jpeg",
    "endereco": {
      "logradouro": "Rua das Flores",
      "numero": "123",
      "complemento": "Sala 1",
      "bairro": "Centro",
      "codigoMunicipio": "3550308",
      "nomeMunicipio": "São Paulo",
      "uf": "SP",
      "cep": "01234-567",
      "codigoPais": "1058",
      "nomePais": "Brasil"
    },
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

### GET /empresa/{id}
**Descrição:** Busca empresa por ID

### GET /empresa/cnpj/{cnpj}
**Descrição:** Busca empresa por CNPJ

### POST /empresa
**Descrição:** Cria nova empresa

**Request Body:**
```json
{
  "cnpj": "12345678000199",
  "razaoSocial": "Empresa LTDA",
  "nomeFantasia": "Empresa",
  "inscricaoEstadual": "123456789",
  "crt": "1",
  "endereco": {
    "logradouro": "Rua das Flores",
    "numero": "123",
    "complemento": "Sala 1",
    "bairro": "Centro",
    "codigoMunicipio": "3550308",
    "nomeMunicipio": "São Paulo",
    "uf": "SP",
    "cep": "01234-567"
  }
}
```

### PUT /empresa/{id}
**Descrição:** Atualiza empresa

### DELETE /empresa/{id}
**Descrição:** Remove empresa

---

## 6. Pedidos (Pedido)

### GET /pedido
**Descrição:** Lista todos os pedidos

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
        "createdAt": "2024-01-15T10:30:00"
      }
    ]
  }
]
```

### GET /pedido/{id}
**Descrição:** Busca pedido por ID

### GET /pedido/comanda/{empresaId}/{numeroComanda}
**Descrição:** Busca pedido por número da comanda na empresa

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

### GET /pedido/abertos/{empresaId}
**Descrição:** Lista pedidos abertos (Pendente, Em Preparo, Pronto) da empresa

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

### POST /pedido
**Descrição:** Cria novo pedido

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

### PUT /pedido/{id}
**Descrição:** Atualiza pedido

### DELETE /pedido/{id}
**Descrição:** Remove pedido

---

## 7. Status de Pedidos (StatusPedido)

### GET /statuspedido
**Descrição:** Lista todos os status

**Response (200):**
```json
[
  {
    "id": 1,
    "descricao": "Pendente",
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

### GET /statuspedido/{id}
**Descrição:** Busca status por ID

### POST /statuspedido
**Descrição:** Cria novo status

**Request Body:**
```json
{
  "descricao": "Pendente"
}
```

### PUT /statuspedido/{id}
**Descrição:** Atualiza status

### DELETE /statuspedido/{id}
**Descrição:** Remove status

---

## 8. Itens de Pedido (ItemPedido)

### GET /itempedido
**Descrição:** Lista todos os itens de pedido

**Response (200):**
```json
[
  {
    "id": 1,
    "pedidoId": 1,
    "produtoId": 1,
    "quantidade": 2,
    "precoUnitario": 15.90,
    "subtotal": 31.80,
    "observacoes": "Sem cebola",
    "createdAt": "2024-01-15T10:30:00"
  }
]
```

### GET /itempedido/{id}
**Descrição:** Busca item de pedido por ID

### POST /itempedido
**Descrição:** Cria novo item de pedido

**Request Body:**
```json
{
  "pedidoId": 1,
  "produtoId": 1,
  "quantidade": 2,
  "precoUnitario": 15.90,
  "subtotal": 31.80,
  "observacoes": "Sem cebola"
}
```

### PUT /itempedido/{id}
**Descrição:** Atualiza item de pedido

### DELETE /itempedido/{id}
**Descrição:** Remove item de pedido

---

## 9. Caixas (Caixa)

### GET /caixa
**Descrição:** Lista todos os caixas

**Response (200):**
```json
[
  {
    "id": 1,
    "empresaId": 1,
    "dataAbertura": "2024-01-15T08:00:00",
    "dataFechamento": null,
    "valorAbertura": 100.00,
    "valorFechamento": 0.00,
    "trocoFinal": 0.00,
    "status": "aberto",
    "observacao": "Caixa principal",
    "totalDinheiro": 500.00,
    "totalCartaoCredito": 300.00,
    "totalCartaoDebito": 200.00,
    "totalPix": 150.00,
    "totalOutros": 50.00,
    "totalVendas": 1200.00
  }
]
```

### GET /caixa/{id}
**Descrição:** Busca caixa por ID

### POST /caixa
**Descrição:** Cria novo caixa

**Request Body:**
```json
{
  "empresaId": 1,
  "valorAbertura": 100.00,
  "observacao": "Caixa principal"
}
```

### PUT /caixa/{id}
**Descrição:** Atualiza caixa

### DELETE /caixa/{id}
**Descrição:** Remove caixa

---

## 10. Pagamentos de Caixa (PagamentoCaixa)

### GET /pagamentocaixa
**Descrição:** Lista todos os pagamentos

**Response (200):**
```json
[
  {
    "id": 1,
    "caixaId": 1,
    "formaPagamento": "Dinheiro",
    "valor": 50.00,
    "dataHora": "2024-01-15T10:30:00",
    "observacao": "Pagamento cliente"
  }
]
```

### GET /pagamentocaixa/{id}
**Descrição:** Busca pagamento por ID

### POST /pagamentocaixa
**Descrição:** Cria novo pagamento

**Request Body:**
```json
{
  "caixaId": 1,
  "formaPagamento": "Dinheiro",
  "valor": 50.00,
  "observacao": "Pagamento cliente"
}
```

### PUT /pagamentocaixa/{id}
**Descrição:** Atualiza pagamento

### DELETE /pagamentocaixa/{id}
**Descrição:** Remove pagamento

---

## 11. Motoboys (Motoboy)

### GET /motoboy
**Descrição:** Lista todos os motoboys

**Response (200):**
```json
[
  {
    "id": 1,
    "empresaId": 1,
    "nome": "João Motoboy",
    "documento": "12345678901",
    "telefone": "11999999999",
    "veiculo": "Moto",
    "placa": "ABC1234",
    "status": "ativo",
    "observacao": "Disponível",
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

### GET /motoboy/{id}
**Descrição:** Busca motoboy por ID

### POST /motoboy
**Descrição:** Cria novo motoboy

**Request Body:**
```json
{
  "empresaId": 1,
  "nome": "João Motoboy",
  "documento": "12345678901",
  "telefone": "11999999999",
  "veiculo": "Moto",
  "placa": "ABC1234",
  "status": "ativo",
  "observacao": "Disponível"
}
```

### PUT /motoboy/{id}
**Descrição:** Atualiza motoboy

### DELETE /motoboy/{id}
**Descrição:** Remove motoboy

---

## 12. Funcionários (Funcionario)

### GET /funcionario
**Descrição:** Lista todos os funcionários

**Response (200):**
```json
[
  {
    "id": 1,
    "nome": "João Silva",
    "cpf": "12345678901",
    "rg": "123456789",
    "telefone": "11999999999",
    "email": "joao@empresa.com",
    "cargo": "Atendente",
    "salario": 1500.00,
    "dataAdmissao": "2024-01-01T00:00:00",
    "dataDemissao": null,
    "ativo": true,
    "endereco": "Rua das Flores, 123",
    "cidade": "São Paulo",
    "uf": "SP",
    "cep": "01234-567",
    "empresaId": 1,
    "createdAt": "2024-01-01T00:00:00",
    "updatedAt": "2024-01-15T10:30:00"
  }
]
```

### GET /funcionario/{id}
**Descrição:** Busca funcionário por ID

### POST /funcionario
**Descrição:** Cria novo funcionário

**Request Body:**
```json
{
  "nome": "João Silva",
  "cpf": "12345678901",
  "rg": "123456789",
  "telefone": "11999999999",
  "email": "joao@empresa.com",
  "cargo": "Atendente",
  "salario": 1500.00,
  "dataAdmissao": "2024-01-01T00:00:00",
  "ativo": true,
  "endereco": "Rua das Flores, 123",
  "cidade": "São Paulo",
  "uf": "SP",
  "cep": "01234-567",
  "empresaId": 1
}
```

### PUT /funcionario/{id}
**Descrição:** Atualiza funcionário

### DELETE /funcionario/{id}
**Descrição:** Remove funcionário

---

## Códigos de Status HTTP

- **200 OK:** Requisição bem-sucedida
- **201 Created:** Recurso criado com sucesso
- **204 No Content:** Requisição bem-sucedida sem conteúdo
- **400 Bad Request:** Dados inválidos na requisição
- **401 Unauthorized:** Não autorizado (credenciais inválidas)
- **404 Not Found:** Recurso não encontrado
- **500 Internal Server Error:** Erro interno do servidor

---

## Observações

1. **Autenticação:** A maioria dos endpoints requer autenticação JWT
2. **Validação:** Todos os campos obrigatórios são validados
3. **Relacionamentos:** As entidades possuem relacionamentos entre si
4. **Auditoria:** Todas as entidades possuem campos de auditoria (createdAt, updatedAt)
5. **Soft Delete:** Algumas entidades podem implementar soft delete
6. **Paginação:** Para listas grandes, considere implementar paginação
7. **Filtros:** Considere adicionar filtros por empresa, data, status, etc. 