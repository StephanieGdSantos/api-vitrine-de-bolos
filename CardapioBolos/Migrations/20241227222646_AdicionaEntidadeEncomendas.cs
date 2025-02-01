using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaEntidadeEncomendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EncomendaId",
                table: "Bolos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDoPedido = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NomeCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TelefoneCliente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ValorFinal = table.Column<double>(type: "float", nullable: false),
                    Finalizado = table.Column<bool>(type: "bit", nullable: false),
                    DataDaEntrega = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomendas", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolos_EncomendaId",
                table: "Bolos",
                column: "EncomendaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolos_Encomendas_EncomendaId",
                table: "Bolos",
                column: "EncomendaId",
                principalTable: "Encomendas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolos_Encomendas_EncomendaId",
                table: "Bolos");

            migrationBuilder.DropTable(
                name: "Encomendas");

            migrationBuilder.DropIndex(
                name: "IX_Bolos_EncomendaId",
                table: "Bolos");

            migrationBuilder.DropColumn(
                name: "EncomendaId",
                table: "Bolos");
        }
    }
}
