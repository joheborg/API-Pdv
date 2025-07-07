# 📋 **Endpoints da API PDV**

## 🔐 **Autenticação**
- `POST /api/v1/auth/login` - Login do usuário
- `POST /api/v1/auth/logout` - Logout do usuário

## 🏢 **Empresas**
- `GET /api/v1/empresa` - Listar todas as empresas
- `GET /api/v1/empresa/{id}` - Buscar empresa por ID
- `GET /api/v1/empresa/cnpj/{cnpj}` - Buscar empresa por CNPJ
- `POST /api/v1/empresa` - Criar empresa
- `PUT /api/v1/empresa/{id}` - Atualizar empresa
- `DELETE /api/v1/empresa/{id}` - Deletar empresa

## 📦 **Produtos**
- `GET /api/v1/produto` - Listar produtos da empresa do usuário (requer autenticação)
- `GET /api/v1/produto/dev/all` - Listar todos os produtos (sem autenticação)
- `GET /api/v1/produto/{id}` - Buscar produto por ID
- `GET /api/v1/produto/codigo/{codigoProduto}` - Buscar produto por código
- `GET /api/v1/produto/barras/{codigoBarras}` - Buscar produto por código de barras
- `GET /api/v1/produto/categoria/{categoriaId}` - Buscar produtos por categoria
- `POST /api/v1/produto` - Criar produto
- `PUT /api/v1/produto/{id}` - Atualizar produto
- `DELETE /api/v1/produto/{id}` - Deletar produto

## 🏷️ **Categorias**
- `GET /api/v1/categoria` - Listar todas as categorias (requer autenticação)
- `GET /api/v1/categoria/dev/all` - Listar todas as categorias (sem autenticação)
- `GET /api/v1/categoria/{id}` - Buscar categoria por ID
- `POST /api/v1/categoria` - Criar categoria
- `PUT /api/v1/categoria/{id}` - Atualizar categoria
- `DELETE /api/v1/categoria/{id}` - Deletar categoria

## 🍽️ **Pedidos**
- `GET /api/v1/pedido` - Listar pedidos da empresa do usuário (requer autenticação)
- `GET /api/v1/pedido/dev/all` - Listar todos os pedidos (sem autenticação)
- `GET /api/v1/pedido/{id}` - Buscar pedido por ID
- `GET /api/v1/pedido/comanda/{numeroComanda}` - Buscar pedido por número da comanda
- `GET /api/v1/pedido/abertos` - Listar pedidos abertos da empresa
- `POST /api/v1/pedido` - Criar pedido
- `PUT /api/v1/pedido/{id}` - Atualizar pedido
- `DELETE /api/v1/pedido/{id}` - Deletar pedido

## 📝 **Itens de Pedido**
- `GET /api/v1/itempedido` - Listar todos os itens de pedido
- `GET /api/v1/itempedido/{id}` - Buscar item de pedido por ID
- `POST /api/v1/itempedido` - Criar item de pedido
- `PUT /api/v1/itempedido/{id}` - Atualizar item de pedido
- `DELETE /api/v1/itempedido/{id}` - Deletar item de pedido

## 👥 **Usuários**
- `GET /api/v1/usuario` - Listar todos os usuários
- `GET /api/v1/usuario/{id}` - Buscar usuário por ID
- `GET /api/v1/usuario/email/{email}` - Buscar usuário por email
- `GET /api/v1/usuario/empresa/{empresaId}` - Buscar usuários por empresa
- `GET /api/v1/usuario/ativos` - Listar usuários ativos
- `POST /api/v1/usuario` - Criar usuário
- `PUT /api/v1/usuario/{id}` - Atualizar usuário
- `PUT /api/v1/usuario/{id}/alterar-senha` - Alterar senha do usuário
- `PUT /api/v1/usuario/{id}/ativar` - Ativar usuário
- `PUT /api/v1/usuario/{id}/desativar` - Desativar usuário
- `DELETE /api/v1/usuario/{id}` - Deletar usuário

