using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaTamanhoEFormatoAoBolo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Formato",
                table: "Bolos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Observacao",
                table: "Bolos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Formato",
                table: "Bolos");

            migrationBuilder.DropColumn(
                name: "Observacao",
                table: "Bolos");
        }
    }
}
