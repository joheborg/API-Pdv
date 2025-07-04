-- Script simples para inserir usuário administrador
-- Execute este script no seu banco de dados MySQL

-- Senha: admin123
INSERT INTO `Usuarios` (`Nome`, `Email`, `Senha`, `Perfil`, `Ativo`, `CreatedAt`, `UpdatedAt`) 
VALUES ('Administrador', 'admin@webpdv.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin', 1, NOW(), NOW());

-- Credenciais para login:
-- Email: admin@webpdv.com
-- Senha: admin123 