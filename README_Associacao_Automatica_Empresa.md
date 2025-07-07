# Associação Automática de Empresa

## Visão Geral

Implementamos um sistema onde a empresa é automaticamente associada ao usuário logado, eliminando a necessidade de selecionar manualmente a empresa em cada operação.

## Mudanças Implementadas

### 1. Helper para Usuário Logado

Criamos o arquivo `API/Utils/UserHelper.cs` com métodos para obter informações do usuário logado:

```csharp
public static class UserHelper
{
    public static int? GetCurrentUserEmpresaId(HttpContext httpContext)
    public static int GetCurrentUserId(HttpContext httpContext)
    public static string GetCurrentUserEmail(HttpContext httpContext)
    public static string GetCurrentUserName(HttpContext httpContext)
    public static string GetCurrentUserProfile(HttpContext httpContext)
}
```

### 2. Controllers Atualizados

#### ProdutoController
- **GET /api/v1/produto**: Agora retorna apenas produtos da empresa do usuário logado
- **POST /api/v1/produto**: Automaticamente associa a empresa do usuário ao produto
- **GET /api/v1/produto/codigo/{codigoProduto}**: Busca por código na empresa do usuário
- Removido parâmetro empresaId das rotas

#### PedidoController
- **GET /api/v1/pedido**: Retorna apenas pedidos da empresa do usuário
- **POST /api/v1/pedido**: Automaticamente associa a empresa do usuário ao pedido
- **GET /api/v1/pedido/comanda/{numeroComanda}**: Busca comanda na empresa do usuário
- **GET /api/v1/pedido/abertos**: Lista pedidos abertos da empresa do usuário
- Removido parâmetro empresaId das rotas

#### CaixaController
- **GET /api/v1/caixa**: Retorna apenas caixas da empresa do usuário
- **POST /api/v1/caixa**: Automaticamente associa a empresa do usuário ao caixa

#### MotoboyController
- **GET /api/v1/motoboy**: Retorna apenas motoboys da empresa do usuário
- **POST /api/v1/motoboy**: Automaticamente associa a empresa do usuário ao motoboy

### 3. Repositórios Atualizados

Adicionamos o método `GetByEmpresaAsync` aos seguintes repositórios:
- `PedidoRepository`
- `CaixaRepository`
- `MotoboyRepository`

E atualizamos as interfaces correspondentes:
- `IPedido`
- `ICaixa`
- `IMotoboy`

### 4. Validações de Segurança

Todos os controllers agora validam se o usuário possui empresa associada:

```csharp
var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext);
if (!empresaId.HasValue)
{
    return BadRequest("Usuário não possui empresa associada");
}
```

## Benefícios

### 1. Segurança
- Usuários só podem acessar dados de sua própria empresa
- Elimina risco de acesso cruzado entre empresas
- Validação automática de permissões

### 2. Simplicidade
- Não é necessário especificar empresaId nos requests
- Interface mais limpa e intuitiva
- Menos parâmetros para gerenciar

### 3. Consistência
- Todos os dados são automaticamente associados à empresa correta
- Elimina erros de associação incorreta
- Comportamento uniforme em toda a API

## Como Funciona

### 1. Login
Quando o usuário faz login, o token JWT inclui o `empresaId`:

```json
{
  "empresaId": 1,
  "nomeEmpresa": "Empresa Exemplo Ltda"
}
```

### 2. Operações
Em cada operação, a API:
1. Extrai o `empresaId` do token JWT
2. Valida se o usuário possui empresa associada
3. Automaticamente associa a empresa aos novos registros
4. Filtra consultas pela empresa do usuário

### 3. Exemplo de Uso

**Antes:**
```http
POST /api/v1/produto
{
  "nome": "Produto",
  "precoVenda": 10.00,
  "empresaId": 1  // ← Necessário especificar
}
```

**Agora:**
```http
POST /api/v1/produto
{
  "nome": "Produto",
  "precoVenda": 10.00
  // empresaId é automaticamente definido
}
```

## Endpoints Afetados

### Produtos
- ✅ `GET /api/v1/produto` - Filtrado por empresa
- ✅ `POST /api/v1/produto` - Empresa automática
- ✅ `GET /api/v1/produto/codigo/{codigo}` - Empresa automática

### Pedidos
- ✅ `GET /api/v1/pedido` - Filtrado por empresa
- ✅ `POST /api/v1/pedido` - Empresa automática
- ✅ `GET /api/v1/pedido/comanda/{numero}` - Empresa automática
- ✅ `GET /api/v1/pedido/abertos` - Filtrado por empresa

### Caixas
- ✅ `GET /api/v1/caixa` - Filtrado por empresa
- ✅ `POST /api/v1/caixa` - Empresa automática

### Motoboys
- ✅ `GET /api/v1/motoboy` - Filtrado por empresa
- ✅ `POST /api/v1/motoboy` - Empresa automática

## Endpoints Não Afetados

### Categorias
- `GET /api/v1/categoria` - Não tem empresa (global)
- `POST /api/v1/categoria` - Não tem empresa (global)

### Usuários
- `GET /api/v1/usuario` - Mantém acesso completo (admin)
- `GET /api/v1/usuario/empresa/{empresaId}` - Endpoint específico por empresa

## Migração

### Para Frontend
1. Remover parâmetros `empresaId` dos requests
2. Atualizar URLs que incluíam `empresaId`
3. Garantir que o token JWT está sendo enviado corretamente

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

## Testes

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

## Considerações de Segurança

1. **Validação de Token**: Todos os endpoints validam o token JWT
2. **Empresa Obrigatória**: Usuários sem empresa não podem realizar operações
3. **Filtro Automático**: Consultas são automaticamente filtradas por empresa
4. **Associação Automática**: Novos registros são automaticamente associados

## Próximos Passos

1. Implementar testes automatizados
2. Adicionar logs de auditoria
3. Considerar implementar cache por empresa
4. Avaliar performance com grandes volumes de dados 