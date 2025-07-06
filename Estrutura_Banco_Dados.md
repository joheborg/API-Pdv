# Estrutura do Banco de Dados - API PDV

## Visão Geral

O sistema utiliza MySQL como banco de dados principal, com Entity Framework Core para ORM. A estrutura segue o padrão de relacionamentos entre entidades para um sistema de PDV completo.

## Tabelas Principais

### 1. Empresas
```sql
CREATE TABLE Empresas (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CNPJ VARCHAR(14) NOT NULL UNIQUE,
    RazaoSocial VARCHAR(200) NOT NULL,
    NomeFantasia VARCHAR(200),
    InscricaoEstadual VARCHAR(50),
    CRT VARCHAR(5),
    LogoBase64 LONGTEXT,
    LogoNome VARCHAR(255),
    LogoMimeType VARCHAR(100),
    Logradouro VARCHAR(255),
    Numero VARCHAR(20),
    Complemento VARCHAR(100),
    Bairro VARCHAR(100),
    CodigoMunicipio VARCHAR(10),
    NomeMunicipio VARCHAR(100),
    UF VARCHAR(2),
    CEP VARCHAR(10),
    CodigoPais VARCHAR(10) DEFAULT '1058',
    NomePais VARCHAR(50) DEFAULT 'Brasil',
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

### 2. Usuários
```sql
CREATE TABLE Usuarios (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Senha VARCHAR(255) NOT NULL,
    Perfil VARCHAR(20),
    Ativo BOOLEAN DEFAULT TRUE,
    UltimoAcesso DATETIME,
    FuncionarioId INT,
    EmpresaId INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (FuncionarioId) REFERENCES Funcionarios(Id),
    FOREIGN KEY (EmpresaId) REFERENCES Empresas(Id)
);
```

### 3. Funcionários
```sql
CREATE TABLE Funcionarios (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nome VARCHAR(100) NOT NULL,
    CPF VARCHAR(14),
    RG VARCHAR(20),
    Telefone VARCHAR(20),
    Email VARCHAR(100),
    Cargo VARCHAR(50),
    Salario DECIMAL(10,2),
    DataAdmissao DATETIME,
    DataDemissao DATETIME,
    Ativo BOOLEAN DEFAULT TRUE,
    Endereco VARCHAR(255),
    Cidade VARCHAR(100),
    UF VARCHAR(2),
    CEP VARCHAR(10),
    EmpresaId INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (EmpresaId) REFERENCES Empresas(Id)
);
```

### 4. Categorias
```sql
CREATE TABLE Categorias (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Descricao VARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

### 5. Produtos
```sql
CREATE TABLE Produtos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    EmpresaId INT NOT NULL,
    CategoriaId INT,
    CodigoProduto VARCHAR(50),
    ImagemUrl VARCHAR(255),
    ImagemBase64 LONGTEXT,
    ImagemNome VARCHAR(255),
    ImagemMimeType VARCHAR(100),
    Nome VARCHAR(100) NOT NULL,
    NomeAlternativo VARCHAR(100),
    Descricao TEXT,
    Ingredientes TEXT,
    PrecoVenda DECIMAL(10,2),
    PrecoCusto DECIMAL(10,2),
    QuantidadePadrao INT,
    Peso VARCHAR(50),
    ServePessoas VARCHAR(50),
    CodigoBarras VARCHAR(50),
    Situacao BOOLEAN DEFAULT TRUE,
    UnidadeVenda VARCHAR(10) DEFAULT 'UN',
    NCM VARCHAR(20),
    CEST VARCHAR(20),
    CFOP VARCHAR(20),
    CSOSN_CST VARCHAR(20),
    OrigemProduto TINYINT,
    CstIcms VARCHAR(3) DEFAULT '00',
    BaseCalculoIcms DECIMAL(10,2) DEFAULT 0.00,
    AliquotaIcms DECIMAL(5,2) DEFAULT 0.00,
    ValorIcms DECIMAL(10,2) DEFAULT 0.00,
    CstIpi VARCHAR(3) DEFAULT '50',
    BaseCalculoIpi DECIMAL(10,2) DEFAULT 0.00,
    AliquotaIpi DECIMAL(5,2) DEFAULT 0.00,
    ValorIpi DECIMAL(10,2) DEFAULT 0.00,
    CstPis VARCHAR(3) DEFAULT '01',
    BaseCalculoPis DECIMAL(10,2) DEFAULT 0.00,
    AliquotaPis DECIMAL(5,2) DEFAULT 1.65,
    ValorPis DECIMAL(10,2) DEFAULT 0.00,
    CstCofins VARCHAR(3) DEFAULT '01',
    BaseCalculoCofins DECIMAL(10,2) DEFAULT 0.00,
    AliquotaCofins DECIMAL(5,2) DEFAULT 7.60,
    ValorCofins DECIMAL(10,2) DEFAULT 0.00,
    CodigoEan VARCHAR(14),
    InformacoesAdicionais TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    EstoqueAtual INT DEFAULT 0,
    EstoqueMinimo INT DEFAULT 0,
    EstoqueMaximo INT DEFAULT 0,
    UltimaMovimentacao DATETIME,
    LocalizacaoEstoque VARCHAR(255),
    ControlaEstoque BOOLEAN DEFAULT TRUE,
    FOREIGN KEY (EmpresaId) REFERENCES Empresas(Id),
    FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id)
);
```

### 6. Status de Pedidos
```sql
CREATE TABLE StatusPedidos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Descricao VARCHAR(100) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

### 7. Pedidos
```sql
CREATE TABLE Pedidos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NumeroPedido VARCHAR(50) NOT NULL,
    NumeroComanda VARCHAR(20),
    NomeCliente VARCHAR(100) NOT NULL,
    TelefoneCliente VARCHAR(20) NOT NULL,
    EmailCliente VARCHAR(100),
    EnderecoCliente VARCHAR(255),
    QuantidadeItens INT DEFAULT 0,
    Total DECIMAL(10,2) DEFAULT 0.00,
    Status VARCHAR(50) NOT NULL,
    DataPedido DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataConclusao DATETIME,
    Observacoes VARCHAR(500),
    SituacaoId INT,
    EmpresaId INT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (SituacaoId) REFERENCES StatusPedidos(Id),
    FOREIGN KEY (EmpresaId) REFERENCES Empresas(Id)
);
```

### 8. Itens de Pedido
```sql
CREATE TABLE ItensPedido (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    PedidoId INT NOT NULL,
    ProdutoId INT NOT NULL,
    Quantidade INT NOT NULL,
    PrecoUnitario DECIMAL(10,2) NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    Observacoes VARCHAR(500),
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (PedidoId) REFERENCES Pedidos(Id),
    FOREIGN KEY (ProdutoId) REFERENCES Produtos(Id)
);
```

### 9. Caixas
```sql
CREATE TABLE Caixas (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    EmpresaId INT NOT NULL,
    DataAbertura DATETIME DEFAULT CURRENT_TIMESTAMP,
    DataFechamento DATETIME,
    ValorAbertura DECIMAL(10,2) NOT NULL,
    ValorFechamento DECIMAL(10,2) DEFAULT 0.00,
    TrocoFinal DECIMAL(10,2) DEFAULT 0.00,
    Status VARCHAR(20) DEFAULT 'aberto',
    Observacao TEXT,
    TotalDinheiro DECIMAL(10,2) DEFAULT 0.00,
    TotalCartaoCredito DECIMAL(10,2) DEFAULT 0.00,
    TotalCartaoDebito DECIMAL(10,2) DEFAULT 0.00,
    TotalPix DECIMAL(10,2) DEFAULT 0.00,
    TotalOutros DECIMAL(10,2) DEFAULT 0.00,
    TotalVendas DECIMAL(10,2) DEFAULT 0.00,
    FOREIGN KEY (EmpresaId) REFERENCES Empresas(Id)
);
```

### 10. Pagamentos de Caixa
```sql
CREATE TABLE PagamentosCaixa (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CaixaId INT NOT NULL,
    FormaPagamento VARCHAR(50) NOT NULL,
    Valor DECIMAL(10,2) NOT NULL,
    DataHora DATETIME DEFAULT CURRENT_TIMESTAMP,
    Observacao TEXT,
    FOREIGN KEY (CaixaId) REFERENCES Caixas(Id)
);
```

### 11. Motoboys
```sql
CREATE TABLE Motoboys (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    EmpresaId INT NOT NULL,
    Nome VARCHAR(100) NOT NULL,
    Documento VARCHAR(20) NOT NULL,
    Telefone VARCHAR(20) NOT NULL,
    Veiculo VARCHAR(50) NOT NULL,
    Placa VARCHAR(10) NOT NULL,
    Status VARCHAR(20) DEFAULT 'ativo',
    Observacao TEXT,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (EmpresaId) REFERENCES Empresas(Id)
);
```

## Relacionamentos

### Relacionamentos Principais

1. **Empresa → Usuários** (1:N)
   - Uma empresa pode ter múltiplos usuários
   - Usuários pertencem a uma empresa

2. **Empresa → Funcionários** (1:N)
   - Uma empresa pode ter múltiplos funcionários
   - Funcionários pertencem a uma empresa

3. **Empresa → Produtos** (1:N)
   - Uma empresa pode ter múltiplos produtos
   - Produtos pertencem a uma empresa

4. **Categoria → Produtos** (1:N)
   - Uma categoria pode ter múltiplos produtos
   - Produtos podem pertencer a uma categoria

5. **Empresa → Pedidos** (1:N)
   - Uma empresa pode ter múltiplos pedidos
   - Pedidos pertencem a uma empresa

6. **StatusPedido → Pedidos** (1:N)
   - Um status pode ter múltiplos pedidos
   - Pedidos têm um status

7. **Pedido → ItensPedido** (1:N)
   - Um pedido pode ter múltiplos itens
   - Itens pertencem a um pedido

8. **Produto → ItensPedido** (1:N)
   - Um produto pode estar em múltiplos itens
   - Itens referenciam um produto

9. **Empresa → Caixas** (1:N)
   - Uma empresa pode ter múltiplos caixas
   - Caixas pertencem a uma empresa

10. **Caixa → PagamentosCaixa** (1:N)
    - Um caixa pode ter múltiplos pagamentos
    - Pagamentos pertencem a um caixa

11. **Empresa → Motoboys** (1:N)
    - Uma empresa pode ter múltiplos motoboys
    - Motoboys pertencem a uma empresa

## Índices Recomendados

```sql
-- Índices para melhor performance
CREATE INDEX idx_usuarios_email ON Usuarios(Email);
CREATE INDEX idx_usuarios_empresa ON Usuarios(EmpresaId);
CREATE INDEX idx_produtos_empresa ON Produtos(EmpresaId);
CREATE INDEX idx_produtos_categoria ON Produtos(CategoriaId);
CREATE INDEX idx_produtos_codigo ON Produtos(CodigoProduto);
CREATE INDEX idx_produtos_barras ON Produtos(CodigoBarras);
CREATE INDEX idx_produtos_ean ON Produtos(CodigoEan);
CREATE INDEX idx_pedidos_empresa ON Pedidos(EmpresaId);
CREATE INDEX idx_pedidos_status ON Pedidos(SituacaoId);
CREATE INDEX idx_pedidos_data ON Pedidos(DataPedido);
CREATE INDEX idx_itens_pedido ON ItensPedido(PedidoId);
CREATE INDEX idx_itens_produto ON ItensPedido(ProdutoId);
CREATE INDEX idx_caixas_empresa ON Caixas(EmpresaId);
CREATE INDEX idx_caixas_status ON Caixas(Status);
CREATE INDEX idx_pagamentos_caixa ON PagamentosCaixa(CaixaId);
CREATE INDEX idx_motoboys_empresa ON Motoboys(EmpresaId);
```

## Dados Iniciais

### Status de Pedidos Padrão
```sql
INSERT INTO StatusPedidos (Descricao) VALUES 
('Pendente'),
('Em Preparo'),
('Pronto'),
('Em Entrega'),
('Entregue'),
('Cancelado');
```

### Categorias Padrão
```sql
INSERT INTO Categorias (Descricao) VALUES 
('Lanches'),
('Bebidas'),
('Sobremesas'),
('Acompanhamentos'),
('Promoções');
```

## Configurações do Entity Framework

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=pdv_db;User=root;Password=password;"
  }
}
```

