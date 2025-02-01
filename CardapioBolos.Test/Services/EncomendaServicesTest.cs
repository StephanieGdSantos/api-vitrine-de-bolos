using CardapioBolos.Banco;
using CardapioBolos.DTO;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
using CardapioBolos.Test.Fixtures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardapioBolos.Test.Services
{
    public class EncomendaServicesTest : IClassFixture<DBFixture>
    {
        private readonly CardapioBolosContext _context;
        private readonly EncomendaServices _encomendaServices;

        public EncomendaServicesTest(DBFixture fixture)
        {
            _context = fixture.Context;
            _encomendaServices = new EncomendaServices(_context);
        }

        [Theory]
        [MemberData(nameof(GetEncomendaRequests))]
        public void ObterEncomenda_DeveRetornarResultadoEsperado(EncomendaRequest encomendaRequest, Encomenda encomendaEsperada)
        {
            var resultado = _encomendaServices.ObterEncomenda(encomendaRequest);

            if (encomendaEsperada == null)
            {
                Assert.Null(resultado);
            }
            else
            {
                Assert.NotNull(resultado);
                Assert.Equal(encomendaEsperada.NomeCliente, resultado.NomeCliente);
                Assert.Equal(encomendaEsperada.TelefoneCliente, resultado.TelefoneCliente);
                Assert.Equal(encomendaEsperada.ValorFinal, resultado.ValorFinal);
                Assert.Equal(encomendaEsperada.Cidade, resultado.Cidade);
                Assert.Equal(encomendaEsperada.Bairro, resultado.Bairro);
                Assert.Equal(encomendaEsperada.Logradouro, resultado.Logradouro);
                Assert.Equal(encomendaEsperada.Numero, resultado.Numero);
                Assert.Equal(encomendaEsperada.Complemento, resultado.Complemento);
                Assert.Equal(encomendaEsperada.Bolos.Count, resultado.Bolos.Count);
                for (int i = 0; i < encomendaEsperada.Bolos.Count; i++)
                {
                    Assert.Equal(encomendaEsperada.Bolos[i].Id, resultado.Bolos[i].Id);
                    Assert.Equal(encomendaEsperada.Bolos[i].Nome, resultado.Bolos[i].Nome);
                    Assert.Equal(encomendaEsperada.Bolos[i].Peso, resultado.Bolos[i].Peso);
                    Assert.Equal(encomendaEsperada.Bolos[i].Formato, resultado.Bolos[i].Formato);
                    Assert.Equal(encomendaEsperada.Bolos[i].Observacao, resultado.Bolos[i].Observacao);
                    Assert.Equal(encomendaEsperada.Bolos[i].Topper, resultado.Bolos[i].Topper);
                    Assert.Equal(encomendaEsperada.Bolos[i].PapelDeArroz, resultado.Bolos[i].PapelDeArroz);
                    Assert.Equal(encomendaEsperada.Bolos[i].Presente, resultado.Bolos[i].Presente);
                    Assert.Equal(encomendaEsperada.Bolos[i].Preco, resultado.Bolos[i].Preco);
                }
            }
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
                    new Encomenda(DateTime.Now, "Cliente Teste", "123456789", new List<Bolo> { bolo1, bolo2 }, 45.0, DateTime.Now.AddDays(1), "Cidade Teste", "Bairro Teste", "Logradouro Teste", "123", "Complemento Teste")
            };

            yield return new object[]
            {
                    new EncomendaRequest(DateTime.Now, "Cliente Teste", "123456789", new List<Bolo> { bolo1 }, 25.0, DateTime.Now.AddDays(1), endereco),
                    new Encomenda(DateTime.Now, "Cliente Teste", "123456789", new List<Bolo> { bolo1 }, 25.0, DateTime.Now.AddDays(1), "Cidade Teste", "Bairro Teste", "Logradouro Teste", "123", "Complemento Teste")
            };

            yield return new object[]
            {
                    new EncomendaRequest(DateTime.Now, "Cliente Teste", "123456789", new List<Bolo>(), 0.0, DateTime.Now.AddDays(1), endereco),
                    null
            };
        }
    }
}
