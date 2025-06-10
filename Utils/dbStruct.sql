CREATE TABLE produtos (
                          id INT AUTO_INCREMENT PRIMARY KEY,

    -- Relacionamento com a empresa
                          empresa_id INT NOT NULL, -- código da empresa/lanchonete

    -- Dados Gerais
                          codigo_produto VARCHAR(50),
                          imagem_url VARCHAR(255),
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
                          unidade_venda VARCHAR(10),

    -- Dados Fiscais
                          ncm VARCHAR(20),
                          cest VARCHAR(20),
                          cfop VARCHAR(20),
                          csosn_cst VARCHAR(20),
                          origem_produto TINYINT,

    -- Datas de controle
                          created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                          updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,

    -- Índices
                          UNIQUE (empresa_id, codigo_produto)
);
