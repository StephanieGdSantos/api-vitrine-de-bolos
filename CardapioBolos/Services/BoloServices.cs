using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;

namespace CardapioBolos.Services
{
    public class BoloServices
    {
        private readonly DAL<Bolo> _bolosDAL;
        private readonly CardapioBolosContext _context;

        public BoloServices(CardapioBolosContext context)
        {
            _bolosDAL = new DAL<Bolo>(context);
            _context = context;
        }

        public async Task<List<Bolo>?> ObterListaDeBolosInseridosAsync(EncomendaRequest encomenda)
        {
            var bolos = await VerificarBolosExistentes(encomenda.Bolos);

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

        public async Task<List<Bolo>>? VerificarBolosExistentes(List<Bolo> bolos)
        {
            if (bolos.Count == 0)
                return null;

            var bolosId = bolos
                .Select(b => b.Id)
                .ToList();

            var bolosEncontrados = _bolosDAL
                .Listar()
                .Result
                .Where(b => bolosId
                    .Contains(b.Id))
                .OrderBy(b => bolosId.IndexOf(b.Id))
                .ToList();

            var bolosNaoEncontrados = bolos
                .Where(bolo => !bolosEncontrados
                    .Any(b => b.Id == bolo.Id))
                .Select(bolo => new Bolo { Nome = bolo.Nome })
                .ToList();

            return (List<Bolo>)bolosEncontrados;
        }

        public string FormatarNomeParaBusca(string nome)
        {
            var nomeSemSimbolos = nome.Split('+');
            var nomeProcurado = string.Join(" ", nomeSemSimbolos).ToLower();

            return nomeProcurado.ToLower();
        }

        public string VerificarIngredientesNaoEncontrados(List<string> nomesIngredientesInseridos, List<Ingrediente> ingredientesDoBolo)
        {
            var ingredientesNaoEncontrados = nomesIngredientesInseridos
                            .Except(ingredientesDoBolo
                            .Select(ingrediente => ingrediente.Nome))
                            .ToList();

            if (ingredientesNaoEncontrados.Count == 0)
            {
                return "";
            }

            string stringIngredientesNaoEncontrados = "";
            if (ingredientesNaoEncontrados.Count > 0)
                stringIngredientesNaoEncontrados = $"Os seguintes ingredientes não foram encontrados: {string.Join(", ", ingredientesNaoEncontrados)}";

            return stringIngredientesNaoEncontrados;
        }
    }
}