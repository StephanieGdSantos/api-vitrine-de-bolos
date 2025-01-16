using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;
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
                administrador.Telefone = administradorServices.FormatarTelefone(administrador.Telefone);
                var validezDoUsuario = administradorServices.ValidarDadosDoAdministrador(administrador);
                if (validezDoUsuario != Results.Ok())
                    return validezDoUsuario;

                var hashSenha = VerificadorDeSenha.GerarHashSenha(administrador.Senha);
                var novoAdministrador = new Administrador(administrador.Nome, administrador.Telefone, administrador.Email, hashSenha);

                dal.Adicionar(novoAdministrador);
                return Results.Ok();
            });

            app.MapPost("/admin/login", ([FromServices] AdministradorServices administradorServices, [FromBody] AdministradorRequest administrador) =>
            {
                var senhaValida = administradorServices.Autenticar(administrador.Email, administrador.Senha);
                if (!senhaValida)
                    return Results.Problem("Dados inválidos.");

                var claims = new List<Claim>
                {
                        new(ClaimTypes.Email, administrador.Email),
                        new(ClaimTypes.Role, "admin")
                };
                var identidade = new ClaimsIdentity(claims, "login");
                var usuario = new ClaimsPrincipal(identidade);

                return Results.SignIn(usuario);
            });

            app.MapPatch("/admin/{id}", ([FromServices] DAL<Administrador> dal, [FromServices] CardapioBolosContext context, [FromBody] Administrador administrador) =>
            {
                var admin = dal.BuscarPorId(administrador.Id);
                if (admin == null)
                    return Results.NotFound();

                var novoAdmin = new Administrador(administrador.Nome, administrador.Telefone, administrador.Email, administrador.Senha);

                dal.Editar(novoAdmin);
                return Results.Ok();
            });

            app.MapDelete("admin/{id}", ([FromServices] DAL<Administrador> dal, [FromServices] CardapioBolosContext context, int id) =>
            {
                var admin = dal.BuscarPorId(id);
                if (admin == null)
                    return Results.NotFound();

                dal.Excluir(admin);
                return Results.Ok();
            });
        }
    }
}
