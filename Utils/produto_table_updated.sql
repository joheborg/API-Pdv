-- Script para criar/atualizar a tabela Produtos com todos os campos fiscais
-- Baseado no dbStruct.sql fornecido

CREATE TABLE IF NOT EXISTS produtos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    
    -- Relacionamento com a empresa
    empresa_id INT NOT NULL,
    
    -- Dados Gerais
    codigo_produto VARCHAR(50),
    imagem_url VARCHAR(255),
    imagem_base64 LONGBLOB,
    imagem_nome VARCHAR(255),
    imagem_mime_type VARCHAR(100),
    nome VARCHAR(100) NOT NULL,
    nome_alternativo VARCHAR(100),
    descricao TEXT,
    categoria VARCHAR(100),
    ingredientes TEXT,
    situacao TINYINT DEFAULT 1,
    
    -- Dados Comerciais
    preco_venda DECIMAL(10,2),
    preco_custo DECIMAL(10,2),
    quantidade_padrao INT,
    peso VARCHAR(50),
    serve_pessoas VARCHAR(50),
    codigo_barras VARCHAR(50),
    unidade_venda VARCHAR(10) DEFAULT 'UN',
    
    -- Dados Fiscais Básicos
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
    
    -- Datas de controle
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    
    -- Índices
    UNIQUE (empresa_id, codigo_produto),
    INDEX idx_ncm (ncm),
    INDEX idx_categoria (categoria),
    INDEX idx_codigo_ean (codigo_ean),
    INDEX idx_codigo_barras (codigo_barras),
    INDEX idx_empresa_id (empresa_id),
    INDEX idx_situacao (situacao),
    
    -- Foreign Key
    FOREIGN KEY (empresa_id) REFERENCES empresas(id) ON DELETE CASCADE
);

-- Comentários sobre a estrutura
-- A tabela produtos armazena informações completas dos produtos do sistema PDV
-- Inclui todos os campos fiscais necessários para emissão de NFe
-- Os campos fiscais têm valores padrão conforme legislação brasileira
-- Relacionamento com empresa através de foreign key
-- Índices para otimizar consultas por NCM, categoria, códigos de barras e EAN 