using Bogus;
using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Services;
using CardapioBolos.Test.Fixtures;
using CardapioBolos.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http.HttpResults;

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

        [Theory]
        [InlineData("teste@gmail.com", true)]
        [InlineData("novoEmail@gmail.com", false)]
        [InlineData(null, false)]
        public void VerificarSeEmailJaEhUsado_DeveRetornarResultadoEsperado(string email, bool resultadoEsperado)
        {
            var resultado = _administradorServices.VerificarSeEmailJaEhUsado(email);

            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData("admin", true)]
        [InlineData("user", false)]
        public void ValidaSeEhAdministrador_DeveRetornarResultadoEsperado(string role, bool resultadoEsperado)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var usuario = new ClaimsPrincipal(identity);

            var resultado = _administradorServices.ValidaSeEhAdministrador(usuario);

            Assert.Equal(resultadoEsperado, resultado);
        }

        [Theory]
        [InlineData("Nome", "email@gmail.com", "12345678910", "Senha123", "Microsoft.AspNetCore.Http.HttpResults.Ok")]
        [InlineData("Nome", "teste@gmail.com", "12345678910", "Senha123", "Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult")]
        [InlineData("Nome", "testegmail.com", "12345678910", "Senha123", "Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult")]
        [InlineData("Nome", "", "12345678910", "Senha123", "Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult")]
        [InlineData("Nome", "email@teste.com", "", "Senha123", "Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult")]
        [InlineData("Nome", "email@teste.com", "123456789", "", "Microsoft.AspNetCore.Http.HttpResults.ProblemHttpResult")]
        public void ValidarDadosDoAdministrador_DeveRetornarResultadoEsperado(string nome, string email, string telefone, string senha, string resultadoEsperado)
        {
            var administrador = new Administrador(nome, telefone, email, senha);
            var resultado = _administradorServices.ValidarDadosDoAdministrador(administrador);

            Assert.Equal(resultadoEsperado, resultado.ToString());
        }

        [Theory]
        [InlineData("(11) 1234-5678", "1112345678")]
        [InlineData("(11) 12345-6789", "11123456789")]
        [InlineData("11123456789", "11123456789")]
        [InlineData("", "")]
        public void FormatarTelefone_DeveRetornarTelefoneSemCaracteres(string telefone, string telefoneEsperado)
        {
            var resultado = _administradorServices.FormatarTelefone(telefone);
            Assert.Equal(telefoneEsperado, resultado);
        }
    }
}
