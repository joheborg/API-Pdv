# Estrutura do Banco de Dados - API PDV

## Visão Geral

O arquivo `Utils/dbstruct.sql` contém todas as queries necessárias para criar o banco de dados e todas as tabelas do sistema PDV.

## Banco de Dados

- **Nome**: `api_pdv`
- **Charset**: `utf8mb4`
- **Collation**: `utf8mb4_unicode_ci`

## Tabelas Implementadas

### 1. empresas
Tabela principal que armazena os dados das empresas do sistema.

**Campos principais:**
- `id` - Chave primária
- `cnpj` - CNPJ da empresa (único)
- `razao_social` - Razão social
- `nome_fantasia` - Nome fantasia
- `inscricao_estadual` - Inscrição estadual
- `crt` - Código de regime tributário

**Campos de imagem:**
- `logo_base64` - Logo em formato LONGBLOB
- `logo_nome` - Nome do arquivo
- `logo_mime_type` - Tipo MIME

**Campos de endereço:**
- `endereco_logradouro` - Logradouro
- `endereco_numero` - Número
- `endereco_complemento` - Complemento
- `endereco_bairro` - Bairro
- `endereco_codigo_municipio` - Código do município
- `endereco_nome_municipio` - Nome do município
- `endereco_uf` - UF
- `endereco_cep` - CEP
- `endereco_codigo_pais` - Código do país (padrão: 1058)
- `endereco_nome_pais` - Nome do país (padrão: Brasil)

### 2. produtos
Tabela que armazena os produtos das empresas.

**Campos principais:**
- `id` - Chave primária
- `empresa_id` - Chave estrangeira para empresas
- `nome` - Nome do produto
- `codigo_produto` - Código interno
- `codigo_barras` - Código de barras
- `categoria` - Categoria do produto

**Campos comerciais:**
- `preco_venda` - Preço de venda
- `preco_custo` - Preço de custo
- `quantidade_padrao` - Quantidade padrão
- `unidade_venda` - Unidade de venda (padrão: UN)
- `situacao` - Status do produto (ativo/inativo)

**Campos de imagem:**
- `imagem_base64` - Imagem em formato LONGBLOB
- `imagem_nome` - Nome do arquivo
- `imagem_mime_type` - Tipo MIME

**Campos fiscais:**
- `ncm` - Classificação NCM
- `cest` - Código CEST
- `cfop` - Código CFOP
- `origem_produto` - Origem do produto

**Campos ICMS:**
- `cst_icms` - CST ICMS (padrão: 00)
- `base_calculo_icms` - Base de cálculo ICMS
- `aliquota_icms` - Alíquota ICMS
- `valor_icms` - Valor ICMS

**Campos IPI:**
- `cst_ipi` - CST IPI (padrão: 50)
- `base_calculo_ipi` - Base de cálculo IPI
- `aliquota_ipi` - Alíquota IPI
- `valor_ipi` - Valor IPI

**Campos PIS:**
- `cst_pis` - CST PIS (padrão: 01)
- `base_calculo_pis` - Base de cálculo PIS
- `aliquota_pis` - Alíquota PIS (padrão: 1.65)
- `valor_pis` - Valor PIS

**Campos COFINS:**
- `cst_cofins` - CST COFINS (padrão: 01)
- `base_calculo_cofins` - Base de cálculo COFINS
- `aliquota_cofins` - Alíquota COFINS (padrão: 7.60)
- `valor_cofins` - Valor COFINS

**Controle de estoque:**
- `estoque_atual` - Estoque atual
- `estoque_minimo` - Estoque mínimo
- `estoque_maximo` - Estoque máximo
- `ultima_movimentacao` - Data da última movimentação
- `localizacao_estoque` - Localização no estoque
- `controla_estoque` - Se controla estoque

### 3. motoboys
Tabela que armazena os dados dos motoboys.

**Campos:**
- `id` - Chave primária
- `empresa_id` - Chave estrangeira para empresas
- `nome` - Nome do motoboy
- `documento` - Documento (CPF/CNH)
- `telefone` - Telefone
- `veiculo` - Tipo de veículo
- `placa` - Placa do veículo
- `status` - Status (ativo/inativo)
- `observacao` - Observações

### 4. caixas
Tabela que controla os caixas abertos/fechados.

**Campos principais:**
- `id` - Chave primária
- `empresa_id` - Chave estrangeira para empresas
- `data_abertura` - Data de abertura
- `data_fechamento` - Data de fechamento
- `valor_abertura` - Valor de abertura
- `valor_fechamento` - Valor de fechamento
- `troco_final` - Troco final
- `status` - Status (aberto/fechado)

**Totais por forma de pagamento:**
- `total_dinheiro` - Total em dinheiro
- `total_cartao_credito` - Total cartão crédito
- `total_cartao_debito` - Total cartão débito
- `total_pix` - Total PIX
- `total_outros` - Total outros
- `total_vendas` - Total geral

