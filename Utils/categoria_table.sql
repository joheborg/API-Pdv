-- Script para criar a tabela Categorias
CREATE TABLE IF NOT EXISTS Categorias (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Descricao VARCHAR(100) NOT NULL,
    CreatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);

-- Inserir algumas categorias de exemplo
INSERT INTO Categorias (Descricao) VALUES 
('Bebidas'),
('Alimentos'),
('Higiene'),
('Limpeza'),
('Eletrônicos'),
('Vestuário'),
('Acessórios'),
('Outros'); 