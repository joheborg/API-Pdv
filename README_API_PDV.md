# API PDV - Sistema de Ponto de Venda

## 📋 Visão Geral

Esta é uma API REST completa para gerenciamento de sistema de PDV (Ponto de Venda), desenvolvida em ASP.NET Core com Entity Framework Core e MySQL.

## 🚀 Funcionalidades Principais

- **Autenticação JWT** - Sistema seguro de login/logout
- **Gestão de Empresas** - Cadastro e configuração de empresas
- **Gestão de Usuários** - Controle de acesso e perfis
- **Gestão de Produtos** - Cadastro completo com dados fiscais
- **Gestão de Categorias** - Organização de produtos
- **Gestão de Pedidos** - Controle completo de vendas
- **Gestão de Caixas** - Controle de abertura/fechamento
- **Gestão de Pagamentos** - Registro de formas de pagamento
- **Gestão de Motoboys** - Controle de entregadores
- **Gestão de Funcionários** - Cadastro de colaboradores

## 📚 Documentação

### Arquivos de Documentação

1. **[Documentacao_Endpoints.md](./Documentacao_Endpoints.md)** - Documentação completa de todos os endpoints
2. **[Exemplos_Uso_Endpoints.md](./Exemplos_Uso_Endpoints.md)** - Exemplos práticos de uso
3. **[Estrutura_Banco_Dados.md](./Estrutura_Banco_Dados.md)** - Estrutura do banco de dados

## 🛠️ Tecnologias Utilizadas

- **.NET 9.0** - Framework principal
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM
- **MySQL** - Banco de dados
- **JWT** - Autenticação
- **Swagger** - Documentação da API

## 🏗️ Arquitetura

```
API-Pdv/
├── Controller/V1/          # Controllers da API
├── Domain/Entities/        # Entidades do domínio
├── Infraestructure/        # Camada de infraestrutura
│   ├── Data/              # Contexto do EF e configurações
│   ├── Repositories/      # Implementações dos repositórios
│   └── Services/          # Serviços de negócio
├── Interfaces/            # Contratos e interfaces
├── Models/               # DTOs e modelos de request/response
├── Migrations/           # Migrações do Entity Framework
└── UseCases/            # Casos de uso da aplicação
```

## 🔧 Configuração e Instalação

### Pré-requisitos

- .NET 9.0 SDK
- MySQL 8.0+
- Visual Studio 2022 ou VS Code

### Passos para Instalação

1. **Clone o repositório**
```bash
git clone [url-do-repositorio]
cd API-Pdv
```

2. **Configure o banco de dados**
```bash
# Edite a connection string em appsettings.json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=pdv_db;User=root;Password=sua_senha;"
}
```

3. **Execute as migrações**
```bash
dotnet ef database update
```

4. **Execute o projeto**
```bash
dotnet run
```

5. **Acesse a documentação Swagger**
```
http://localhost:5000/swagger
```

## 🔐 Autenticação

A API utiliza JWT (JSON Web Tokens) para autenticação.

### Fluxo de Autenticação

1. **Login**
```http
POST /api/v1/auth/login
Content-Type: application/json

{
  "email": "admin@empresa.com",
  "senha": "123456"
}
```

2. **Usar Token**
```http
GET /api/v1/produto
Authorization: Bearer {seu_token_jwt}
```

## 📊 Principais Endpoints

### Autenticação
- `POST /api/v1/auth/login` - Login do usuário
- `POST /api/v1/auth/logout` - Logout do usuário

### Usuários
- `GET /api/v1/usuario` - Listar usuários
- `POST /api/v1/usuario` - Criar usuário
- `PUT /api/v1/usuario/{id}` - Atualizar usuário
- `DELETE /api/v1/usuario/{id}` - Remover usuário

### Produtos
- `GET /api/v1/produto` - Listar produtos
- `POST /api/v1/produto` - Criar produto
- `GET /api/v1/produto/barras/{codigo}` - Buscar por código de barras
- `GET /api/v1/produto/empresa/{empresaId}` - Produtos por empresa

