-- Query para inserir a empresa Jonathan Borges Ferreira
INSERT INTO Empresas (
    CNPJ,
    RazaoSocial,
    NomeFantasia,
    InscricaoEstadual,
    CRT,
    Logradouro,
    Numero,
    Bairro,
    NomeMunicipio,
    CodigoMunicipio,
    UF,
    CEP,
    CreatedAt,
    UpdatedAt
) VALUES (
    '53348077000113',
    '53.348.077 Jonathan Borges Ferreira',
    'Jonathan Borges Ferreira',
    '3256118181',
    1,
    'Rua caibate',
    '55',
    'Dona Augusta',
    'Campo Bom',
    '4303905',
    'RS',
    '93700000',
    NOW(),
    NOW()
);

-- Verificar se foi inserida corretamente
SELECT * FROM Empresas WHERE CNPJ = '53348077000113'; 