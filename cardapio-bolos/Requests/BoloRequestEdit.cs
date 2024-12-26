using CardapioBolos.Model;
using System.ComponentModel.DataAnnotations;

namespace CardapioBolos.Requests;
public record BoloRequestEdit(int Id, string Nome, [Required] string Imagem, string Descricao, string ListaDeIngredientes, double Preco) : BoloRequest(Nome, Imagem, Descricao, ListaDeIngredientes, Preco);
