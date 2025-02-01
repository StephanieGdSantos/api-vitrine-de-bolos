using CardapioBolos.DTO;
using CardapioBolos.Model;
using System.ComponentModel.DataAnnotations;

namespace CardapioBolos.Requests;
public record BoloRequest([Required] string Nome, [Required] string Imagem, [Required] string Descricao, [Required] List<IngredienteDTO> Ingredientes, [Required] double Preco, [Required] double Peso);