### 5. pedidos
Tabela que armazena os pedidos realizados.

**Campos:**
- `id` - Chave primária
- `empresa_id` - Chave estrangeira para empresas
- `caixa_id` - Chave estrangeira para caixas (opcional)
- `data_hora` - Data e hora do pedido
- `valor_total` - Valor total do pedido
- `observacao` - Observações
- `status` - Status do pedido

### 6. itens_pedido
Tabela que armazena os itens de cada pedido.

**Campos:**
- `id` - Chave primária
- `pedido_id` - Chave estrangeira para pedidos
- `produto_id` - Chave estrangeira para produtos
- `quantidade` - Quantidade
- `preco_unitario` - Preço unitário
- `total` - Total do item

### 7. pagamentos_caixa
Tabela que registra os pagamentos realizados no caixa.

**Campos:**
- `id` - Chave primária
- `caixa_id` - Chave estrangeira para caixas
- `forma_pagamento` - Forma de pagamento
- `valor` - Valor do pagamento
- `data_hora` - Data e hora
- `observacao` - Observações

## Relacionamentos

1. **Empresa → Produtos** (1:N)
   - Uma empresa pode ter vários produtos
   - `empresas.id` → `produtos.empresa_id`

2. **Empresa → Motoboys** (1:N)
   - Uma empresa pode ter vários motoboys
   - `empresas.id` → `motoboys.empresa_id`

3. **Empresa → Caixas** (1:N)
   - Uma empresa pode ter vários caixas
   - `empresas.id` → `caixas.empresa_id`

4. **Caixa → Pedidos** (1:N)
   - Um caixa pode ter vários pedidos
   - `caixas.id` → `pedidos.caixa_id`

5. **Caixa → PagamentosCaixa** (1:N)
   - Um caixa pode ter vários pagamentos
   - `caixas.id` → `pagamentos_caixa.caixa_id`

6. **Pedido → ItensPedido** (1:N)
   - Um pedido pode ter vários itens
   - `pedidos.id` → `itens_pedido.pedido_id`

7. **Produto → ItensPedido** (1:N)
   - Um produto pode estar em vários itens de pedido
   - `produtos.id` → `itens_pedido.produto_id`

## Convenções Utilizadas

### Nomenclatura
- **Tabelas**: snake_case (ex: `empresas`, `produtos`)
- **Colunas**: snake_case (ex: `razao_social`, `preco_venda`)
- **Chaves primárias**: `id` (INT AUTO_INCREMENT)
- **Chaves estrangeiras**: `nome_tabela_id` (ex: `empresa_id`)

### Tipos de Dados
- **Valores monetários**: `DECIMAL(10,2)`
- **Percentuais**: `DECIMAL(5,2)`
- **Imagens**: `LONGBLOB` (conversão automática de base64)
- **Textos longos**: `TEXT`
- **Campos de texto**: `VARCHAR` com limites apropriados
- **Datas**: `DATETIME`

### Índices
- **Chaves primárias**: Automáticos
- **Chaves estrangeiras**: Automáticos
- **Campos de busca**: CNPJ, código de barras, etc.
- **Campos de filtro**: Status, categoria, etc.

### Constraints
- **UNIQUE**: CNPJ da empresa
- **NOT NULL**: Campos obrigatórios
- **DEFAULT**: Valores padrão para campos opcionais
- **FOREIGN KEY**: Relacionamentos com CASCADE/RESTRICT conforme necessário

## Como Usar

1. **Criar o banco de dados:**
   ```sql
   -- Execute o arquivo dbstruct.sql completo
   mysql -u usuario -p < Utils/dbstruct.sql
   ```

2. **Verificar a estrutura:**
   ```sql
   USE api_pdv;
   SHOW TABLES;
   DESCRIBE empresas;
   ```

3. **Inserir dados iniciais:**
   - O arquivo já inclui uma empresa padrão
   - Você pode modificar os dados conforme necessário

## Observações Importantes

1. **Imagens**: São armazenadas como LONGBLOB no banco, mas o EF Core faz a conversão automática de base64
2. **Endereços**: São armazenados como colunas separadas na tabela empresas
3. **Controle de estoque**: Implementado na tabela produtos
4. **Controle fiscal**: Campos completos para ICMS, IPI, PIS e COFINS
5. **Controle de caixa**: Sistema completo de abertura/fechamento com totais por forma de pagamento

## Próximos Passos

Se você precisar adicionar novas entidades (como Usuario ou Funcionario), siga o mesmo padrão:
1. Crie a entidade no Domain/Entities
2. Crie a configuração no Infraestructure/Data/Configurations
3. Adicione a tabela no dbstruct.sql
4. Atualize o ApplicationDbContext
5. Crie os repositórios e controllers 