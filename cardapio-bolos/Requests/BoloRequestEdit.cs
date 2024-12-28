using CardapioBolos.Model;
using System.ComponentModel.DataAnnotations;

namespace CardapioBolos.Requests;
public record BoloRequestEdit(int Id, string Nome, string Imagem, string Descricao, string ListaDeIngredientes, double Preco, double Peso);