## 👨‍💼 **Funcionários**
- `GET /api/v1/funcionario` - Listar todos os funcionários
- `GET /api/v1/funcionario/{id}` - Buscar funcionário por ID
- `POST /api/v1/funcionario` - Criar funcionário
- `PUT /api/v1/funcionario/{id}` - Atualizar funcionário
- `DELETE /api/v1/funcionario/{id}` - Deletar funcionário

## 🚚 **Motoboys**
- `GET /api/v1/motoboy` - Listar todos os motoboys
- `GET /api/v1/motoboy/{id}` - Buscar motoboy por ID
- `POST /api/v1/motoboy` - Criar motoboy
- `PUT /api/v1/motoboy/{id}` - Atualizar motoboy
- `DELETE /api/v1/motoboy/{id}` - Deletar motoboy

## 💰 **Caixa**
- `GET /api/v1/caixa` - Listar todos os caixas
- `GET /api/v1/caixa/{id}` - Buscar caixa por ID
- `POST /api/v1/caixa` - Criar caixa
- `PUT /api/v1/caixa/{id}` - Atualizar caixa
- `DELETE /api/v1/caixa/{id}` - Deletar caixa

## 💳 **Pagamentos de Caixa**
- `GET /api/v1/pagamentocaixa` - Listar todos os pagamentos
- `GET /api/v1/pagamentocaixa/{id}` - Buscar pagamento por ID
- `POST /api/v1/pagamentocaixa` - Criar pagamento
- `PUT /api/v1/pagamentocaixa/{id}` - Atualizar pagamento
- `DELETE /api/v1/pagamentocaixa/{id}` - Deletar pagamento

## 📊 **Status de Pedidos**
- `GET /api/v1/statuspedido` - Listar todos os status
- `GET /api/v1/statuspedido/{id}` - Buscar status por ID
- `POST /api/v1/statuspedido` - Criar status
- `PUT /api/v1/statuspedido/{id}` - Atualizar status
- `DELETE /api/v1/statuspedido/{id}` - Deletar status

---

## 🔧 **Endpoints de Desenvolvimento (Sem Autenticação)**
- `GET /api/v1/produto/dev/all` - Listar todos os produtos
- `GET /api/v1/pedido/dev/all` - Listar todos os pedidos
- `GET /api/v1/categoria/dev/all` - Listar todas as categorias

---

## 📍 **Configurações**

### **URL Base**
- **Desenvolvimento**: `http://localhost:5193`
- **Produção**: `https://seu-dominio.com`

### **Autenticação**
- **Tipo**: JWT Bearer Token
- **Header**: `Authorization: Bearer {token}`
- **Duração**: 8 horas
- **Claims**: ID, Nome, Email, Perfil, EmpresaId, NomeEmpresa

### **Documentação**
- **Swagger UI**: `http://localhost:5193/swagger`
- **OpenAPI JSON**: `http://localhost:5193/swagger/v1/swagger.json`

---

## 📊 **Resumo**
- **Total de Endpoints**: 75 endpoints
- **Controllers**: 12 controllers
- **Autenticação**: JWT Bearer Token
- **Versão da API**: v1

---

## 🚀 **Como Usar**

### **1. Autenticação**
```bash
# Login
curl -X POST "http://localhost:5193/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@empresa.com", "senha": "123456"}'
```

### **2. Usar Endpoints Protegidos**
```bash
# Com token JWT
curl -X GET "http://localhost:5193/api/v1/produto" \
  -H "Authorization: Bearer {seu-token-jwt}"
```

### **3. Usar Endpoints de Desenvolvimento**
```bash
# Sem autenticação
curl -X GET "http://localhost:5193/api/v1/produto/dev/all"
```

---

## 📝 **Notas**
- Todos os endpoints principais requerem autenticação JWT
- Endpoints `/dev/all` são para desenvolvimento e não requerem autenticação
- A API filtra automaticamente dados por empresa baseado no token JWT
- Categorias e Status são globais (não filtrados por empresa) 