# Solução para Erro 400 - Autenticação Obrigatória

## Problema Identificado

O erro 400 (Bad Request) está ocorrendo porque a API agora requer autenticação JWT, mas o frontend não está enviando o token de autorização.

### Erro Atual:
```
fail: WebPdv.Services.ProdutoService[0]
      Erro ao buscar produtos da API
      System.Net.Http.HttpRequestException: Response status code does not indicate success: 400 (Bad Request).
```

## Soluções Implementadas

### 1. Endpoints de Desenvolvimento (Solução Temporária)

Adicionei endpoints de desenvolvimento que não requerem autenticação para facilitar os testes:

#### Produtos:
- **URL**: `GET /api/v1/produto/dev/all`
- **Autenticação**: Não requerida
- **Retorna**: Todos os produtos do sistema

#### Pedidos:
- **URL**: `GET /api/v1/pedido/dev/all`
- **Autenticação**: Não requerida
- **Retorna**: Todos os pedidos do sistema

#### Categorias:
- **URL**: `GET /api/v1/categoria/dev/all`
- **Autenticação**: Não requerida
- **Retorna**: Todas as categorias do sistema

### 2. Como Usar os Endpoints de Desenvolvimento

Atualize seu frontend para usar os endpoints de desenvolvimento:

```csharp
// Em ProdutoService.cs
public async Task<IEnumerable<Produto>> GetAllAsync()
{
    try
    {
        var response = await _httpClient.GetAsync("api/v1/produto/dev/all");
        response.EnsureSuccessStatusCode();
        
        var content = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<IEnumerable<Produto>>(content, _jsonOptions);
    }
    catch (HttpRequestException ex)
    {
        _logger.LogError(ex, "Erro ao buscar produtos da API");
        throw new Exception("Erro ao conectar com a API de produtos", ex);
    }
}
```

## Solução Permanente: Implementar Autenticação

### 1. Fluxo de Autenticação

```csharp
// 1. Login para obter token
public async Task<LoginResponse> LoginAsync(string email, string senha)
{
    var loginRequest = new { Email = email, Senha = senha };
    var json = JsonSerializer.Serialize(loginRequest);
    var content = new StringContent(json, Encoding.UTF8, "application/json");
    
    var response = await _httpClient.PostAsync("api/v1/auth/login", content);
    response.EnsureSuccessStatusCode();
    
    var responseContent = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<LoginResponse>(responseContent);
}

// 2. Armazenar token
private string _authToken;

public void SetAuthToken(string token)
{
    _authToken = token;
    _httpClient.DefaultRequestHeaders.Authorization = 
        new AuthenticationHeaderValue("Bearer", token);
}

// 3. Usar endpoints autenticados
public async Task<IEnumerable<Produto>> GetAllAsync()
{
    if (string.IsNullOrEmpty(_authToken))
    {
        throw new UnauthorizedAccessException("Token de autenticação não encontrado");
    }
    
    var response = await _httpClient.GetAsync("api/v1/produto");
    response.EnsureSuccessStatusCode();
    
    var content = await response.Content.ReadAsStringAsync();
    return JsonSerializer.Deserialize<IEnumerable<Produto>>(content, _jsonOptions);
}
```

### 2. Modelos de Dados

```csharp
public class LoginRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
}

public class LoginResponse
{
    public bool Sucesso { get; set; }
    public string Token { get; set; }
    public UsuarioInfo Usuario { get; set; }
    public string Mensagem { get; set; }
}

public class UsuarioInfo
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Perfil { get; set; }
    public int? EmpresaId { get; set; }
    public string NomeEmpresa { get; set; }
}
```

### 3. Implementação no Frontend

```csharp
// Program.cs ou Startup.cs
services.AddHttpClient<IAuthService, AuthService>();
services.AddHttpClient<IProdutoService, ProdutoService>();

// AuthService.cs
public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<AuthService> _logger;

    public AuthService(HttpClient httpClient, ILogger<AuthService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<bool> LoginAsync(string email, string senha)
    {
        try
        {
            var loginRequest = new { Email = email, Senha = senha };
            var json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            var response = await _httpClient.PostAsync("api/v1/auth/login", content);
            
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);
                
                if (loginResponse.Sucesso)
                {
                    // Armazenar token
                    _httpClient.DefaultRequestHeaders.Authorization = 
                        new AuthenticationHeaderValue("Bearer", loginResponse.Token);
                    return true;
                }
            }
            
            return false;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro durante login");
            return false;
        }
    }
}
```

## Endpoints Disponíveis

### Autenticação:
- `POST /api/v1/auth/login` - Login do usuário

### Produtos (Com Autenticação):
- `GET /api/v1/produto` - Listar produtos da empresa do usuário
- `GET /api/v1/produto/{id}` - Buscar produto por ID
- `POST /api/v1/produto` - Criar produto
- `PUT /api/v1/produto/{id}` - Atualizar produto
- `DELETE /api/v1/produto/{id}` - Deletar produto

### Produtos (Desenvolvimento):
- `GET /api/v1/produto/dev/all` - Listar todos os produtos (sem autenticação)

### Pedidos (Com Autenticação):
- `GET /api/v1/pedido` - Listar pedidos da empresa do usuário
- `GET /api/v1/pedido/{id}` - Buscar pedido por ID
- `POST /api/v1/pedido` - Criar pedido
- `PUT /api/v1/pedido/{id}` - Atualizar pedido
- `DELETE /api/v1/pedido/{id}` - Deletar pedido

### Pedidos (Desenvolvimento):
- `GET /api/v1/pedido/dev/all` - Listar todos os pedidos (sem autenticação)

## Próximos Passos

1. **Imediato**: Use os endpoints `/dev/all` para continuar o desenvolvimento
2. **Curto Prazo**: Implemente a autenticação no frontend
3. **Médio Prazo**: Remova os endpoints de desenvolvimento quando a autenticação estiver funcionando

## Dados de Teste

Para testar a autenticação, use um dos usuários existentes no banco:

```sql
-- Verificar usuários existentes
SELECT id, nome, email, perfil, empresa_id FROM usuarios WHERE ativo = 1;
```

## Configuração JWT

O JWT está configurado em `appsettings.json`:

```json
{
  "Jwt": {
    "Key": "SuaChaveSecretaAqui123456789012345678901234567890",
    "Issuer": "WebPdv",
    "Audience": "WebPdv"
  }
}
```

O token é válido por 8 horas e inclui as seguintes claims:
- `NameIdentifier`: ID do usuário
- `Name`: Nome do usuário
- `Email`: Email do usuário
- `Perfil`: Perfil do usuário
- `EmpresaId`: ID da empresa
- `NomeEmpresa`: Nome da empresa 