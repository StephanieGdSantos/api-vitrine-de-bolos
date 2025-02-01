using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaEnderecoAEncomendaEEditaTabelaBolos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BoloIds",
                table: "Encomendas");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Encomendas",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PapelDeArroz",
                table: "Bolos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Presente",
                table: "Bolos",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Topper",
                table: "Bolos",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_EnderecoId",
                table: "Encomendas",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encomendas_Enderecos_EnderecoId",
                table: "Encomendas",
                column: "EnderecoId",
                principalTable: "Enderecos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encomendas_Enderecos_EnderecoId",
                table: "Encomendas");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropIndex(
                name: "IX_Encomendas_EnderecoId",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "PapelDeArroz",
                table: "Bolos");

            migrationBuilder.DropColumn(
                name: "Presente",
                table: "Bolos");

            migrationBuilder.DropColumn(
                name: "Topper",
                table: "Bolos");

            migrationBuilder.AddColumn<string>(
                name: "BoloIds",
                table: "Encomendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