### Pedidos
- `GET /api/v1/pedido` - Listar pedidos
- `GET /api/v1/pedido/{id}` - Buscar pedido por ID
- `GET /api/v1/pedido/comanda/{empresaId}/{numeroComanda}` - Buscar pedido por número da comanda
- `GET /api/v1/pedido/abertos/{empresaId}` - Listar pedidos abertos
- `POST /api/v1/pedido` - Criar pedido
- `PUT /api/v1/pedido/{id}` - Atualizar pedido

### Empresas
- `GET /api/v1/empresa` - Listar empresas
- `POST /api/v1/empresa` - Criar empresa
- `GET /api/v1/empresa/cnpj/{cnpj}` - Buscar por CNPJ

## 🗄️ Estrutura do Banco de Dados

### Tabelas Principais

- **Empresas** - Dados das empresas
- **Usuarios** - Usuários do sistema
- **Produtos** - Cadastro de produtos
- **Categorias** - Categorias de produtos
- **Pedidos** - Pedidos de venda
- **ItensPedido** - Itens dos pedidos
- **StatusPedidos** - Status dos pedidos
- **Caixas** - Controle de caixas
- **PagamentosCaixa** - Pagamentos registrados
- **Motoboys** - Entregadores
- **Funcionarios** - Colaboradores

## 🔄 Fluxo de Venda

1. **Abrir Caixa** - Registrar abertura do caixa
2. **Criar Pedido** - Iniciar novo pedido
3. **Adicionar Itens** - Incluir produtos no pedido
4. **Registrar Pagamento** - Registrar forma de pagamento
5. **Atualizar Status** - Acompanhar status do pedido
6. **Fechar Caixa** - Finalizar operação do caixa

## 📈 Monitoramento

### Logs Importantes

- Login/logout de usuários
- Criação/edição de pedidos
- Abertura/fechamento de caixas
- Registro de pagamentos
- Alterações em produtos

### Métricas Recomendadas

- Total de vendas por período
- Produtos mais vendidos
- Tempo médio de atendimento
- Taxa de conversão
- Satisfação do cliente

## 🔒 Segurança

### Medidas Implementadas

- **Autenticação JWT** - Tokens seguros com expiração
- **Validação de Dados** - Validação em todos os inputs
- **Controle de Acesso** - Perfis de usuário
- **Logs de Auditoria** - Registro de todas as operações
- **Sanitização** - Prevenção de SQL Injection

### Boas Práticas

- Use HTTPS em produção
- Implemente rate limiting
- Faça backup regular dos dados
- Monitore logs de erro
- Mantenha dependências atualizadas

## 🧪 Testes

### Testes Recomendados

1. **Testes de Autenticação**
   - Login com credenciais válidas
   - Login com credenciais inválidas
   - Acesso a endpoints protegidos

2. **Testes de CRUD**
   - Criação de registros
   - Leitura de registros
   - Atualização de registros
   - Remoção de registros

3. **Testes de Negócio**
   - Fluxo completo de venda
   - Controle de estoque
   - Cálculo de impostos
   - Fechamento de caixa

## 🚀 Deploy

### Ambiente de Desenvolvimento

```bash
dotnet run --environment Development
```

### Ambiente de Produção

```bash
dotnet publish -c Release
dotnet run --environment Production
```

### Docker (Opcional)

```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:9.0
COPY bin/Release/net9.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "API-Pdv.dll"]
```

## 📞 Suporte

### Contato
- **Email**: suporte@empresa.com
- **Documentação**: [Link para documentação completa]
- **Issues**: [Link para repositório de issues]

### Recursos Adicionais

- [Documentação do ASP.NET Core](https://docs.microsoft.com/pt-br/aspnet/core/)
- [Documentação do Entity Framework](https://docs.microsoft.com/pt-br/ef/)
- [Documentação do JWT](https://jwt.io/)

## 📝 Licença

Este projeto está sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

## 🤝 Contribuição

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

---

**Desenvolvido com ❤️ para facilitar a gestão de PDVs** 