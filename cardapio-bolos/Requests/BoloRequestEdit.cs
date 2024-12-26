using CardapioBolos.Model;
using System.ComponentModel.DataAnnotations;

namespace CardapioBolos.Requests;
public record BoloRequestEdit(int Id, string Nome, string Descricao, ICollection<Ingrediente> ListaDeIngredientes, double Preco) : BoloRequest(Nome, Descricao, ListaDeIngredientes, Preco);
