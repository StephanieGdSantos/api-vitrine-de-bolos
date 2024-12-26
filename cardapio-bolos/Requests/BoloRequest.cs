using CardapioBolos.Model;
using System.ComponentModel.DataAnnotations;

namespace CardapioBolos.Requests;
public record BoloRequest([Required] string Nome, [Required] string Descricao, [Required] string ListaDeIngredientes, [Required] double Preco);
