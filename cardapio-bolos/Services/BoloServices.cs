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

            var nomeBolosNaoEncontrados = bolos
                .Select(bolo => bolo.Nome)
                .ToList()
                .Except(bolosEncontrados
                    .Select(bolo => bolo.Nome))
                .ToList();

            var bolosNaoEncontrados = new List<Bolo>();

            nomeBolosNaoEncontrados.ForEach(bolo =>
            {
                bolosNaoEncontrados.Add(new Bolo()
                {
                    Nome = bolo
                });
            });

            bolosNaoEncontrados
                .ForEach(bolo => bolosEncontrados
                    .Add(bolo));

            var bolosAInserir = bolosEncontrados;

            return bolosAInserir;
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