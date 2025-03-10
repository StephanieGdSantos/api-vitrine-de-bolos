﻿using CardapioBolos.Banco;
using CardapioBolos.DTO;
using CardapioBolos.Model;
using CardapioBolos.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace CardapioBolos.EndPoints
{
    public static class IngredientesExtensions
    {
        public static void AddEndpointsIngredientes(this WebApplication app)
        {
            app.MapGet("/ingredientes", async ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingredientes = await dal.Listar();
                if (!ingredientes.Any())
                    return Results.NoContent();

                return Results.Ok(ingredientes);
            })
            .WithTags("Ingredientes")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Lista ingredientes", description: "Lista todos os ingredientes cadastrados."));

            app.MapGet("/ingredientes/{id}", async ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingredientesJaCadastrados = await dal.Listar();
                var ingrediente = ingredientesJaCadastrados
                    .Where(i => i.Id == id)
                    .Select(i => new IngredienteDTO { Nome = i.Nome })
                    .FirstOrDefault();

                if (ingrediente == null)
                    return Results.NoContent();

                return Results.Ok(ingrediente);
            })
            .WithTags("Ingredientes")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Busca ingrediente pelo id", description: "Busca um ingrediente pelo id."));

            app.MapPost("/ingredientes", async ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, [FromBody] List<string> listaIngredientes) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                foreach (var ingrediente in listaIngredientes)
                {
                    if (dal.Buscar(ingr => ingr.Nome.ToLower().Equals(ingrediente.ToLower())).Result != null)
                        return Results.Problem("O ingrediente já está cadastrado.");
                }

                var listaNovosIngredientes = new List<Ingrediente>();
                foreach (var ingrediente in listaIngredientes)
                    listaNovosIngredientes.Add(new Ingrediente(ingrediente));

                foreach (var ingrediente in listaNovosIngredientes)
                    await dal.Adicionar(ingrediente);

                return Results.Ok();
            })
            .WithTags("Ingredientes")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Adiciona ingredientes", description: "Adiciona novos ingredientes ao banco de dados."));

            app.MapPatch("/ingredientes/{id}", async ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, [FromBody] string nomeIngrediente, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingredienteExistente = await dal.BuscarPorId(id);
                if (ingredienteExistente == null)
                    return Results.NotFound();

                ingredienteExistente.Nome = nomeIngrediente;

                await dal.Editar(ingredienteExistente);
                return Results.Ok();
            })
            .WithTags("Ingredientes")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Atualiza um ingrediente", description: "Atualiza o nome de um ingrediente"));

            app.MapDelete("/ingredientes/{id}", async ([FromServices] DAL<Ingrediente> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var ingrediente = await dal.BuscarPorId(id);
                if (ingrediente == null)
                    return Results.NotFound();

                dal.Excluir(ingrediente);
                return Results.Ok();
            })
            .WithTags("Ingredientes")
            .WithMetadata(new SwaggerOperationAttribute(summary: "Exclui ingrediente", description: "Exclui um ingrediente."));
        }
    }
}
