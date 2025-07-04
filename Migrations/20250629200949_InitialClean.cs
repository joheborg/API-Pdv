using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Pdv.Migrations
{
    /// <inheritdoc />
    public partial class InitialClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "empresas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    cnpj = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    razao_social = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome_fantasia = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    inscricao_estadual = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    crt = table.Column<string>(type: "varchar(5)", maxLength: 5, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logo_base64 = table.Column<byte[]>(type: "LONGBLOB", nullable: true),
                    logo_nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    logo_mime_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_logradouro = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_numero = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_bairro = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_codigo_municipio = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_nome_municipio = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_uf = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_cep = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_codigo_pais = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValue: "1058")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    endereco_nome_pais = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, defaultValue: "Brasil")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_empresas", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "motoboy",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    documento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    telefone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    veiculo = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    placa = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observacao = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_motoboy", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "produtos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    codigo_produto = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    imagem_url = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    imagem_base64 = table.Column<byte[]>(type: "LONGBLOB", nullable: true),
                    imagem_nome = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    imagem_mime_type = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nome_alternativo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    descricao = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    categoria = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ingredientes = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    preco_venda = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    preco_custo = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    quantidade_padrao = table.Column<int>(type: "int", nullable: true),
                    peso = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    serve_pessoas = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    codigo_barras = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    situacao = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true),
                    unidade_venda = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, defaultValue: "UN")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ncm = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cest = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cfop = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    csosn_cst = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    origem_produto = table.Column<byte>(type: "tinyint unsigned", nullable: true),
                    cst_icms = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true, defaultValue: "00")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    base_calculo_icms = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    aliquota_icms = table.Column<decimal>(type: "decimal(5,2)", nullable: true, defaultValue: 0.00m),
                    valor_icms = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    cst_ipi = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true, defaultValue: "50")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    base_calculo_ipi = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    aliquota_ipi = table.Column<decimal>(type: "decimal(5,2)", nullable: true, defaultValue: 0.00m),
                    valor_ipi = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    cst_pis = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true, defaultValue: "01")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    base_calculo_pis = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    aliquota_pis = table.Column<decimal>(type: "decimal(5,2)", nullable: true, defaultValue: 1.65m),
                    valor_pis = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    cst_cofins = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: true, defaultValue: "01")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    base_calculo_cofins = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    aliquota_cofins = table.Column<decimal>(type: "decimal(5,2)", nullable: true, defaultValue: 7.60m),
                    valor_cofins = table.Column<decimal>(type: "decimal(10,2)", nullable: true, defaultValue: 0.00m),
                    codigo_ean = table.Column<string>(type: "varchar(14)", maxLength: 14, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    informacoes_adicionais = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    updated_at = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    estoque_atual = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    estoque_minimo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    estoque_maximo = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    ultima_movimentacao = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    localizacao_estoque = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    controla_estoque = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_produtos", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StatusPedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusPedidos", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "caixas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    data_abertura = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    data_fechamento = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    valor_abertura = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    valor_fechamento = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    troco_final = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    observacao = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    total_dinheiro = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cartao_credito = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_cartao_debito = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_pix = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_outros = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total_vendas = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caixas", x => x.id);
                    table.ForeignKey(
                        name: "FK_caixas_empresas_empresa_id",
                        column: x => x.empresa_id,
                        principalTable: "empresas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pagamentos_caixa",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    caixa_id = table.Column<int>(type: "int", nullable: false),
                    forma_pagamento = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    data_hora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    observacao = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagamentos_caixa", x => x.id);
                    table.ForeignKey(
                        name: "FK_pagamentos_caixa_caixas_caixa_id",
                        column: x => x.caixa_id,
                        principalTable: "caixas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "pedidos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    empresa_id = table.Column<int>(type: "int", nullable: false),
                    caixa_id = table.Column<int>(type: "int", nullable: true),
                    data_hora = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    valor_total = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    observacao = table.Column<string>(type: "TEXT", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedidos", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedidos_caixas_caixa_id",
                        column: x => x.caixa_id,
                        principalTable: "caixas",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "itens_pedido",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    pedido_id = table.Column<int>(type: "int", nullable: false),
                    produto_id = table.Column<int>(type: "int", nullable: false),
                    quantidade = table.Column<int>(type: "int", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    total = table.Column<decimal>(type: "decimal(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itens_pedido", x => x.id);
                    table.ForeignKey(
                        name: "FK_itens_pedido_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_caixas_empresa_id",
                table: "caixas",
                column: "empresa_id");

            migrationBuilder.CreateIndex(
                name: "idx_cnpj",
                table: "empresas",
                column: "cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "idx_razao_social",
                table: "empresas",
                column: "razao_social");

            migrationBuilder.CreateIndex(
                name: "IX_itens_pedido_pedido_id",
                table: "itens_pedido",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_pagamentos_caixa_caixa_id",
                table: "pagamentos_caixa",
                column: "caixa_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_caixa_id",
                table: "pedidos",
                column: "caixa_id");

            migrationBuilder.CreateIndex(
                name: "idx_categoria",
                table: "produtos",
                column: "categoria");

            migrationBuilder.CreateIndex(
                name: "idx_codigo_barras",
                table: "produtos",
                column: "codigo_barras");

            migrationBuilder.CreateIndex(
                name: "idx_codigo_ean",
                table: "produtos",
                column: "codigo_ean");

            migrationBuilder.CreateIndex(
                name: "idx_ncm",
                table: "produtos",
                column: "ncm");

            migrationBuilder.CreateIndex(
                name: "idx_situacao",
                table: "produtos",
                column: "situacao");

            migrationBuilder.CreateIndex(
                name: "uk_empresa_codigo_produto",
                table: "produtos",
                columns: new[] { "empresa_id", "codigo_produto" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropTable(
                name: "itens_pedido");

            migrationBuilder.DropTable(
                name: "motoboy");

            migrationBuilder.DropTable(
                name: "pagamentos_caixa");

            migrationBuilder.DropTable(
                name: "produtos");

            migrationBuilder.DropTable(
                name: "StatusPedidos");

            migrationBuilder.DropTable(
                name: "pedidos");

            migrationBuilder.DropTable(
                name: "caixas");

            migrationBuilder.DropTable(
                name: "empresas");
        }
    }
}
