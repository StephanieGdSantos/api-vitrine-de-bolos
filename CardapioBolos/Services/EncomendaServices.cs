using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;

namespace CardapioBolos.Services
{
    public class EncomendaServices
    {
        private readonly DAL<Encomenda> _encomendaDAL;
        private readonly CardapioBolosContext _context;

        public EncomendaServices(CardapioBolosContext context)
        {
            _encomendaDAL = new DAL<Encomenda>(context);
            _context = context;
        }

        public Encomenda? ObterEncomenda(EncomendaRequest encomenda)
        {
            var listaDeBolos = new BoloServices(_context).ObterListaDeBolosInseridos(encomenda);
             
            if (listaDeBolos == null)
                return null;

            var valorFinalEncomenda = listaDeBolos
                .Select(b => b.Preco)
                .Sum();

            var enderecoDaEncomenda = encomenda.Endereco;

            var novaEncomenda = new Encomenda(encomenda.DataDoPedido, encomenda.NomeCliente, encomenda.TelefoneCliente, listaDeBolos, valorFinalEncomenda, encomenda.DataDaEntrega, enderecoDaEncomenda.Cidade, enderecoDaEncomenda.Bairro, enderecoDaEncomenda.Logradouro, enderecoDaEncomenda.Numero, enderecoDaEncomenda.Complemento);

            return novaEncomenda;
        }
    }
}
