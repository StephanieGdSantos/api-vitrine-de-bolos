using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministradorId",
                table: "Bolos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Administrador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrador", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bolos_AdministradorId",
                table: "Bolos",
                column: "AdministradorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bolos_Administrador_AdministradorId",
                table: "Bolos",
                column: "AdministradorId",
                principalTable: "Administrador",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bolos_Administrador_AdministradorId",
                table: "Bolos");

            migrationBuilder.DropTable(
                name: "Administrador");

            migrationBuilder.DropIndex(
                name: "IX_Bolos_AdministradorId",
                table: "Bolos");

            migrationBuilder.DropColumn(
                name: "AdministradorId",
                table: "Bolos");
        }
    }
}
