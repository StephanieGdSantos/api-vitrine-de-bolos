using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class ExcluiTabelaEnderecoEAdicionaComoAtributo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                table: "Encomendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                table: "Encomendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                table: "Encomendas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                table: "Encomendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Numero",
                table: "Encomendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "Cidade",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "Complemento",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                table: "Encomendas");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Encomendas");

            migrationBuilder.AddColumn<int>(
                name: "EnderecoId",
                table: "Encomendas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
    }
}
