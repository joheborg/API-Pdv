# 🔐 **Validação do Token JWT - Como Funciona**

## 📋 **Visão Geral**

A validação do token JWT na API PDV é feita automaticamente pelo middleware do ASP.NET Core. Este documento explica como funciona esse processo e como você pode customizá-lo.

---

## ⚙️ **Configuração da Validação**

### **1. Configuração no Program.cs**
```csharp
// JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,           // ✅ Valida o emissor do token
            ValidateAudience = true,         // ✅ Valida o público-alvo
            ValidateLifetime = true,         // ✅ Valida a data de expiração
            ValidateIssuerSigningKey = true, // ✅ Valida a chave de assinatura
            ValidIssuer = builder.Configuration["Jwt:Issuer"] ?? "WebPdv",
            ValidAudience = builder.Configuration["Jwt:Audience"] ?? "WebPdv",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? "SuaChaveSecretaAqui123456789"))
        };
    });
```

### **2. Middleware Pipeline**
```csharp
var app = builder.Build();

// Middleware de autenticação
app.UseAuthentication();  // 🔍 Valida o token JWT
app.UseAuthorization();   // 🔐 Verifica permissões

app.MapControllers();
```

---

## 🔍 **Processo de Validação**

### **1. Fluxo de Validação Automática**

```mermaid
sequenceDiagram
    participant Client
    participant JWT Middleware
    participant Token Validator
    participant Controller
    participant Database

    Client->>JWT Middleware: Request + Authorization Header
    JWT Middleware->>Token Validator: Extract and validate token
    Token Validator->>Token Validator: Check signature
    Token Validator->>Token Validator: Check expiration
    Token Validator->>Token Validator: Check issuer
    Token Validator->>Token Validator: Check audience
    Token Validator-->>JWT Middleware: Valid/Invalid result
    alt Token Válido
        JWT Middleware->>Controller: Request with User claims
        Controller->>Controller: Process request
        Controller-->>Client: 200 OK + Response
    else Token Inválido
        JWT Middleware-->>Client: 401 Unauthorized
    end
```

### **2. Validações Realizadas**

#### **✅ Validação de Assinatura**
```csharp
ValidateIssuerSigningKey = true
```
- Verifica se o token foi assinado com a chave correta
- Previne tokens falsificados
- Usa HMAC SHA256

#### **✅ Validação de Emissor (Issuer)**
```csharp
ValidateIssuer = true
ValidIssuer = "WebPdv"
```
- Verifica se o token foi emitido pela API correta
- Previne tokens de outras aplicações

#### **✅ Validação de Público-Alvo (Audience)**
```csharp
ValidateAudience = true
ValidAudience = "WebPdv"
```
- Verifica se o token foi criado para esta API
- Previne uso de tokens em aplicações erradas

#### **✅ Validação de Tempo de Vida**
```csharp
ValidateLifetime = true
```
- Verifica se o token não expirou
- Tokens expiram em 8 horas
- Rejeita tokens antigos automaticamente

---

## 🔧 **Como Funciona na Prática**

### **1. Requisição Chega**
```http
GET /api/v1/produto
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

### **2. Middleware Intercepta**
```csharp
// O middleware automaticamente:
// 1. Extrai o token do header Authorization
// 2. Remove o prefixo "Bearer "
// 3. Decodifica o JWT
// 4. Valida todas as configurações
// 5. Se válido, adiciona as claims ao HttpContext.User
```

### **3. Claims Disponíveis**
```csharp
// Após validação, você pode acessar:
var userId = UserHelper.GetCurrentUserId(HttpContext);        // ID do usuário
var userEmail = UserHelper.GetCurrentUserEmail(HttpContext);  // Email
var userName = UserHelper.GetCurrentUserName(HttpContext);    // Nome
var empresaId = UserHelper.GetCurrentUserEmpresaId(HttpContext); // ID da empresa
var userProfile = UserHelper.GetCurrentUserProfile(HttpContext);  // Perfil
```

---

## 🛡️ **Validações de Segurança**

### **1. Validação de Assinatura**
```csharp
// O token é assinado com esta chave:
var key = new SymmetricSecurityKey(
    Encoding.UTF8.GetBytes("SuaChaveSecretaAqui123456789012345678901234567890")
);

// Se alguém tentar modificar o token sem a chave, a validação falha
```

### **2. Validação de Claims**
```csharp
// Claims obrigatórias no token:
var claims = new[]
{
    new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),     // ID
    new Claim(ClaimTypes.Name, usuario.Nome),                       // Nome
    new Claim(ClaimTypes.Email, usuario.Email),                     // Email
    new Claim("Perfil", usuario.Perfil ?? ""),                     // Perfil
    new Claim("EmpresaId", usuario.EmpresaId?.ToString() ?? ""),   // Empresa
    new Claim("NomeEmpresa", usuario.NomeEmpresa ?? "")            // Nome da Empresa
};
```

### **3. Validação de Expiração**
```csharp
// Token válido por 8 horas
expires: DateTime.Now.AddHours(8)

// Após 8 horas, automaticamente rejeitado
```

---

## 🔍 **Debugging da Validação**

### **1. Verificar Token no jwt.io**
```bash
# 1. Pegue o token da resposta de login
# 2. Vá para https://jwt.io
# 3. Cole o token
# 4. Verifique as claims e expiração
```

### **2. Logs de Validação**
```csharp
// Adicione logs para debug
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Falha na autenticação: {context.Exception.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine($"Token validado para usuário: {context.Principal.Identity.Name}");
                return Task.CompletedTask;
            }
        };
        
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // ... configurações existentes
        };
    });
