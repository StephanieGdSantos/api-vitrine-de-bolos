using CardapioBolos.Banco;
using CardapioBolos.DTO;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
using CardapioBolos.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace CardapioBolos.EndPoints;

public static class BolosExtensions
{
    public static void AddEndpointsBolos(this WebApplication app)
    {
        app.MapGet("/bolos", ([FromServices] DAL<Bolo> dal) =>
        {
            var bolos = dal.Listar();

            if (!bolos.Any())
                return Results.NoContent();

            return Results.Ok(bolos.OrderBy(bolo => bolo.Nome));
        });

        app.MapGet("/bolos/id={id}", ([FromServices] DAL<Bolo> dal, int id) =>
        {
            var boloSelecionado = dal.BuscarPorId(id);
            if (boloSelecionado == null)
                return Results.NotFound();

            return Results.Ok(boloSelecionado);
        });

        app.MapGet("/bolos/{nome}", ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, string nome) =>
        {
            var nomeProcurado = new BoloServices(context).FormatarNomeParaBusca(nome);
            var bolosExistentes = dal.Listar().Select(bolo => bolo.Nome).ToArray();
            var bolosEncontrados = Buscador.BuscarNomesSemelhantes(bolosExistentes, nomeProcurado);

            var bolos = dal.Listar().Where(bolo => bolosEncontrados.Contains(bolo.Nome.ToLower()));
            if (!bolos.Any())
                return Results.NotFound();

            return Results.Ok(bolos);
        });

        app.MapPost("/bolos", ([FromServices] DAL<Bolo> boloDal, [FromServices] DAL<Ingrediente> ingredienteDal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, [FromBody] BoloRequest bolo) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
                return Results.Unauthorized();

            var nomesIngredientesInseridos = bolo.Ingredientes.Select(ingrediente => ingrediente.Nome);

            var ingredientesDoBolo = ingredienteDal.Listar()
                .Where(ingrediente => nomesIngredientesInseridos.Contains(ingrediente.Nome))
                .ToList();

            var novoBolo = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, ingredientesDoBolo, bolo.Preco);
            boloDal.Adicionar(novoBolo);
            return Results.Ok();
        });

        app.MapPatch("/bolos", ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario,  [FromBody] BoloRequestEdit bolo) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
                return Results.Unauthorized();

            var boloAAtualizar = dal.BuscarPorId(bolo.Id);
            if (boloAAtualizar == null)
                return Results.NotFound();

            var nomeIngredientesInseridos = bolo.Ingredientes
            .Select(ingrediente => ingrediente.Nome);

            var ingredientesDoBolo = dal.Listar()
                .Where(ingrediente => nomeIngredientesInseridos.Contains(ingrediente.Nome))
                .ToList();

            var boloAtualizado = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, bolo.Ingredientes, bolo.Preco)
            {
                Id = bolo.Id
            };
            dal.Editar(boloAtualizado);
            return Results.Ok();
        });

        app.MapDelete("/bolos/{id}", ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, int id) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
                return Results.Unauthorized();

            var boloAExcluir = dal.BuscarPorId(id);
            if (boloAExcluir == null)
                return Results.NotFound();

            dal.Excluir(boloAExcluir);
            return Results.NoContent();
        });
    }
}