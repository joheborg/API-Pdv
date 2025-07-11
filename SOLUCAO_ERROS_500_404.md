# Solução para Erros 500 e 404 na API

## Problemas Identificados

### 1. Erro 500 ao criar produtos
- **Causa**: O controller de produtos está tentando obter `EmpresaId` do usuário autenticado, mas o endpoint não está protegido por autenticação
- **Solução**: Criado endpoint de desenvolvimento `/api/v1/produto/dev/create` que não requer autenticação

### 2. Erro 404 ao buscar pedidos
- **Causa**: O controller de pedidos requer autenticação (`[Authorize]`), mas o token JWT pode não estar sendo enviado corretamente
- **Solução**: Criado endpoint de desenvolvimento `/api/v1/pedido/dev/all` e `/api/v1/pedido/dev/empresa/{empresaId}` que não requerem autenticação

## Endpoints de Desenvolvimento Criados

### Produtos
- `GET /api/v1/produto/dev/all` - Listar todos os produtos (sem autenticação)
- `POST /api/v1/produto/dev/create` - Criar produto (sem autenticação, usa EmpresaId = 1)

### Pedidos
- `GET /api/v1/pedido/dev/all` - Listar todos os pedidos (sem autenticação)
- `GET /api/v1/pedido/dev/empresa/{empresaId}` - Listar pedidos por empresa (sem autenticação)

## Como Testar

### 1. Testar criação de produtos
```bash
curl -X POST "https://localhost:5193/api/v1/produto/dev/create" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "Produto Teste",
    "descricao": "Descrição do produto teste",
    "precoVenda": 10.50,
    "precoCusto": 8.00,
    "codigoProduto": "TEST001"
  }'
```

### 2. Testar busca de pedidos
```bash
curl -X GET "https://localhost:5193/api/v1/pedido/dev/all"
```

### 3. Testar busca de pedidos por empresa
```bash
curl -X GET "https://localhost:5193/api/v1/pedido/dev/empresa/1"
```

## Scripts SQL para Preparar Banco de Dados

### 1. Verificar estrutura das tabelas
Execute o script `check_pedidos_table.sql` para verificar se as tabelas existem e têm dados.

### 2. Inserir dados de teste
Execute o script `insert_test_data.sql` para inserir dados de teste nas tabelas.

## Próximos Passos

1. **Teste os endpoints de desenvolvimento** para verificar se a API está funcionando
2. **Verifique a configuração de autenticação** no frontend (WebPdv)
3. **Implemente autenticação adequada** quando necessário
4. **Atualize o frontend** para usar os endpoints corretos

## Configuração de Autenticação

Para usar os endpoints protegidos, o frontend deve:

1. **Fazer login** para obter o token JWT
2. **Incluir o token** no header Authorization: `Bearer {token}`
3. **Incluir a claim EmpresaId** no token JWT

### Exemplo de token JWT válido:
```json
{
  "sub": "1",
  "email": "usuario@teste.com",
  "EmpresaId": "1",
  "Perfil": "Admin",
  "exp": 1234567890
}
```

## Logs de Debug

Para habilitar logs detalhados, adicione no `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "API_Pdv": "Debug"
    }
  }
}
``` 