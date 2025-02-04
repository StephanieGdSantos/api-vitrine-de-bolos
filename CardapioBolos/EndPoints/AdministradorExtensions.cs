using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
using CardapioBolos.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
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
            })
            .WithTags("Administrador")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Adiciona um administrador", description: "Adiciona um novo administrador ao sistema."));

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
            })
            .WithTags("Administrador")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Realiza login como administrador", description: "Realiza login no sistema como administrador."));

            app.MapPatch("/admin/{id}", async ([FromServices] DAL<Administrador> dal, [FromServices] CardapioBolosContext context, [FromBody] Administrador administrador) =>
            {
                var admin = await dal.BuscarPorId(administrador.Id);
                if (admin == null)
                    return Results.NotFound();

                var novoAdmin = new Administrador(administrador.Nome, administrador.Telefone, administrador.Email, administrador.Senha);

                await dal.Editar(novoAdmin);
                return Results.Ok();
            })
            .WithTags("Administrador")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Atualiza administrador", description: "Atualiza dados de um administrador já cadastrado."));


            app.MapDelete("admin/{id}", async ([FromServices] DAL<Administrador> dal, [FromServices] CardapioBolosContext context, int id) =>
            {
                var admin = await dal.BuscarPorId(id);
                if (admin == null)
                    return Results.NotFound();

                dal.Excluir(admin);
                return Results.Ok();
            })
            .WithTags("Administrador")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Exclui um administrador", description: "Exclui a conta de um administrador."));

        }
    }
}
