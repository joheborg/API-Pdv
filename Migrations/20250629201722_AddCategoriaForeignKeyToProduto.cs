using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Pdv.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoriaForeignKeyToProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_categoria",
                table: "produtos");

            migrationBuilder.DropColumn(
                name: "categoria",
                table: "produtos");

            migrationBuilder.AddColumn<int>(
                name: "categoria_id",
                table: "produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "idx_categoria_id",
                table: "produtos",
                column: "categoria_id");

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_Categorias_categoria_id",
                table: "produtos",
                column: "categoria_id",
                principalTable: "Categorias",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_produtos_empresas_empresa_id",
                table: "produtos",
                column: "empresa_id",
                principalTable: "empresas",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_produtos_Categorias_categoria_id",
                table: "produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_produtos_empresas_empresa_id",
                table: "produtos");

            migrationBuilder.DropIndex(
                name: "idx_categoria_id",
                table: "produtos");

            migrationBuilder.DropColumn(
                name: "categoria_id",
                table: "produtos");

            migrationBuilder.AddColumn<string>(
                name: "categoria",
                table: "produtos",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "idx_categoria",
                table: "produtos",
                column: "categoria");
        }
    }
}
