using CardapioBolos.Utils;

namespace CardapioBolos.Test.Utils
{
    public class BuscadorTest
    {

        [Theory]
        [InlineData(new string[] { }, "bolo", new string[] { })]
        [InlineData(new string[] { "bolo", "bola", "bala", "bela", "rolo", "rua", "poço" }, "bolo", new string[] { "bolo", "bola" })]
        [InlineData(new string[] { "bolo", "bola", "bala", "bela" }, "bola", new string[] { "bola", "bolo" })]
        public void BuscarPorMaiorStringComum_DeveRetornarResultadoEsperado(string[] listaPalavrasDisponiveis, string palavraProcurada, string[] resultadoEsperado)
        {
            var resultado = Buscador.BuscarPorMaiorStringComum(listaPalavrasDisponiveis, palavraProcurada);

            Assert.Equal(resultadoEsperado, resultado);
        }

        [Fact]
        public void BuscarPorMaiorStringComum_QuandoNaoHouverPalavrasSemelhantes_DeveRetornarAlgoMesmoAssim()
        {
            var palavrasDisponiveis = new string[] { "casa", "carro", "moto" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorStringComum(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(2, resultado.Count());
        }

        [Theory]
        [InlineData(new string[] { }, "bolo", new string[] { })]
        [InlineData(new string[] { "bolo", "bola", "bala", "bela", "rolo", "rua", "poço" }, "bolo", new string[] { "bolo", "bola" })]
        [InlineData(new string[] { "bolo", "bola", "bala", "bela" }, "bola", new string[] { "bola", "bolo" })]
        public void BuscarPorMaiorProporcaoSemelhante_DeveRetornarResultadoEsperado(string[] listaPalavrasDisponiveis, string palavraProcurada, string[] resultadoEsperado)
        {
            var resultado = Buscador.BuscarPorMaiorProporcaoSemelhante(listaPalavrasDisponiveis, palavraProcurada);

            Assert.Equal(resultadoEsperado, resultado);
        }

        [Fact]
        public void BuscarPorMaiorProporcaoSemelhante_QuandoNaoHouverPalavrasSemelhantes_DeveRetornarAlgoMesmoAssim()
        {
            var palavrasDisponiveis = new string[] { "casa", "carro", "moto" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorProporcaoSemelhante(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(2, resultado.Count());
        }

        [Theory]
        [InlineData(new string[] { }, "bolo", new string[] { })]
        [InlineData(new string[] { "bolo", "bola", "bala", "bela", "rolo", "rua", "poço" }, "bolo", new string[] { "bolo", "bola" })]
        [InlineData(new string[] { "bolo", "bola", "bala", "bela" }, "bola", new string[] { "bola", "bolo" })]
        public void BuscarNomesSemelhantes_DeveRetornarResultadoEsperado(string[] palavrasDisponiveis, string palavraProcurada, string[] resultadoEsperado)
        {
            var resultado = Buscador.BuscarNomesSemelhantes(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(resultadoEsperado, resultado);
        }

        [Fact]
        public void BuscarNomesSemelhantes_QuandoNaoHouverPalavrasSemelhantes_DeveRetornarAlgoMesmoAssim()
        {
            var palavrasDisponiveis = new string[] { "casa", "carro", "moto" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarNomesSemelhantes(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(2, resultado.Count());
        }
    }
}