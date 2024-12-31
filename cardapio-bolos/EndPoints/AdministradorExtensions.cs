using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Services;
using CardapioBolos.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace CardapioBolos.EndPoints
{
    public static class AdministradorExtensions
    {
        public static void AddEndPointsAdministrador(this WebApplication app)
        {
            app.MapPost("/admin", ([FromServices] DAL<Administrador> dal, [FromServices] AdministradorServices administradorServices, [FromBody] Administrador administrador, [FromServices] CardapioBolosContext context) =>
            {
                var emailJaUsado = administradorServices.VerificarSeEmailJaEhUsado(administrador.Email);
                if (emailJaUsado)
                    return Results.Problem("O e-mail já está cadastrado.");

                var hashSenha = VerificadorDeSenha.GerarHashSenha(administrador.Senha);
                var novoAdministrador = new Administrador(administrador.Nome, administrador.Telefone, administrador.Email, hashSenha);
                dal.Adicionar(novoAdministrador);
                return Results.Ok();
            });

            app.MapPost("/admin/login", ([FromServices] AdministradorServices administradorServices, [FromBody] Administrador administrador) =>
            {
                var senhaValida = administradorServices.Autenticar(administrador.Email, administrador.Senha);
                if (!senhaValida)
                    return Results.Problem("Dados inválidos.");

                var claims = new List<Claim>
                {
                        new(ClaimTypes.Name, administrador.Nome),
                        new(ClaimTypes.Email, administrador.Email),
                        new(ClaimTypes.Role, "admin")
                };
                var identidade = new ClaimsIdentity(claims, "login");
                var usuario = new ClaimsPrincipal(identidade);

                return Results.SignIn(usuario);
            });

            app.MapGet("/sessao", [Authorize] (ClaimsPrincipal usuario) =>
            {
                if (usuario == null)
                    return Results.Problem("Usuário não autenticado.");

                var nome = usuario.FindFirst(ClaimTypes.Name)?.Value;
                var email = usuario.FindFirst(ClaimTypes.Email)?.Value;
                var role = usuario.FindFirst(ClaimTypes.Role)?.Value;

                return Results.Ok(new
                {
                    Nome = nome,
                    Email = email,
                    Role = role
                });
            });
        }
    }
}
