# Sistema de Login - WebPdv

## Visão Geral
Sistema de autenticação completo com JWT (JSON Web Token) implementado para a API WebPdv.

## Funcionalidades Implementadas

### 1. **Autenticação JWT**
- Login com email e senha
- Geração de token JWT válido por 8 horas
- Validação de senha com hash SHA256
- Atualização automática do último acesso

### 2. **Segurança**
- Hash de senha com SHA256
- Validação de usuário ativo
- Token JWT com claims personalizados
- Configuração de chave secreta

### 3. **Endpoints Disponíveis**

#### Login
```http
POST /api/v1/auth/login
Content-Type: application/json

{
    "email": "admin@webpdv.com",
    "senha": "123456"
}
```

**Resposta de Sucesso:**
```json
{
    "sucesso": true,
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
    "usuario": {
        "id": 1,
        "nome": "Administrador",
        "email": "admin@webpdv.com",
        "perfil": "Admin",
        "empresaId": null,
        "nomeEmpresa": null
    },
    "mensagem": "Login realizado com sucesso"
}
```

#### Logout
```http
POST /api/v1/auth/logout
Authorization: Bearer {token}
```

## Como Usar

### 1. **Configuração**
- O JWT está configurado no `appsettings.json`
- Chave secreta: `SuaChaveSecretaAqui123456789012345678901234567890`
- Issuer e Audience: `WebPdv`

### 2. **Teste do Login**
Execute o script `Utils/usuario_teste.sql` no banco de dados para criar um usuário de teste:

```sql
INSERT INTO Usuarios (Nome, Email, Senha, Perfil, Ativo, CreatedAt, UpdatedAt) VALUES 
('Administrador', 'admin@webpdv.com', 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 'Admin', 1, NOW(), NOW());
```

**Credenciais de teste:**
- **Email:** admin@webpdv.com
- **Senha:** 123456

### 3. **Usando o Token**
Após o login bem-sucedido, use o token retornado no header `Authorization`:

```http
GET /api/v1/produto
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### 4. **Swagger**
O Swagger está configurado para aceitar JWT. Clique no botão "Authorize" e insira:
```
Bearer {seu_token_aqui}
```

## Estrutura do Token JWT

O token contém as seguintes claims:
- `nameidentifier`: ID do usuário
- `name`: Nome do usuário
- `email`: Email do usuário
- `Perfil`: Perfil do usuário (Admin, Gerente, Operador)
- `EmpresaId`: ID da empresa (se aplicável)
- `NomeEmpresa`: Nome da empresa (se aplicável)

## Próximos Passos

### 1. **Proteger Endpoints**
Para proteger endpoints, adicione o atributo `[Authorize]`:

```csharp
[Authorize]
[HttpGet]
public async Task<IActionResult> GetAll()
{
    // Endpoint protegido
}
```

### 2. **Autorização por Perfil**
Para restringir por perfil:

```csharp
[Authorize(Roles = "Admin")]
[HttpPost]
public async Task<IActionResult> Create()
{
    // Apenas administradores
}
```

### 3. **Refresh Token**
Implementar refresh token para renovação automática.

### 4. **Logout com Blacklist**
Implementar blacklist de tokens para logout efetivo.

## Segurança

### Recomendações:
1. **Altere a chave JWT** no `appsettings.json` para uma chave única e segura
2. **Use HTTPS** em produção
3. **Implemente rate limiting** para evitar ataques de força bruta
4. **Adicione validação de complexidade** de senha
5. **Implemente recuperação de senha** por email

### Hash de Senha:
- Algoritmo: SHA256
- Formato: Base64
- Exemplo: `123456` → `jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=`

## Troubleshooting

### Erro de Login:
- Verifique se o usuário existe e está ativo
- Confirme se a senha está correta
- Verifique se o email está no formato correto

### Erro de Token:
- Verifique se o token não expirou (8 horas)
- Confirme se o formato está correto: `Bearer {token}`
- Verifique se a chave JWT está configurada corretamente 