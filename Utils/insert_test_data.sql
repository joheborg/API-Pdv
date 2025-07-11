-- Inserir empresa de teste se não existir
INSERT IGNORE INTO empresas (id, nome, cnpj, email, telefone, endereco, cidade, uf, cep, created_at, updated_at)
VALUES (1, 'Empresa Teste', '12345678901234', 'teste@empresa.com', '11999999999', 'Rua Teste, 123', 'São Paulo', 'SP', '01234-567', NOW(), NOW());

-- Inserir categoria de teste se não existir
INSERT IGNORE INTO categorias (id, nome, descricao, empresa_id, created_at, updated_at)
VALUES (1, 'Geral', 'Categoria geral', 1, NOW(), NOW());

-- Inserir produtos de teste
INSERT IGNORE INTO produtos (
    empresa_id, categoria_id, codigo_produto, nome, descricao, 
    preco_venda, preco_custo, quantidade_padrao, peso, serve_pessoas,
    codigo_barras, unidade_venda, situacao, created_at, updated_at
) VALUES 
(1, 1, 'PROD001', 'Produto Teste 1', 'Descrição do produto teste 1', 
 10.50, 8.00, 1, '100g', '1 pessoa', '7891234567890', 'UN', true, NOW(), NOW()),
(1, 1, 'PROD002', 'Produto Teste 2', 'Descrição do produto teste 2', 
 15.75, 12.00, 1, '200g', '2 pessoas', '7891234567891', 'UN', true, NOW(), NOW());

-- Inserir pedidos de teste
INSERT IGNORE INTO Pedidos (
    NumeroPedido, NumeroComanda, NomeCliente, TelefoneCliente, EmailCliente,
    EnderecoCliente, QuantidadeItens, Total, Status, DataPedido, 
    Observacoes, SituacaoId, EmpresaId, CreatedAt, UpdatedAt
) VALUES 
('PED001', 'CMD001', 'Cliente Teste 1', '11999999999', 'cliente1@teste.com',
 'Rua Cliente, 123', 2, 25.25, 'Pendente', NOW(), 
 'Observação do pedido 1', 1, 1, NOW(), NOW()),
('PED002', 'CMD002', 'Cliente Teste 2', '11888888888', 'cliente2@teste.com',
 'Rua Cliente, 456', 1, 15.75, 'Em Preparo', NOW(), 
 'Observação do pedido 2', 1, 1, NOW(), NOW()); 