# Exemplos de Uso dos Endpoints - API PDV

## 1. Fluxo de Autenticação

### 1.1 Login
```bash
curl -X POST http://localhost:5000/api/v1/auth/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "admin@empresa.com",
    "senha": "123456"
  }'
```

**Resposta:**
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

### 1.2 Usar Token em Requisições
```bash
curl -X GET http://localhost:5000/api/v1/produto \
  -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
```

## 2. Gestão de Empresas

### 2.1 Criar Empresa
```bash
curl -X POST http://localhost:5000/api/v1/empresa \
  -H "Content-Type: application/json" \
  -d '{
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
  }'
```

### 2.2 Buscar Empresa por CNPJ
```bash
curl -X GET http://localhost:5000/api/v1/empresa/cnpj/12345678000199
```

## 3. Gestão de Categorias

### 3.1 Criar Categoria
```bash
curl -X POST http://localhost:5000/api/v1/categoria \
  -H "Content-Type: application/json" \
  -d '{
    "descricao": "Lanches"
  }'
```

### 3.2 Listar Categorias
```bash
curl -X GET http://localhost:5000/api/v1/categoria
```

## 4. Gestão de Produtos

### 4.1 Criar Produto
```bash
curl -X POST http://localhost:5000/api/v1/produto \
  -H "Content-Type: application/json" \
  -d '{
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
    "estoqueMaximo": 100,
    "ncm": "21069090",
    "cstIcms": "00",
    "aliquotaIcms": 18.00
  }'
```

### 4.2 Buscar Produto por Código de Barras
```bash
curl -X GET http://localhost:5000/api/v1/produto/barras/7891234567890
```

### 4.3 Listar Produtos por Empresa
```bash
curl -X GET http://localhost:5000/api/v1/produto/empresa/1
```

### 4.4 Listar Produtos por Categoria
```bash
curl -X GET http://localhost:5000/api/v1/produto/categoria/1
```

## 5. Gestão de Usuários

### 5.1 Criar Usuário
```bash
curl -X POST http://localhost:5000/api/v1/usuario \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@empresa.com",
    "senha": "123456",
    "perfil": "Operador",
    "ativo": true,
    "empresaId": 1
  }'
```

### 5.2 Listar Usuários Ativos
```bash
curl -X GET http://localhost:5000/api/v1/usuario/ativos
```

### 5.3 Ativar/Desativar Usuário
```bash
# Ativar
curl -X PUT http://localhost:5000/api/v1/usuario/1/ativar

# Desativar
curl -X PUT http://localhost:5000/api/v1/usuario/1/desativar
```

### 5.4 Alterar Senha
```bash
curl -X PUT http://localhost:5000/api/v1/usuario/1/alterar-senha \
  -H "Content-Type: application/json" \
  -d '{
    "novaSenha": "nova123456"
  }'
```

## 6. Gestão de Pedidos

### 6.1 Criar Pedido
```bash
curl -X POST http://localhost:5000/api/v1/pedido \
  -H "Content-Type: application/json" \
  -d '{
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
  }'
```

### 6.2 Buscar Pedido por ID
```bash
curl -X GET http://localhost:5000/api/v1/pedido/1
```

### 6.3 Buscar Pedido por Número da Comanda
```bash
curl -X GET http://localhost:5000/api/v1/pedido/comanda/1/C001
```

### 6.4 Listar Pedidos Abertos
```bash
curl -X GET http://localhost:5000/api/v1/pedido/abertos/1
```

## 7. Gestão de Itens de Pedido

### 7.1 Adicionar Item ao Pedido
```bash
curl -X POST http://localhost:5000/api/v1/itempedido \
  -H "Content-Type: application/json" \
  -d '{
    "pedidoId": 1,
    "produtoId": 1,
    "quantidade": 2,
    "precoUnitario": 15.90,
    "subtotal": 31.80,
    "observacoes": "Sem cebola"
  }'
```

## 8. Gestão de Status de Pedidos

### 8.1 Criar Status
```bash
curl -X POST http://localhost:5000/api/v1/statuspedido \
  -H "Content-Type: application/json" \
  -d '{
    "descricao": "Em Preparo"
  }'
```

