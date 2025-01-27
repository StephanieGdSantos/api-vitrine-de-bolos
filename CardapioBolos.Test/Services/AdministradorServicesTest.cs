using Bogus;
using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Services;
using CardapioBolos.Test.Fixtures;
using CardapioBolos.Utils;
using Microsoft.EntityFrameworkCore;

namespace CardapioBolos.Test.Services
{
    public class AdministradorServicesTest : IClassFixture<DBFixture>
    {
        private readonly CardapioBolosContext _context;
        private readonly AdministradorServices _administradorServices;

        public AdministradorServicesTest(DBFixture fixture)
        {
            _context = fixture.Context;
            _administradorServices = new AdministradorServices(_context);
        }

        [Theory]
        [InlineData(null, "senha", false)]
        [InlineData("email", null, false)]
        [InlineData("email", "senha", false)]
        [InlineData("email", "senhaErrada", false)]
        [InlineData("teste@gmail.com", "TesteSenha", true)]
        public void Autenticar_DeveRetornarResultadoEsperado(string email, string senha, bool resultadoEsperado)
        {
            var resultado = _administradorServices.Autenticar(email, senha);

            Assert.Equal(resultadoEsperado, resultado);
        }
    }
}