### Configurações de Migração
```bash
# Criar migração
dotnet ef migrations add InitialCreate

# Aplicar migração
dotnet ef database update

# Remover migração
dotnet ef migrations remove
```

## Backup e Restauração

### Backup
```bash
mysqldump -u root -p pdv_db > backup_pdv_$(date +%Y%m%d_%H%M%S).sql
```

### Restauração
```bash
mysql -u root -p pdv_db < backup_pdv_20240115_143000.sql
```

## Monitoramento

### Queries Úteis para Monitoramento

```sql
-- Total de vendas por empresa
SELECT 
    e.RazaoSocial,
    COUNT(p.Id) as TotalPedidos,
    SUM(p.Total) as TotalVendas
FROM Empresas e
LEFT JOIN Pedidos p ON e.Id = p.EmpresaId
WHERE p.DataPedido >= DATE_SUB(NOW(), INTERVAL 30 DAY)
GROUP BY e.Id, e.RazaoSocial;

-- Produtos mais vendidos
SELECT 
    pr.Nome,
    COUNT(ii.Id) as QuantidadeVendida,
    SUM(ii.Subtotal) as TotalVendido
FROM Produtos pr
JOIN ItensPedido ii ON pr.Id = ii.ProdutoId
JOIN Pedidos p ON ii.PedidoId = p.Id
WHERE p.DataPedido >= DATE_SUB(NOW(), INTERVAL 30 DAY)
GROUP BY pr.Id, pr.Nome
ORDER BY QuantidadeVendida DESC
LIMIT 10;

-- Status dos caixas
SELECT 
    e.RazaoSocial,
    c.Status,
    c.ValorAbertura,
    c.TotalVendas,
    c.DataAbertura
FROM Caixas c
JOIN Empresas e ON c.EmpresaId = e.Id
WHERE c.Status = 'aberto';
```

## Considerações de Segurança

1. **Senhas**: Devem ser hasheadas (bcrypt, PBKDF2)
2. **Tokens**: JWT com expiração adequada
3. **Logs**: Registrar todas as operações críticas
4. **Backup**: Automatizar backups diários
5. **Acesso**: Limitar acesso por IP em produção
6. **SSL**: Usar HTTPS em produção
7. **Validação**: Validar todos os inputs
8. **Sanitização**: Prevenir SQL Injection 