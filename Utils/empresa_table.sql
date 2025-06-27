-- Script para criar a tabela Empresa
CREATE TABLE IF NOT EXISTS empresas (
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
    
    -- Endereço (propriedade complexa)
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

-- Comentários sobre a estrutura
-- A tabela empresas armazena informações das empresas do sistema PDV
-- O CNPJ é único para cada empresa
-- O endereço é armazenado como propriedades separadas (owned entity no EF Core)
-- As datas de criação e atualização são gerenciadas automaticamente 