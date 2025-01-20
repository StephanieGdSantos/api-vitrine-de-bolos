using CardapioBolos.Utils;

namespace CardapioBolos.Test.Utils
{
    public class BuscadorTest
    {
        [Fact]
        public void BuscarPorMaiorStringComum_QuandoAsPalavrasDisponiveisForemVazias_DeveRetornarVazio()
        {
            var palavrasDisponiveis = new string[] { };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorStringComum(palavrasDisponiveis, palavraProcurada);

            Assert.Empty(resultado);
        }

        [Fact]
        public void BuscarPorMaiorStringComum_QuandoNaoHouverPalavrasSemelhantes_DeveRetornarAlgoMesmoAssim()
        {
            var palavrasDisponiveis = new string[] { "casa", "carro", "moto" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorStringComum(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void BuscarPorMaiorStringComum_QuandoHouverVariasPalavrasSemelhantes_RetornaAsDuasMaisProximas()
        {
            var palavrasDisponiveis = new string[] { "bolo", "bola", "bala", "bela", "rolo", "rua", "po�o" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorStringComum(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(new string[] { "bolo", "bola" }, resultado);
        }

        [Fact]
        public void BuscarPorMaiorStringComum_QuandoHouverEmpateDeSemelhanca_RetornaAsDuasMaisProximas()
        {
            var palavrasDisponiveis = new string[] { "bolo", "bola", "bala", "bela" };
            var palavraProcurada = "bola";

            var resultado = Buscador.BuscarPorMaiorStringComum(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(new string[] { "bola", "bolo" }, resultado);
        }

        [Fact]
        public void BuscarPorMaiorProporcaoSemelhante_QuandoAsPalavrasDisponiveisForemVazias_DeveRetornarVazio()
        {
            var palavrasDisponiveis = new string[] { };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorProporcaoSemelhante(palavrasDisponiveis, palavraProcurada);

            Assert.Empty(resultado);
        }

        [Fact]
        public void BuscarPorMaiorProporcaoSemelhante_QuandoNaoHouverPalavrasSemelhantes_DeveRetornarAlgoMesmoAssim()
        {
            var palavrasDisponiveis = new string[] { "casa", "carro", "moto" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorProporcaoSemelhante(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void BuscarPorMaiorProporcaoSemelhante_QuandoHouverVariasPalavrasSemelhantes_RetornaAsDuasMaisProximas()
        {
            var palavrasDisponiveis = new string[] { "bolo", "bola", "bala", "bela", "rolo", "rua", "poço" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarPorMaiorProporcaoSemelhante(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(new string[] { "bolo", "bola" }, resultado);
        }

        [Fact]
        public void BuscarPorMaiorProporcaoSemelhante_QuandoHouverEmpateDeSemelhanca_RetornaAsDuasMaisProximas()
        {
            var palavrasDisponiveis = new string[] { "bolo", "bola", "bala", "bela" };
            var palavraProcurada = "bola";

            var resultado = Buscador.BuscarPorMaiorProporcaoSemelhante(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(new string[] { "bola", "bolo" }, resultado);
        }

        [Fact]
        public void BuscarNomesSemelhantes_QuandoAsPalavrasDisponiveisForemVazias_DeveRetornarVazio()
        {
            var palavrasDisponiveis = new string[] { };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarNomesSemelhantes(palavrasDisponiveis, palavraProcurada);

            Assert.Empty(resultado);
        }

        [Fact]
        public void BuscarNomesSemelhantes_QuandoNaoHouverPalavrasSemelhantes_DeveRetornarAlgoMesmoAssim()
        {
            var palavrasDisponiveis = new string[] { "casa", "carro", "moto" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarNomesSemelhantes(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(2, resultado.Count());
        }

        [Fact]
        public void BuscarNomesSemelhantes_QuandoHouverVariasPalavrasSemelhantes_RetornaAsDuasMaisProximas()
        {
            var palavrasDisponiveis = new string[] { "bolo", "bola", "bala", "bela", "rolo", "rua", "poço" };
            var palavraProcurada = "bolo";

            var resultado = Buscador.BuscarNomesSemelhantes(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(new string[] { "bolo", "bola" }, resultado);
        }

        [Fact]
        public void BuscarNomesSemelhantes_QuandoHouverEmpateDeSemelhanca_RetornaAsDuasMaisProximas()
        {
            var palavrasDisponiveis = new string[] { "bolo", "bola", "bala", "bela" };
            var palavraProcurada = "bola";

            var resultado = Buscador.BuscarNomesSemelhantes(palavrasDisponiveis, palavraProcurada);

            Assert.Equal(new string[] { "bola", "bolo" }, resultado);
        }
    }
}