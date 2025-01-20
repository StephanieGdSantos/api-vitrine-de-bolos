using CardapioBolos.Utils;

namespace CardapioBolos.Test.Utils
{
    public class VerificadorDeSenhaTest
    {
        [Fact]
        public void GerarHashSenha_QuandoASenhaForValida_DeveRetornarHashDaSenha()
        {
            var senha = "123456";
            var hashEsperado = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";

            var hash = VerificadorDeSenha.GerarHashSenha(senha);

            Assert.Equal(hashEsperado, hash);
        }

        [Fact]
        public void GerarHashSenha_QuandoASenhaForVazia_DeveRetornarVazio()
        {
            var senha = "";

            var hash = VerificadorDeSenha.GerarHashSenha(senha);

            Assert.Empty(hash);
        }

        [Fact]
        public void VerificarSenha_QuandoSenhaEstiverCorreta_DeveRetornarTrue()
        {
            var senhaInserida = "123456";
            var hashArmazenado = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";

            var senhaCorreta = VerificadorDeSenha.VerificarSenha(senhaInserida, hashArmazenado);

            Assert.True(senhaCorreta);
        }

        [Fact]
        public void VerificarSenha_QuandoASenhaForVazia_DeveRetornarFalse()
        {
            var senhaInserida = "";
            var hashArmazenado = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=";

            var senhaCorreta = VerificadorDeSenha.VerificarSenha(senhaInserida, hashArmazenado);

            Assert.False(senhaCorreta);
        }

        [Fact]
        public void VerificarSenha_QuandoNaoTiverSenhaArmazenada_DeveRetornarFalse()
        {
            var senhaInserida = "123456";
            var hashArmazenado = "";

            var senhaCorreta = VerificadorDeSenha.VerificarSenha(senhaInserida, hashArmazenado);

            Assert.False(senhaCorreta);
        }

        [Fact]
        public void VerificarSenha_QuandoASenhaEstiverErrada_DeveRetornarFalse()
        {
            var senhaInserida = "123456";
            var hashArmazenado = "jZae727K08KaOmKSgOaGzww/XVqGr/PKEgIMkjrcbJI=123";

            var senhaCorreta = VerificadorDeSenha.VerificarSenha(senhaInserida, hashArmazenado);

            Assert.False(senhaCorreta);
        }
    }
}
