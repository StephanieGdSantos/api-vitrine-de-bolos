using CardapioBolos.Banco;
using CardapioBolos.DTO;
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

            app.MapGet("/ingredientes/{id}", async ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingrediente = await Task.Run(() => dal.Listar()
                    .Where(i => i.Id == id)
                    .Select(i => new IngredienteDTO { Nome = i.Nome })
                    .FirstOrDefault());

                if (ingrediente == null)
                    return Results.NoContent();

                return Results.Ok(ingrediente);
            });

            app.MapPost("/ingredientes", ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, [FromBody] List<string> listaIngredientes) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                foreach (var ingrediente in listaIngredientes)
                {
                    if (dal.Buscar(ingr => ingr.Nome.Equals(ingrediente)) != null)
                        return Results.Problem("O ingrediente já está cadastrado.");
                }

                var listaNovosIngredientes = new List<Ingrediente>();
                foreach (var ingrediente in listaIngredientes)
                    listaNovosIngredientes.Add(new Ingrediente(ingrediente));

                foreach (var ingrediente in listaNovosIngredientes)
                    dal.Adicionar(ingrediente);

                return Results.Ok();
            });

            app.MapPatch("/ingredientes/{id}", async ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, [FromBody] string nomeIngrediente, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingredienteExistente = dal.BuscarPorId(id);
                if (ingredienteExistente == null)
                    return Results.NotFound();

                ingredienteExistente.Nome = nomeIngrediente;

                await dal.Editar(ingredienteExistente);
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
