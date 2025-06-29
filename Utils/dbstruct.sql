-- =====================================================
-- ESTRUTURA COMPLETA DO BANCO DE DADOS - API PDV
-- =====================================================

-- Criar banco de dados
CREATE DATABASE IF NOT EXISTS api_pdv
CHARACTER SET utf8mb4
COLLATE utf8mb4_unicode_ci;

USE api_pdv;

-- =====================================================
-- TABELA: empresas
-- =====================================================
CREATE TABLE empresas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    cnpj VARCHAR(14) NOT NULL UNIQUE,
    razao_social VARCHAR(200) NOT NULL,
    nome_fantasia VARCHAR(200),
    inscricao_estadual VARCHAR(50),
    crt VARCHAR(5),
    
    -- Logo da empresa
    logo_base64 LONGBLOB,
    logo_nome VARCHAR(255),
    logo_mime_type VARCHAR(100),
    
    -- Endereço
    endereco_logradouro VARCHAR(255),
    endereco_numero VARCHAR(20),
    endereco_complemento VARCHAR(100),
    endereco_bairro VARCHAR(100),
    endereco_codigo_municipio VARCHAR(10),
    endereco_nome_municipio VARCHAR(100),
    endereco_uf VARCHAR(2),
    endereco_cep VARCHAR(10),
    endereco_codigo_pais VARCHAR(10) DEFAULT '1058',
    endereco_nome_pais VARCHAR(50) DEFAULT 'Brasil',
    
    -- Datas
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    -- Índices
    INDEX idx_cnpj (cnpj),
    INDEX idx_razao_social (razao_social)
);

-- =====================================================
-- TABELA: produtos
-- =====================================================
CREATE TABLE produtos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    empresa_id INT NOT NULL,
    
    -- Dados gerais
    codigo_produto VARCHAR(50),
    imagem_url VARCHAR(255),
    
    -- Campos de imagem
    imagem_base64 LONGBLOB,
    imagem_nome VARCHAR(255),
    imagem_mime_type VARCHAR(100),
    
    nome VARCHAR(100) NOT NULL,
    nome_alternativo VARCHAR(100),
    descricao TEXT,
    categoria VARCHAR(100),
    ingredientes TEXT,
    
    -- Dados Comerciais
    preco_venda DECIMAL(10,2),
    preco_custo DECIMAL(10,2),
    quantidade_padrao INT,
    peso VARCHAR(50),
    serve_pessoas VARCHAR(50),
    codigo_barras VARCHAR(50),
    situacao BOOLEAN NOT NULL DEFAULT TRUE,
    unidade_venda VARCHAR(10) DEFAULT 'UN',
    
    -- Dados fiscais básicos
    ncm VARCHAR(20),
    cest VARCHAR(20),
    cfop VARCHAR(20),
    csosn_cst VARCHAR(20),
    origem_produto TINYINT,
    
    -- Dados Fiscais ICMS
    cst_icms VARCHAR(3) DEFAULT '00',
    base_calculo_icms DECIMAL(10,2) DEFAULT 0.00,
    aliquota_icms DECIMAL(5,2) DEFAULT 0.00,
    valor_icms DECIMAL(10,2) DEFAULT 0.00,
    
    -- Dados Fiscais IPI
    cst_ipi VARCHAR(3) DEFAULT '50',
    base_calculo_ipi DECIMAL(10,2) DEFAULT 0.00,
    aliquota_ipi DECIMAL(5,2) DEFAULT 0.00,
    valor_ipi DECIMAL(10,2) DEFAULT 0.00,
    
    -- Dados Fiscais PIS
    cst_pis VARCHAR(3) DEFAULT '01',
    base_calculo_pis DECIMAL(10,2) DEFAULT 0.00,
    aliquota_pis DECIMAL(5,2) DEFAULT 1.65,
    valor_pis DECIMAL(10,2) DEFAULT 0.00,
    
    -- Dados Fiscais COFINS
    cst_cofins VARCHAR(3) DEFAULT '01',
    base_calculo_cofins DECIMAL(10,2) DEFAULT 0.00,
    aliquota_cofins DECIMAL(5,2) DEFAULT 7.60,
    valor_cofins DECIMAL(10,2) DEFAULT 0.00,
    
    -- Códigos adicionais
    codigo_ean VARCHAR(14),
    informacoes_adicionais TEXT,
    
    -- Controle de Estoque
    estoque_atual INT NOT NULL DEFAULT 0,
    estoque_minimo INT NOT NULL DEFAULT 0,
    estoque_maximo INT NOT NULL DEFAULT 0,
    ultima_movimentacao DATETIME,
    localizacao_estoque VARCHAR(100),
    controla_estoque BOOLEAN NOT NULL DEFAULT TRUE,
    
    -- Datas
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    -- Foreign Keys
    FOREIGN KEY (empresa_id) REFERENCES empresas(id) ON DELETE CASCADE,
    
    -- Índices
    INDEX idx_empresa_id (empresa_id),
    INDEX idx_codigo_produto (codigo_produto),
    INDEX idx_codigo_barras (codigo_barras),
    INDEX idx_categoria (categoria),
    INDEX idx_situacao (situacao)
);

-- =====================================================
-- TABELA: motoboys
-- =====================================================
CREATE TABLE motoboys (
    id INT AUTO_INCREMENT PRIMARY KEY,
    empresa_id INT NOT NULL,
    nome VARCHAR(100) NOT NULL,
    documento VARCHAR(20) NOT NULL,
    telefone VARCHAR(20) NOT NULL,
    veiculo VARCHAR(50) NOT NULL,
    placa VARCHAR(10) NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'ativo',
    observacao TEXT,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    -- Foreign Keys
    FOREIGN KEY (empresa_id) REFERENCES empresas(id) ON DELETE CASCADE,
    
    -- Índices
    INDEX idx_empresa_id (empresa_id),
    INDEX idx_documento (documento),
    INDEX idx_status (status)
);

