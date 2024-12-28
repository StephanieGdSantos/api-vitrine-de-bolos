using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class CriaConexaoEntreBolosEEncomendas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomendas_Bolos_BoloId",
                table: "Encomendas");

            migrationBuilder.DropIndex(
                name: "IX_Encomendas_BoloId",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "BoloId",
                table: "Encomendas");

            migrationBuilder.CreateTable(
                name: "BoloEncomenda",
                columns: table => new
                {
                    BoloId = table.Column<int>(type: "int", nullable: false),
                    EncomendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoloEncomenda", x => new { x.BoloId, x.EncomendaId });
                    table.ForeignKey(
                        name: "FK_BoloEncomenda_Bolos_BoloId",
                        column: x => x.BoloId,
                        principalTable: "Bolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoloEncomenda_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoloEncomenda_EncomendaId",
                table: "BoloEncomenda",
                column: "EncomendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoloEncomenda");

            migrationBuilder.AddColumn<int>(
                name: "BoloId",
                table: "Encomendas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_BoloId",
                table: "Encomendas",
                column: "BoloId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomendas_Bolos_BoloId",
                table: "Encomendas",
                column: "BoloId",
                principalTable: "Bolos",
                principalColumn: "Id");
        }
    }
}
