-- Script para inserir um usuário de teste
-- Senha: 123456 (hash SHA256)

INSERT INTO Usuarios (Nome, Email, Senha, Perfil, Ativo, CreatedAt, UpdatedAt) VALUES 
('Administrador', 'admin@webpdv.com', 'jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=', 'Admin', 1, NOW(), NOW());

-- Para testar o login:
-- Email: admin@webpdv.com
-- Senha: 123456 