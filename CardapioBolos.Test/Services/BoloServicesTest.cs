using CardapioBolos.Banco;
using CardapioBolos.DTO;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
using CardapioBolos.Test.Fixtures;
using Xunit;

namespace CardapioBolos.Test.Services
{
    public class BoloServicesTest : IClassFixture<DBFixture>
    {
        private readonly CardapioBolosContext _context;
        private readonly BoloServices _boloServices;

        public BoloServicesTest(DBFixture fixture)
        {
            _context = fixture.Context;
            _boloServices = new BoloServices(_context);
        }

        [Theory]
        [InlineData("2023-01-01", "Cliente", "12345678910", new object[] { }, 100.00, "2023-01-01", "Cidade", "Bairro", "Logradouro", "Numero", "Complemento", null)]
        [InlineData("2023-01-01", "Cliente", "12345678910", new object[] { new object[] { 1, "Bolo de Cenoura", "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-cenoura-1-730x449.jpg", "Bolo de cenoura com cobertura de chocolate", new object[] { }, 20.0 } }, 100.00, "2023-01-01", "Cidade", "Bairro", "Logradouro", "Numero", "Complemento", new object[] { new object[] { 1, "Bolo de Cenoura", "https://www.receiteria.com.br/wp-content/uploads/receitas-de-bolo-de-cenoura-1-730x449.jpg", "Bolo de cenoura com cobertura de chocolate", new object[] { }, 20.0 } })]
        public void ObterListaDeBolosInseridos_DeveRetornarRespostaEsperada(string dataDoPedido, string nomeCliente, string telefoneCliente, object[] bolos, double valorFinal, string dataDaEntrega, string cidade, string bairro, string logradouro, string numero, string complemento, object[] boloEsperadoNoRetorno)
        {
            // Arrange
            var dataPedido = DateTime.Parse(dataDoPedido);
            var dataEntrega = DateTime.Parse(dataDaEntrega);
            var bolosList = bolos.Select(b => new Bolo
            {
                Id = (int)((object[])b)[0],
                Nome = (string)((object[])b)[1],
                Imagem = (string)((object[])b)[2],
                Descricao = (string)((object[])b)[3],
                Ingredientes = ((object[])((object[])b)[4]).Cast<Ingrediente>().ToList(),
                Preco = (double)((object[])b)[5]
            }).ToList();

            List<Bolo> resultadoEsperado = null;
            if (boloEsperadoNoRetorno != null)
            {
                resultadoEsperado = boloEsperadoNoRetorno.Select(b => new Bolo
                {
                    Id = (int)((object[])b)[0],
                    Nome = (string)((object[])b)[1],
                    Imagem = (string)((object[])b)[2],
                    Descricao = (string)((object[])b)[3],
                    Ingredientes = ((object[])((object[])b)[4]).Cast<Ingrediente>().ToList(),
                    Preco = (double)((object[])b)[5],
                    Peso = 1
                }).ToList();
            }

            var endereco = new EnderecoDTO { Cidade = cidade, Bairro = bairro, Logradouro = logradouro, Numero = numero, Complemento = complemento };
            var encomenda = new EncomendaRequest(dataPedido, nomeCliente, telefoneCliente, bolosList, valorFinal, dataEntrega, endereco);

            // Act
            var resultado = _boloServices.ObterListaDeBolosInseridos(encomenda);

            // Assert
            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData(new object[] { new object[] { 1, "Bolo de Chocolate" }, new object[] { 2, "Bolo de Cenoura" } }, 2)]
        [InlineData(new object[] { new object[] { 1, "Bolo de Chocolate" }, new object[] { 4, "Bolo de limão" } }, 1)]
        [InlineData(new object[] { }, 0)]
        public void VerificarBolosExistentes_DeveRetornarQuantidadeEsperada(object[] bolosData, int quantidadeEsperada)
        {
            var bolos = bolosData.Select(b => new Bolo { Id = (int)((object[])b)[0], Nome = (string)((object[])b)[1] }).ToList();

            var resultado = _boloServices.VerificarBolosExistentes(bolos);

            if (quantidadeEsperada == 0)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.NotNull(resultado);
                Assert.Equal(quantidadeEsperada, resultado.Count);
            }
        }

        [Theory]
        [InlineData("Bolo+de+Chocolate", "bolo de chocolate")]
        [InlineData("Bolo+de+Morango", "bolo de morango")]
        [InlineData("Bolo+de+Laranja", "bolo de laranja")]
        public void FormatarNomeParaBusca_DeveRetornarNomeFormatado(string nome, string nomeEsperado)
        {
            var resultado = _boloServices.FormatarNomeParaBusca(nome);

            Assert.Equal(nomeEsperado, resultado);
        }

        [Theory]
        [InlineData(new string[] { "Farinha", "Açúcar", "Ovo" }, new string[] { "Farinha", "Açúcar", "Ovo" }, "")]
        [InlineData(new string[] { "Farinha", "Açúcar", "Ovo" }, new string[] { "Farinha", "Açúcar" }, "Os seguintes ingredientes não foram encontrados: Ovo")]
        [InlineData(new string[] { "Farinha", "Açúcar", "Ovo" }, new string[] { }, "Os seguintes ingredientes não foram encontrados: Farinha, Açúcar, Ovo")]
        public void VerificarIngredientesNaoEncontrados_DeveRetornarMensagemEsperada(string[] nomesIngredientesInseridos, string[] nomesIngredientesExistentes, string mensagemEsperada)
        {
            var ingredientesDoBolo = nomesIngredientesExistentes.Select(nome => new Ingrediente { Nome = nome }).ToList();

            var resultado = _boloServices.VerificarIngredientesNaoEncontrados(nomesIngredientesInseridos.ToList(), ingredientesDoBolo);

            Assert.Equal(mensagemEsperada, resultado);
        }
    }
}
