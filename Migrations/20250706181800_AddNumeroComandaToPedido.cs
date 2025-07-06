﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API_Pdv.Migrations
{
    /// <inheritdoc />
    public partial class AddNumeroComandaToPedido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NumeroComanda",
                table: "Pedidos",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumeroComanda",
                table: "Pedidos");
        }
    }
}
