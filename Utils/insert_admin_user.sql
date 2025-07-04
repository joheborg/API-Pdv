-- Script para inserir usuário administrador
-- Senha: admin123 (hash SHA256)

-- Primeiro, verificar se a tabela Usuarios existe
-- Se não existir, criar a tabela
CREATE TABLE IF NOT EXISTS `Usuarios` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Nome` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Email` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `Senha` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Perfil` varchar(20) CHARACTER SET utf8mb4 NULL,
    `Ativo` tinyint(1) NOT NULL DEFAULT 1,
    `UltimoAcesso` datetime(6) NULL,
    `FuncionarioId` int NULL,
    `EmpresaId` int NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `UpdatedAt` datetime(6) NOT NULL,
    CONSTRAINT `PK_Usuarios` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

-- Criar índice único para email
CREATE UNIQUE INDEX IF NOT EXISTS `idx_usuario_email` ON `Usuarios` (`Email`);

-- Inserir usuário administrador
-- Senha: admin123 (hash SHA256: 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9)
INSERT INTO `Usuarios` (`Nome`, `Email`, `Senha`, `Perfil`, `Ativo`, `CreatedAt`, `UpdatedAt`) 
VALUES ('Administrador', 'admin@webpdv.com', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Admin', 1, NOW(), NOW())
ON DUPLICATE KEY UPDATE 
    `Nome` = VALUES(`Nome`),
    `Senha` = VALUES(`Senha`),
    `Perfil` = VALUES(`Perfil`),
    `Ativo` = VALUES(`Ativo`),
    `UpdatedAt` = NOW();

-- Verificar se foi inserido
SELECT 'Usuário admin criado com sucesso!' as Status;

-- Mostrar o usuário criado
SELECT Id, Nome, Email, Perfil, Ativo, CreatedAt FROM Usuarios WHERE Email = 'admin@webpdv.com'; 