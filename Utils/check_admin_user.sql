-- Script para verificar se o usuário admin existe
-- Execute este script para verificar se o usuário foi criado corretamente

SELECT 
    Id,
    Nome,
    Email,
    Perfil,
    CASE 
        WHEN Ativo = 1 THEN 'Ativo'
        ELSE 'Inativo'
    END as Status,
    CreatedAt,
    UltimoAcesso
FROM Usuarios 
WHERE Email = 'admin@webpdv.com';

-- Se não retornar nenhum resultado, o usuário não existe
-- Execute o script insert_admin_user.sql primeiro 