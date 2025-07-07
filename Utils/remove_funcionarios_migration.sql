-- Script para remover a tabela Funcionarios e suas referências
-- Execute este script no banco de dados

-- 1. Remover a chave estrangeira da tabela Usuarios
ALTER TABLE Usuarios DROP FOREIGN KEY FK_Usuarios_Funcionarios_FuncionarioId;

-- 2. Remover a coluna FuncionarioId da tabela Usuarios
ALTER TABLE Usuarios DROP COLUMN FuncionarioId;

-- 3. Remover a tabela Funcionarios
DROP TABLE IF EXISTS Funcionarios;

-- 4. Verificar se a tabela foi removida
SHOW TABLES LIKE 'Funcionarios';

-- 5. Verificar a estrutura da tabela Usuarios
DESCRIBE Usuarios; 