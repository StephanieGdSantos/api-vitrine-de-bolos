using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cardapio_bolos.Migrations
{
    /// <inheritdoc />
    public partial class PopulandoABase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "Bolos", 
                new string[] { 
                    "Nome", 
                    "Descricao", 
                    "Preco",
                    "ListaIngredientes",
                    "Imagem",
                    "Peso"
                }, 
                new object[] { 
                    "Bolo de Cenoura", 
                    "Bolo de cenoura com cobertura de chocolate",
                    20.0,
                    "Cenoura, ovos, óleo, açúcar, farinha de trigo, fermento em pó, chocolate em pó, açúcar e manteiga",
                    "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-cenoura-1-730x449.jpg",
                    1.0
                }
            );

            migrationBuilder.InsertData(
                "Bolos",
                new string[] {
                    "Nome",
                    "Descricao",
                    "Preco",
                    "ListaIngredientes",
                    "Imagem",
                    "Peso"
                },
                new object[] {
                    "Bolo de Chocolate",
                    "Bolo de chocolate com cobertura de chocolate",
                    25.0,
                    "Ovos, açúcar, óleo, farinha de trigo, chocolate em pó, fermento em pó, leite, açúcar e manteiga",
                    "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-chocolate-1-730x449.jpg",
                    1.0
                }
            );

            migrationBuilder.InsertData(
                "Bolos",
                new string[] {
                    "Nome",
                    "Descricao",
                    "Preco",
                    "ListaIngredientes",
                    "Imagem",
                    "Peso"
                },
                new object[] {
                    "Bolo de Laranja",
                    "Bolo de laranja com cobertura de chocolate",
                    20.0,
                    "Laranja, ovos, óleo, açúcar, farinha de trigo, fermento em pó, chocolate em pó, açúcar e manteiga",
                    "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-laranja-1-730x449.jpg",
                    1.0
                }
            );

            migrationBuilder.InsertData(
            "Bolos",
                new string[] {
                    "Nome",
                    "Descricao",
                    "Preco",
                    "ListaIngredientes",
                    "Imagem",
                    "Peso"
                },
                new object[] {
                    "Bolo de Limão",
                    "Bolo de limão com cobertura de chocolate",
                    20.0,
                    "Limão, ovos, óleo, açúcar, farinha de trigo, fermento em pó, chocolate em pó, açúcar e manteiga",
                    "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-limao-1-730x449.jpg",
                    1.0
                }
            );

            migrationBuilder.InsertData(
               "Administrador",
               new string[] {
                      "Nome",
                      "Telefone",
                      "Email",
                      "Senha"
                },
               new object[] {
                         "Maxwell",
                         "11945670394",
                         "maxmoto@gmail.com",
                         "senha1234"
               }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData("Bolos", "Nome", "Bolo de Cenoura");
            migrationBuilder.DeleteData("Bolos", "Nome", "Bolo de Chocolate");
            migrationBuilder.DeleteData("Bolos", "Nome", "Bolo de Laranja");
            migrationBuilder.DeleteData("Bolos", "Nome", "Bolo de Limão");
            migrationBuilder.DeleteData("Administrador", "Nome", "Maxwell");
        }
    }
}
