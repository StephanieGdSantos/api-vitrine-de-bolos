using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTabelaIngrediente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ListaIngredientes",
                table: "Bolos");

            migrationBuilder.CreateTable(
                name: "Ingrediente",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingrediente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoloIngrediente",
                columns: table => new
                {
                    BoloId = table.Column<int>(type: "int", nullable: false),
                    IngredienteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoloIngrediente", x => new { x.BoloId, x.IngredienteId });
                    table.ForeignKey(
                        name: "FK_BoloIngrediente_Bolos_BoloId",
                        column: x => x.BoloId,
                        principalTable: "Bolos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoloIngrediente_Ingrediente_IngredienteId",
                        column: x => x.IngredienteId,
                        principalTable: "Ingrediente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoloIngrediente_IngredienteId",
                table: "BoloIngrediente",
                column: "IngredienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoloIngrediente");

            migrationBuilder.DropTable(
                name: "Ingrediente");

            migrationBuilder.AddColumn<string>(
                name: "ListaIngredientes",
                table: "Bolos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
