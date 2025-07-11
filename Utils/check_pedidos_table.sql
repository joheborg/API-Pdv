-- Verificar se a tabela Pedidos existe
SHOW TABLES LIKE 'Pedidos';

-- Verificar estrutura da tabela Pedidos
DESCRIBE Pedidos;

-- Verificar se há dados na tabela
SELECT COUNT(*) as total_pedidos FROM Pedidos;

-- Verificar dados de exemplo
SELECT * FROM Pedidos LIMIT 5;

-- Verificar se a tabela produtos existe
SHOW TABLES LIKE 'produtos';

-- Verificar estrutura da tabela produtos
DESCRIBE produtos;

-- Verificar se há dados na tabela produtos
SELECT COUNT(*) as total_produtos FROM produtos;

-- Verificar dados de exemplo
SELECT * FROM produtos LIMIT 5; 