## 9. Gestão de Caixas

### 9.1 Abrir Caixa
```bash
curl -X POST http://localhost:5000/api/v1/caixa \
  -H "Content-Type: application/json" \
  -d '{
    "empresaId": 1,
    "valorAbertura": 100.00,
    "observacao": "Caixa principal"
  }'
```

### 9.2 Fechar Caixa
```bash
curl -X PUT http://localhost:5000/api/v1/caixa/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "empresaId": 1,
    "dataAbertura": "2024-01-15T08:00:00",
    "dataFechamento": "2024-01-15T18:00:00",
    "valorAbertura": 100.00,
    "valorFechamento": 1200.00,
    "trocoFinal": 50.00,
    "status": "fechado",
    "observacao": "Caixa principal",
    "totalDinheiro": 500.00,
    "totalCartaoCredito": 300.00,
    "totalCartaoDebito": 200.00,
    "totalPix": 150.00,
    "totalOutros": 50.00,
    "totalVendas": 1200.00
  }'
```

## 10. Gestão de Pagamentos

### 10.1 Registrar Pagamento
```bash
curl -X POST http://localhost:5000/api/v1/pagamentocaixa \
  -H "Content-Type: application/json" \
  -d '{
    "caixaId": 1,
    "formaPagamento": "Dinheiro",
    "valor": 50.00,
    "observacao": "Pagamento cliente"
  }'
```

## 11. Gestão de Motoboys

### 11.1 Cadastrar Motoboy
```bash
curl -X POST http://localhost:5000/api/v1/motoboy \
  -H "Content-Type: application/json" \
  -d '{
    "empresaId": 1,
    "nome": "João Motoboy",
    "documento": "12345678901",
    "telefone": "11999999999",
    "veiculo": "Moto",
    "placa": "ABC1234",
    "status": "ativo",
    "observacao": "Disponível"
  }'
```

## 12. Gestão de Funcionários

### 12.1 Cadastrar Funcionário
```bash
curl -X POST http://localhost:5000/api/v1/funcionario \
  -H "Content-Type: application/json" \
  -d '{
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
  }'
```

## 13. Exemplos com JavaScript/Fetch

### 13.1 Login
```javascript
const login = async (email, senha) => {
  const response = await fetch('http://localhost:5000/api/v1/auth/login', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify({ email, senha })
  });
  
  const data = await response.json();
  localStorage.setItem('token', data.token);
  return data;
};
```

### 13.2 Listar Produtos
```javascript
const listarProdutos = async () => {
  const token = localStorage.getItem('token');
  const response = await fetch('http://localhost:5000/api/v1/produto', {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  
  return await response.json();
};
```

### 13.3 Criar Produto
```javascript
const criarProduto = async (produto) => {
  const token = localStorage.getItem('token');
  const response = await fetch('http://localhost:5000/api/v1/produto', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify(produto)
  });
  
  return await response.json();
};
```

### 13.4 Buscar Pedido por Comanda
```javascript
const buscarPedidoPorComanda = async (empresaId, numeroComanda) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`http://localhost:5000/api/v1/pedido/comanda/${empresaId}/${numeroComanda}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  
  return await response.json();
};
```

### 13.5 Listar Pedidos Abertos
```javascript
const listarPedidosAbertos = async (empresaId) => {
  const token = localStorage.getItem('token');
  const response = await fetch(`http://localhost:5000/api/v1/pedido/abertos/${empresaId}`, {
    headers: {
      'Authorization': `Bearer ${token}`
    }
  });
  
  return await response.json();
};
```

## 14. Exemplos com Python/Requests

### 14.1 Login
```python
import requests
import json

def login(email, senha):
    url = "http://localhost:5000/api/v1/auth/login"
    data = {
        "email": email,
        "senha": senha
    }
    
    response = requests.post(url, json=data)
    return response.json()

