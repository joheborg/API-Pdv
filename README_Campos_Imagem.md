# Campos de Imagem - Conversão Automática Base64 ↔ Bytes

## Visão Geral

Os campos de imagem foram implementados com conversão automática entre `string` (base64) na aplicação e `LONGBLOB` (bytes) no banco de dados. Isso permite trabalhar com base64 na API enquanto armazena eficientemente os dados como bytes no banco.

## Como Funciona a Conversão

### 1. Na Aplicação (C#)
```csharp
// Na entidade Produto
public string? ImagemBase64 { get; set; }  // String base64

// Na entidade Empresa  
public string? LogoBase64 { get; set; }    // String base64
```

### 2. No Banco de Dados (MySQL)
```sql
-- Tabela Produtos
ImagemBase64 LONGBLOB,    -- Armazenado como bytes

-- Tabela Empresas
LogoBase64 LONGBLOB,      -- Armazenado como bytes
```

### 3. Conversão Automática (Entity Framework)
```csharp
// Configuração no Entity Framework
.HasConversion(
    v => v == null ? null : Convert.FromBase64String(v),  // String → Bytes (INSERT/UPDATE)
    v => v == null ? null : Convert.ToBase64String(v)     // Bytes → String (SELECT)
)
```

## Fluxo de Dados

### Inserção/Atualização (String → Bytes)
1. **API recebe**: String base64
2. **Entity Framework converte**: `Convert.FromBase64String()`
3. **Banco armazena**: LONGBLOB (bytes)

### Consulta (Bytes → String)
1. **Banco retorna**: LONGBLOB (bytes)
2. **Entity Framework converte**: `Convert.ToBase64String()`
3. **API retorna**: String base64

## Campos Implementados

### Produto
```csharp
public string? ImagemBase64 { get; set; }      // Imagem em base64
public string? ImagemNome { get; set; }        // Nome do arquivo
public string? ImagemMimeType { get; set; }    // Tipo MIME
public string? ImagemUrl { get; set; }         // URL externa (mantido)
```

### Empresa
```csharp
public string? LogoBase64 { get; set; }        // Logo em base64
public string? LogoNome { get; set; }          // Nome do arquivo
public string? LogoMimeType { get; set; }      // Tipo MIME
```

## Exemplo de Uso na API

### Criar Produto com Imagem
```json
POST /api/v1/produto
{
  "empresaId": 1,
  "nome": "Produto com Imagem",
  "precoVenda": 100.00,
  "imagemBase64": "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==",
  "imagemNome": "produto.jpg",
  "imagemMimeType": "image/jpeg"
}
```

### Criar Empresa com Logo
```json
POST /api/v1/empresa
{
  "cnpj": "12345678000199",
  "razaoSocial": "Empresa com Logo LTDA",
  "logoBase64": "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==",
  "logoNome": "logo.png",
  "logoMimeType": "image/png",
  "endereco": {
    "logradouro": "Rua das Flores",
    "numero": "123",
    "bairro": "Centro",
    "nomeMunicipio": "São Paulo",
    "uf": "SP",
    "cep": "01234-567"
  }
}
```

## Conversão Manual (se necessário)

### String Base64 para Bytes
```csharp
string base64String = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==";
byte[] imageBytes = Convert.FromBase64String(base64String);
```

### Bytes para String Base64
```csharp
byte[] imageBytes = GetImageBytesFromDatabase();
string base64String = Convert.ToBase64String(imageBytes);
```

## Vantagens da Implementação

### 1. **Eficiência no Banco**
- Armazenamento como bytes (LONGBLOB) é mais eficiente
- Menor uso de espaço em disco
- Melhor performance em consultas

### 2. **Facilidade na API**
- Trabalha com base64 (string) na aplicação
- Fácil de serializar/deserializar JSON
- Compatível com frontend web

### 3. **Conversão Automática**
- Transparente para o desenvolvedor
- Sem necessidade de conversão manual
- Entity Framework gerencia automaticamente

### 4. **Flexibilidade**
- Mantém campo ImagemUrl para URLs externas
- Suporta múltiplos formatos de imagem
- Metadados (nome, mime type) preservados

## Scripts SQL

### Para Tabelas Novas
Execute os scripts:
- `Utils/produto_table_updated.sql`
- `Utils/empresa_table.sql`

### Para Tabelas Existentes
Execute o script:
- `Utils/add_image_fields_migration.sql`

## Tipos MIME Suportados

```csharp
// Exemplos de tipos MIME comuns
"image/jpeg"     // .jpg, .jpeg
"image/png"      // .png
"image/gif"      // .gif
"image/webp"     // .webp
"image/svg+xml"  // .svg
```

## Limitações

1. **Tamanho**: LONGBLOB suporta até 4GB
2. **Performance**: Imagens muito grandes podem impactar performance
3. **Cache**: Considere cache para imagens frequentemente acessadas
4. **Backup**: Backups podem ser maiores devido aos dados binários

## Próximos Passos

1. Execute os scripts SQL para adicionar os campos
2. Teste a criação de produtos/empresas com imagens
3. Implemente validação de tamanho e tipo de imagem
4. Considere implementar compressão de imagem
5. Adicione endpoints para upload/download de imagens 