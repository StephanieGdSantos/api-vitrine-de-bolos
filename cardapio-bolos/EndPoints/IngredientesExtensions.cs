using CardapioBolos.Banco;
using CardapioBolos.Model;
using Microsoft.AspNetCore.Mvc;

namespace CardapioBolos.EndPoints;

public static class IngredientesExtensions
{
    public static void AddEndpointsIngredientes(this WebApplication app)
    {
        app.MapGet("/ingredientes", ([FromServices] DAL<Ingrediente> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/ingredientes/{nome}", ([FromServices] DAL<Ingrediente> dal, string nome) =>
        {
            var ingrediente = dal.Buscar(ingrediente => ingrediente.Nome.Equals(nome));
            if (ingrediente == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(ingrediente);
        });

        app.MapPost("/ingredientes", ([FromServices] DAL<Ingrediente> dal, [FromBody] Ingrediente ingrediente) =>
        {
            var novoIngrediente = new Ingrediente()
            {
                Nome = ingrediente.Nome
            };
            dal.Adicionar(novoIngrediente);
            return Results.Ok();
        });

        app.MapPut("/ingredientes/", ([FromServices] DAL<Ingrediente> dal, [FromBody] Ingrediente ingrediente) =>
        {
            var ingredienteParaAtualizar = dal.Buscar(ingredientes => ingredientes.Id.Equals(ingrediente.Id));
            if (ingredienteParaAtualizar == null)
            {
                return Results.NotFound();
            }

            dal.Editar(ingredienteParaAtualizar);
            return Results.Ok();
        });

        app.MapDelete("/ingredientes/{id}", ([FromServices] DAL<Ingrediente> dal, int id) =>
        {
            var ingrediente = dal.Buscar(ingrediente => ingrediente.Id.Equals(id));
            if (ingrediente == null)
            {
                return Results.NotFound();
            }

            dal.Excluir(ingrediente);
            return Results.NoContent();
        });

    }
}
