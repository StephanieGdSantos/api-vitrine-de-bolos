using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaChaveEstrangeira : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
