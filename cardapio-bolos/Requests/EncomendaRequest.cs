using CadapioBolos.DTO;
using CardapioBolos.Model;
using System.ComponentModel.DataAnnotations;

namespace CardapioBolos.Requests
{
    public record EncomendaRequest([Required] DateTime DataDoPedido, [Required] string NomeCliente, [Required] string TelefoneCliente, [Required] List<Bolo> Bolos, [Required] double ValorFinal, [Required] DateTime DataDaEntrega) { }
}
