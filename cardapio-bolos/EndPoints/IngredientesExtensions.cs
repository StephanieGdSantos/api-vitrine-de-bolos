using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardapioBolos.EndPoints
{
    public static class IngredientesExtensions
    {
        public static void AddEndpointsIngredientes(this WebApplication app)
        {
            app.MapGet("/ingredientes", ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingredientes = dal.Listar();
                if (!ingredientes.Any())
                    return Results.NoContent();

                return Results.Ok(ingredientes);
            });

            app.MapGet("/ingredientes/{id}", ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingrediente = dal.BuscarPorId(id);
                if (ingrediente == null)
                    return Results.NotFound();

                return Results.Ok(ingrediente);
            });

            app.MapPost("/ingredientes", ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, [FromBody] string ingrediente) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var novoIngrediente = new Ingrediente(ingrediente);

                dal.Adicionar(novoIngrediente);
                return Results.Ok();
            });

            app.MapPatch("/ingrediente/{id}", ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, [FromBody] Ingrediente ingrediente) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingredienteExistente = dal.BuscarPorId(ingrediente.Id);
                if (ingredienteExistente == null)
                    return Results.NotFound();

                dal.Editar(ingrediente);
                return Results.Ok();
            });

            app.MapDelete("/ingredientes/{id}", ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingrediente = dal.BuscarPorId(id);
                if (ingrediente == null)
                    return Results.NotFound();

                dal.Excluir(ingrediente);
                return Results.Ok();
            });
        }
    }
}