# Uso
resultado = login("admin@empresa.com", "123456")
token = resultado["token"]
```

### 14.2 Listar Produtos
```python
def listar_produtos(token):
    url = "http://localhost:5000/api/v1/produto"
    headers = {
        "Authorization": f"Bearer {token}"
    }
    
    response = requests.get(url, headers=headers)
    return response.json()
```

### 14.3 Buscar Pedido por Comanda
```python
def buscar_pedido_por_comanda(token, empresa_id, numero_comanda):
    url = f"http://localhost:5000/api/v1/pedido/comanda/{empresa_id}/{numero_comanda}"
    headers = {
        "Authorization": f"Bearer {token}"
    }
    
    response = requests.get(url, headers=headers)
    return response.json()
```

### 14.4 Listar Pedidos Abertos
```python
def listar_pedidos_abertos(token, empresa_id):
    url = f"http://localhost:5000/api/v1/pedido/abertos/{empresa_id}"
    headers = {
        "Authorization": f"Bearer {token}"
    }
    
    response = requests.get(url, headers=headers)
    return response.json()
```

## 15. Fluxo Completo de Venda

### 15.1 Abrir Caixa
```bash
curl -X POST http://localhost:5000/api/v1/caixa \
  -H "Content-Type: application/json" \
  -d '{
    "empresaId": 1,
    "valorAbertura": 100.00,
    "observacao": "Caixa principal"
  }'
```

### 15.2 Criar Pedido
```bash
curl -X POST http://localhost:5000/api/v1/pedido \
  -H "Content-Type: application/json" \
  -d '{
    "numeroPedido": "PED001",
    "numeroComanda": "C001",
    "nomeCliente": "João Silva",
    "telefoneCliente": "11999999999",
    "quantidadeItens": 2,
    "total": 31.80,
    "status": "Pendente",
    "situacaoId": 1,
    "empresaId": 1
  }'
```

### 15.3 Adicionar Itens
```bash
curl -X POST http://localhost:5000/api/v1/itempedido \
  -H "Content-Type: application/json" \
  -d '{
    "pedidoId": 1,
    "produtoId": 1,
    "quantidade": 2,
    "precoUnitario": 15.90,
    "subtotal": 31.80
  }'
```

### 15.4 Registrar Pagamento
```bash
curl -X POST http://localhost:5000/api/v1/pagamentocaixa \
  -H "Content-Type: application/json" \
  -d '{
    "caixaId": 1,
    "formaPagamento": "Dinheiro",
    "valor": 31.80
  }'
```

### 15.5 Atualizar Status do Pedido
```bash
curl -X PUT http://localhost:5000/api/v1/pedido/1 \
  -H "Content-Type: application/json" \
  -d '{
    "id": 1,
    "numeroPedido": "PED001",
    "numeroComanda": "C001",
    "nomeCliente": "João Silva",
    "telefoneCliente": "11999999999",
    "quantidadeItens": 2,
    "total": 31.80,
    "status": "Concluído",
    "situacaoId": 1,
    "empresaId": 1
  }'
```

## 16. Tratamento de Erros

### 16.1 Erro de Validação
```json
{
  "errors": {
    "email": ["Email é obrigatório"],
    "senha": ["Senha deve ter pelo menos 6 caracteres"]
  }
}
```

### 16.2 Erro de Recurso Não Encontrado
```json
{
  "message": "Produto com ID 999 não encontrado"
}
```

### 16.3 Erro de Autenticação
```json
{
  "sucesso": false,
  "mensagem": "Credenciais inválidas"
}
```

### 16.4 Erro Interno do Servidor
```json
{
  "message": "Erro interno do servidor: Connection timeout"
}
```

## 17. Boas Práticas

1. **Sempre inclua o token de autenticação** em requisições protegidas
2. **Valide os dados** antes de enviar para a API
3. **Trate os erros** adequadamente no frontend
4. **Use HTTPS** em produção
5. **Implemente rate limiting** para evitar abuso
6. **Faça backup** dos dados regularmente
7. **Monitore** o uso da API
8. **Documente** mudanças na API
9. **Teste** todos os endpoints antes de usar em produção
10. **Mantenha** o token seguro e não o exponha 