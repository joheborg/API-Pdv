# Implementação da Tabela Categoria

## Visão Geral
Foi implementada uma tabela de categoria completa com CRUD (Create, Read, Update, Delete) seguindo o padrão arquitetural do projeto.

## Estrutura Criada

### 1. Entidade (Domain/Entities/Categoria.cs)
- **Id**: Chave primária auto-incremento
- **Descricao**: Campo obrigatório com máximo de 100 caracteres
- **CreatedAt**: Data de criação
- **UpdatedAt**: Data de atualização

### 2. Interface do Repositório (Interfaces/Repositories/ICategoria.cs)
Métodos implementados:
- `GetAllAsync()`: Lista todas as categorias
- `GetByIdAsync(int id)`: Busca categoria por ID
- `CreateAsync(Categoria categoria)`: Cria nova categoria
- `UpdateAsync(Categoria categoria)`: Atualiza categoria existente
- `DeleteAsync(int id)`: Remove categoria

### 3. Implementação do Repositório (Infraestructure/Repositories/Categoria.cs)
- Implementação completa dos métodos da interface
- Tratamento de erros para registros não encontrados
- Controle automático de datas (CreatedAt/UpdatedAt)

### 4. Configuração Entity Framework (Infraestructure/Data/Configurations/CategoriaConfiguration.cs)
- Configuração da tabela "Categorias"
- Definição de chave primária e propriedades
- Configuração de constraints e tipos de dados

### 5. Contexto do Banco (Infraestructure/Data/Context/ApplicationDbContext.cs)
- Adicionado DbSet<Categoria> para acesso à tabela

### 6. Controller (Controller/V1/Categoria.cs)
Endpoints implementados:
- `GET /api/v1/categoria` - Lista todas as categorias
- `GET /api/v1/categoria/{id}` - Busca categoria por ID
- `POST /api/v1/categoria` - Cria nova categoria
- `PUT /api/v1/categoria/{id}` - Atualiza categoria
- `DELETE /api/v1/categoria/{id}` - Remove categoria

### 7. Registro de Dependências (Program.cs)
- Adicionado registro do serviço ICategoria

### 8. Script SQL (Utils/categoria_table.sql)
- Script para criar a tabela no banco de dados
- Inserção de categorias de exemplo

## Como Usar

### 1. Executar o Script SQL
Execute o arquivo `Utils/categoria_table.sql` no seu banco de dados MySQL.

### 2. Testar os Endpoints

#### Listar todas as categorias:
```http
GET /api/v1/categoria
```

#### Buscar categoria por ID:
```http
GET /api/v1/categoria/1
```

#### Criar nova categoria:
```http
POST /api/v1/categoria
Content-Type: application/json

{
    "descricao": "Nova Categoria"
}
```

#### Atualizar categoria:
```http
PUT /api/v1/categoria/1
Content-Type: application/json

{
    "id": 1,
    "descricao": "Categoria Atualizada"
}
```

#### Deletar categoria:
```http
DELETE /api/v1/categoria/1
```

## Integração com Produtos
A tabela de categorias pode ser facilmente integrada com a tabela de produtos através de uma chave estrangeira, permitindo categorizar os produtos de forma organizada.

## Próximos Passos
1. Executar o script SQL para criar a tabela
2. Testar os endpoints via Swagger ou Postman
3. Integrar com a tabela de produtos se necessário
4. Adicionar validações específicas se necessário 