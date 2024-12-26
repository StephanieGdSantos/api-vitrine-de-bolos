using CardapioBolos.Model;
using System.ComponentModel.DataAnnotations;

namespace CardapioBolos.Requests;
public record BoloRequestEdit(int Id, string Nome, string Descricao, string ListaDeIngredientes, double Preco) : BoloRequest(Nome, Descricao, ListaDeIngredientes, Preco);
