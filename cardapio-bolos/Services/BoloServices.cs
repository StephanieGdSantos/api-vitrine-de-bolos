using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;

namespace CardapioBolos.Services
{
    public class BoloServices
    {
        private readonly DAL<Bolo> _bolosDAL;

        public BoloServices(CardapioBolosContext context)
        {
            _bolosDAL = new DAL<Bolo>(context);
        }

        public List<Bolo>? ObterListaDeBolosInseridos(EncomendaRequest encomenda)
        {
            var bolos = VerificarBolosExistentes(encomenda.Bolos);

            if (bolos == null)
                return null;

            bolos.ForEach(bolo =>
            {
                bolo.Peso = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Peso;
                bolo.Formato = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Formato;
                bolo.Observacao = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Observacao;
                bolo.Topper = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Topper;
                bolo.PapelDeArroz = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).PapelDeArroz;
                bolo.Presente = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Presente;
                bolo.Preco *= bolo.Peso;
            });

            return bolos;
        }

        public List<Bolo>? VerificarBolosExistentes(List<Bolo> bolos)
        {
            if (bolos.Count == 0)
                return null;

            var bolosId = bolos
                .Select(b => b.Id)
                .ToList();

            var bolosEncontrados = _bolosDAL
                .Listar()
                .Where(b => bolosId.Contains(b.Id))
                .ToList();

            var bolosNaoEncontrados = bolos
                .Except(bolosEncontrados)
                .ToList();

            bolosNaoEncontrados.ForEach(bolo =>
            {
                bolo.Nome = "Bolo não encontrado";
                bolo.Imagem = null;
                bolo.Descricao = null;
                bolo.Ingredientes = null;
                bolo.Preco = 0;
                bolo.Peso = 0;
                bolo.Formato = null;
                bolo.Observacao = null;
                bolo.Topper = null;
                bolo.PapelDeArroz = null;
                bolo.Presente = null;
            });

            bolosNaoEncontrados
                .ForEach(bolo => bolosEncontrados
                    .Add(bolo));

            var bolosAInserir = bolosEncontrados;

            return bolosAInserir;
        }

        public string FormatarNomeParaBusca(string nome)
        {
            var nomeProcurado = nome.Replace(" ", "+");
            nomeProcurado.Split('+');
            nomeProcurado = string.Join(" ", nomeProcurado).ToLower();

            return nomeProcurado.ToLower();
        }
    }
}