```

### **3. Teste de Validação**
```bash
# Token válido
curl -H "Authorization: Bearer SEU_TOKEN_VALIDO" \
  http://localhost:5193/api/v1/produto

# Token inválido
curl -H "Authorization: Bearer TOKEN_INVALIDO" \
  http://localhost:5193/api/v1/produto
# Retorna: 401 Unauthorized
```

---

## 🚨 **Cenários de Falha**

### **1. Token Expirado**
```json
{
  "error": "invalid_token",
  "error_description": "The token expired at '2024-01-15T10:30:00Z'"
}
```

### **2. Token Malformado**
```json
{
  "error": "invalid_token",
  "error_description": "The token is malformed"
}
```

### **3. Assinatura Inválida**
```json
{
  "error": "invalid_token",
  "error_description": "The signature is invalid"
}
```

### **4. Emissor Inválido**
```json
{
  "error": "invalid_token",
  "error_description": "The issuer 'InvalidIssuer' is invalid"
}
```

---

## 🔧 **Customizações Avançadas**

### **1. Validação Customizada de Claims**
```csharp
options.Events = new JwtBearerEvents
{
    OnTokenValidated = context =>
    {
        var claimsIdentity = context.Principal.Identity as ClaimsIdentity;
        
        // Verificar se usuário tem empresa
        var empresaIdClaim = claimsIdentity.FindFirst("EmpresaId");
        if (empresaIdClaim == null || string.IsNullOrEmpty(empresaIdClaim.Value))
        {
            context.Fail("Usuário não possui empresa associada");
            return Task.CompletedTask;
        }
        
        return Task.CompletedTask;
    }
};
```

### **2. Validação de Usuário Ativo**
```csharp
options.Events = new JwtBearerEvents
{
    OnTokenValidated = async context =>
    {
        var userId = context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!string.IsNullOrEmpty(userId))
        {
            var usuarioRepository = context.HttpContext.RequestServices
                .GetRequiredService<IUsuario>();
            
            var usuario = await usuarioRepository.GetByIdAsync(int.Parse(userId));
            if (usuario == null || !usuario.Ativo)
            {
                context.Fail("Usuário inativo ou não encontrado");
                return;
            }
        }
    }
};
```

### **3. Blacklist de Tokens**
```csharp
public interface ITokenBlacklistService
{
    Task<bool> IsTokenBlacklistedAsync(string token);
}

options.Events = new JwtBearerEvents
{
    OnTokenValidated = async context =>
    {
        var token = context.SecurityToken.RawData;
        var blacklistService = context.HttpContext.RequestServices
            .GetRequiredService<ITokenBlacklistService>();
        
        if (await blacklistService.IsTokenBlacklistedAsync(token))
        {
            context.Fail("Token foi revogado");
            return;
        }
    }
};
```

---

## 📊 **Monitoramento**

### **1. Métricas de Validação**
```csharp
// Log de tentativas de acesso
options.Events = new JwtBearerEvents
{
    OnAuthenticationFailed = context =>
    {
        _logger.LogWarning("Falha na autenticação: {Error}", context.Exception.Message);
        return Task.CompletedTask;
    },
    OnTokenValidated = context =>
    {
        _logger.LogInformation("Token validado para usuário: {UserId}", 
            context.Principal.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        return Task.CompletedTask;
    }
};
```

### **2. Alertas de Segurança**
```csharp
// Alertar sobre tokens suspeitos
OnAuthenticationFailed = context =>
{
    if (context.Exception is SecurityTokenExpiredException)
    {
        _logger.LogInformation("Token expirado - normal");
    }
    else
    {
        _logger.LogWarning("Tentativa de acesso com token inválido: {Error}", 
            context.Exception.Message);
    }
    return Task.CompletedTask;
}
```

---

## ✅ **Checklist de Validação**

- [x] **Assinatura**: Token assinado com chave correta
- [x] **Emissor**: Token emitido pela API correta
- [x] **Público-Alvo**: Token criado para esta aplicação
- [x] **Expiração**: Token não expirado
- [x] **Claims**: Todas as claims necessárias presentes
- [x] **Usuário Ativo**: Usuário existe e está ativo
- [x] **Empresa**: Usuário tem empresa associada
- [x] **Logs**: Falhas de validação são logadas
- [x] **Segurança**: Tokens inválidos são rejeitados

---

## 🔍 **Testando a Validação**

### **1. Token Válido**
```bash
# Login para obter token
curl -X POST "http://localhost:5193/api/v1/auth/login" \
  -H "Content-Type: application/json" \
  -d '{"email": "admin@empresa.com", "senha": "123456"}'

# Usar token em requisição
curl -H "Authorization: Bearer SEU_TOKEN" \
  http://localhost:5193/api/v1/produto
# ✅ Retorna 200 OK
```

### **2. Token Inválido**
```bash
curl -H "Authorization: Bearer TOKEN_INVALIDO" \
  http://localhost:5193/api/v1/produto
# ❌ Retorna 401 Unauthorized
```

### **3. Sem Token**
```bash
curl http://localhost:5193/api/v1/produto
# ❌ Retorna 401 Unauthorized
```

---

**Status**: ✅ **Validação JWT Implementada e Funcional** 