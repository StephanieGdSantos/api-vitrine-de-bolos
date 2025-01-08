using CardapioBolos.DTO;
using CardapioBolos.Model;
using System.Collections.Generic;

namespace CardapioBolos.Requests
{
    public record EncomendaRequestEdit(int Id, string TelefoneCliente, List<Bolo> Bolos, double ValorFinal, bool Finalizado, DateTime DataDaEntrega, EnderecoDTO Endereco) { }
}