-- =====================================================
-- TABELA: caixas
-- =====================================================
CREATE TABLE caixas (
    id INT AUTO_INCREMENT PRIMARY KEY,
    empresa_id INT NOT NULL,
    data_abertura DATETIME NOT NULL,
    data_fechamento DATETIME,
    valor_abertura DECIMAL(10,2) NOT NULL,
    valor_fechamento DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    troco_final DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    status VARCHAR(20) NOT NULL DEFAULT 'aberto',
    observacao TEXT,
    
    -- Totais por forma de pagamento
    total_dinheiro DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_cartao_credito DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_cartao_debito DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_pix DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_outros DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    total_vendas DECIMAL(10,2) NOT NULL DEFAULT 0.00,
    
    -- Foreign Keys
    FOREIGN KEY (empresa_id) REFERENCES empresas(id) ON DELETE CASCADE,
    
    -- Índices
    INDEX idx_empresa_id (empresa_id),
    INDEX idx_data_abertura (data_abertura),
    INDEX idx_status (status)
);

-- =====================================================
-- TABELA: pedidos
-- =====================================================
CREATE TABLE pedidos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    empresa_id INT NOT NULL,
    caixa_id INT,
    data_hora DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    valor_total DECIMAL(10,2) NOT NULL,
    observacao TEXT,
    status VARCHAR(20) NOT NULL DEFAULT 'aberto',
    
    -- Foreign Keys
    FOREIGN KEY (empresa_id) REFERENCES empresas(id) ON DELETE CASCADE,
    FOREIGN KEY (caixa_id) REFERENCES caixas(id) ON DELETE SET NULL,
    
    -- Índices
    INDEX idx_empresa_id (empresa_id),
    INDEX idx_caixa_id (caixa_id),
    INDEX idx_data_hora (data_hora),
    INDEX idx_status (status)
);

-- =====================================================
-- TABELA: itens_pedido
-- =====================================================
CREATE TABLE itens_pedido (
    id INT AUTO_INCREMENT PRIMARY KEY,
    pedido_id INT NOT NULL,
    produto_id INT NOT NULL,
    quantidade INT NOT NULL,
    preco_unitario DECIMAL(10,2) NOT NULL,
    total DECIMAL(10,2) NOT NULL,
    
    -- Foreign Keys
    FOREIGN KEY (pedido_id) REFERENCES pedidos(id) ON DELETE CASCADE,
    FOREIGN KEY (produto_id) REFERENCES produtos(id) ON DELETE RESTRICT,
    
    -- Índices
    INDEX idx_pedido_id (pedido_id),
    INDEX idx_produto_id (produto_id)
);

-- =====================================================
-- TABELA: pagamentos_caixa
-- =====================================================
CREATE TABLE pagamentos_caixa (
    id INT AUTO_INCREMENT PRIMARY KEY,
    caixa_id INT NOT NULL,
    forma_pagamento VARCHAR(50) NOT NULL,
    valor DECIMAL(10,2) NOT NULL,
    data_hora DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    observacao TEXT,
    
    -- Foreign Keys
    FOREIGN KEY (caixa_id) REFERENCES caixas(id) ON DELETE CASCADE,
    
    -- Índices
    INDEX idx_caixa_id (caixa_id),
    INDEX idx_data_hora (data_hora),
    INDEX idx_forma_pagamento (forma_pagamento)
);

-- =====================================================
-- INSERIR DADOS INICIAIS (OPCIONAL)
-- =====================================================

-- Inserir empresa padrão
INSERT INTO empresas (cnpj, razao_social, nome_fantasia, inscricao_estadual, crt, created_at, updated_at) 
VALUES ('12345678000199', 'Empresa Padrão LTDA', 'Empresa Padrão', '123456789', '1', NOW(), NOW());

-- =====================================================
-- COMENTÁRIOS SOBRE A ESTRUTURA
-- =====================================================

/*
ESTRUTURA DO BANCO DE DADOS - API PDV

1. CONVENÇÕES UTILIZADAS:
   - Todas as tabelas e colunas em snake_case
   - Chaves primárias: id (INT AUTO_INCREMENT)
   - Chaves estrangeiras: nome_tabela_id
   - Datas: created_at, updated_at
   - Imagens: armazenadas como LONGBLOB (bytes) no banco

2. RELACIONAMENTOS:
   - Empresa -> Produtos (1:N)
   - Empresa -> Motoboys (1:N)
   - Empresa -> Caixas (1:N)
   - Caixa -> Pedidos (1:N)
   - Caixa -> PagamentosCaixa (1:N)
   - Pedido -> ItensPedido (1:N)
   - Produto -> ItensPedido (1:N)

3. CONTROLES IMPLEMENTADOS:
   - Controle de estoque em produtos
   - Controle de status em caixas e pedidos
   - Controle de formas de pagamento
   - Controle fiscal completo (ICMS, IPI, PIS, COFINS)
   - Controle de imagens (base64 -> bytes)

4. ÍNDICES CRIADOS:
   - Chaves primárias e estrangeiras
   - Campos de busca frequente (CNPJ, código de barras, etc.)
   - Campos de filtro (status, categoria, etc.)

5. TIPOS DE DADOS:
   - DECIMAL(10,2) para valores monetários
   - DECIMAL(5,2) para percentuais
   - LONGBLOB para imagens
   - TEXT para campos longos
   - VARCHAR com limites apropriados
*/ 