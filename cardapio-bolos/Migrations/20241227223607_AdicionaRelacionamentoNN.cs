using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaRelacionamentoNN : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolos_Encomendas_EncomendaId",
                table: "Bolos");

            migrationBuilder.DropIndex(
                name: "IX_Bolos_EncomendaId",
                table: "Bolos");

            migrationBuilder.DropColumn(
                name: "EncomendaId",
                table: "Bolos");

            migrationBuilder.CreateTable(
                name: "EncomendaBolo",
                columns: table => new
                {
                    BoloId = table.Column<int>(type: "int", nullable: false),
                    EncomendaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncomendaBolo", x => new { x.BoloId, x.EncomendaId });
                    table.ForeignKey(
                        name: "FK_EncomendaBolo_Bolos_BoloId",
                        column: x => x.BoloId,
                        principalTable: "Bolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncomendaBolo_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EncomendaBolo_EncomendaId",
                table: "EncomendaBolo",
                column: "EncomendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EncomendaBolo");

            migrationBuilder.AddColumn<int>(
                name: "EncomendaId",
                table: "Bolos",
                type: "int",
                nullable: true);

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
    }
}
