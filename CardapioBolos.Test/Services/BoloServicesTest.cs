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

        public static IEnumerable<object[]> GetEncomendaRequests()
        {
            var endereco = new EnderecoDTO
            {
                Cidade = "Cidade Teste",
                Bairro = "Bairro Teste",
                Logradouro = "Logradouro Teste",
                Numero = "123",
                Complemento = "Complemento Teste"
            };

            var bolo1 = new Bolo { Id = 1, Nome = "Bolo de Chocolate", Peso = 1, Formato = "Redondo", Observacao = "Sem açúcar", Topper = true, PapelDeArroz = false, Presente = true, Preco = 25.0 };
            var bolo2 = new Bolo { Id = 2, Nome = "Bolo de Cenoura", Peso = 1, Formato = "Quadrado", Observacao = "Com cobertura", Topper = false, PapelDeArroz = true, Presente = false, Preco = 20.0 };

            yield return new object[]
            {
                new EncomendaRequest(DateTime.Now, "Cliente Teste", "123456789", new List<Bolo> { bolo1, bolo2 }, 45.0, DateTime.Now.AddDays(1), endereco),
                new List<Bolo> { bolo1, bolo2 }
            };

            yield return new object[]
            {
                new EncomendaRequest(DateTime.Now, "Cliente Teste", "123456789", new List<Bolo> { bolo1 }, 25.0, DateTime.Now.AddDays(1), endereco),
                new List<Bolo> { bolo1 }
            };

            yield return new object[]
            {
                new EncomendaRequest(DateTime.Now, "Cliente Teste", "123456789", new List<Bolo>(), 0.0, DateTime.Now.AddDays(1), endereco),
                null
            };
        }

        [Theory]
        [MemberData(nameof(GetEncomendaRequests))]
        public void ObterListaDeBolosInseridos_DeveRetornarListaDeBolosEsperada(EncomendaRequest encomendaRequest, List<Bolo> bolosEsperados)
        {
            var resultado = _boloServices.ObterListaDeBolosInseridos(encomendaRequest);

            if (bolosEsperados == null)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.NotNull(resultado);
                Assert.Equal(bolosEsperados.Count, resultado.Count);
                for (int i = 0; i < bolosEsperados.Count; i++)
                {
                    Assert.Equal(bolosEsperados[i].Id, resultado[i].Id);
                    Assert.Equal(bolosEsperados[i].Nome, resultado[i].Nome);
                    Assert.Equal(bolosEsperados[i].Peso, resultado[i].Peso);
                    Assert.Equal(bolosEsperados[i].Formato, resultado[i].Formato);
                    Assert.Equal(bolosEsperados[i].Observacao, resultado[i].Observacao);
                    Assert.Equal(bolosEsperados[i].Topper, resultado[i].Topper);
                    Assert.Equal(bolosEsperados[i].PapelDeArroz, resultado[i].PapelDeArroz);
                    Assert.Equal(bolosEsperados[i].Presente, resultado[i].Presente);
                    Assert.Equal(bolosEsperados[i].Preco, resultado[i].Preco);
                }
            }
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
