using CardapioBolos.Banco;
using CardapioBolos.DTO;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
using CardapioBolos.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Security.Claims;

namespace CardapioBolos.EndPoints;

public static class BolosExtensions
{
    public static void AddEndpointsBolos(this WebApplication app)
    {
        app.MapGet("/bolos", async ([FromServices] DAL<Bolo> dal) =>
        {
            var bolos = await dal.Listar();

            if (!bolos.Any())
                return Results.NoContent();

            return Results.Ok(bolos.OrderBy(bolo => bolo.Nome));
        })
        .WithTags("Bolos")
        .WithMetadata(new SwaggerOperationAttribute(summary: "Lista todos os bolos", description: "Lista todos os bolos cadastrados."));

        app.MapGet("/bolos/{id}", async ([FromServices] DAL<Bolo> dal, int id) =>
        {
            var bolos = await dal.Listar();
            var boloSelecionado = bolos
                .Where(b => b.Id == id)
                .Select(b => new BoloDTO() { Id = id, Nome = b.Nome, Descricao = b.Descricao, Imagem = b.Imagem, Peso = b.Peso, Preco = b.Preco, Ingredientes = b.Ingredientes })
                .FirstOrDefault();

            if (boloSelecionado == null)
                return Results.NoContent();

            return Results.Ok(boloSelecionado);
        })
        .WithTags("Bolos")
        .WithMetadata(new SwaggerOperationAttribute(summary: "Busca um bolo pelo id", description: "Busca um bolo cadastrado pelo id."));

        app.MapGet("/bolos/nome={nome}", async ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, string nome) =>
        {
            var nomeProcurado = new BoloServices(context).FormatarNomeParaBusca(nome);
            var bolosExistentes = await dal.Listar();
            var nomeBolosExistentes = bolosExistentes
                .Select(bolo => bolo.Nome).ToArray();

            var bolosEncontrados = Buscador.BuscarNomesSemelhantes(nomeBolosExistentes, nomeProcurado);

            var bolos = await dal.Listar();
            var bolosSelecionados = bolos
                .Where(bolo => bolosEncontrados
                    .Contains(bolo.Nome
                    .ToLower()))
                .Select(b => new BoloDTO() { Id = b.Id, Nome = b.Nome, Descricao = b.Descricao, Imagem = b.Imagem, Peso = b.Peso, Preco = b.Preco, Ingredientes = b.Ingredientes });

            if (!bolosSelecionados.Any())
                return Results.NoContent();

            return Results.Ok(bolosSelecionados);
        })
        .WithTags("Bolos")
        .WithMetadata(new SwaggerOperationAttribute(summary: "Busca um bolo pelo nome", description: "Busca um bolo cadastrado pelo nome."));

        app.MapPost("/bolos", async ([FromServices] DAL<Bolo> boloDal, [FromServices] DAL<Ingrediente> ingredienteDal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, [FromBody] BoloRequest bolo) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
                return Results.Unauthorized();

            var nomesIngredientesInseridos = bolo.Ingredientes.Select(ingrediente => ingrediente.Nome).ToList();

            var ingredientesDoBolo = await ingredienteDal.Listar();
            var ingredientesInseridos = ingredientesDoBolo
                .Where(ingrediente => nomesIngredientesInseridos.Contains(ingrediente.Nome))
                .ToList();
            
            var erroNosIngredientes = new BoloServices(context).VerificarIngredientesNaoEncontrados(nomesIngredientesInseridos, ingredientesInseridos);
            if (erroNosIngredientes != "")
                return Results.Problem(erroNosIngredientes);

            var novoBolo = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, ingredientesInseridos, bolo.Preco);
            boloDal.Adicionar(novoBolo);
            return Results.Ok();
        })
        .WithTags("Bolos")
        .WithMetadata(new SwaggerOperationAttribute(summary: "Adiciona um bolo", description: "Adiciona um novo bolo no banco de dados."));

        app.MapPatch("/bolos/{id}", async ([FromServices] DAL<Bolo> dal, [FromServices] DAL<Ingrediente> ingredientesDAL, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario,  [FromBody] BoloRequestEdit bolo, int id) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
                return Results.Unauthorized();

            var boloAAtualizar = dal.BuscarPorId(id);

            if (boloAAtualizar == null)
                return Results.NoContent();

            var nomeIngredientesInseridos = bolo.Ingredientes
                .Select(ingrediente => ingrediente.Nome)
                .ToList();

            var ingredientesDoBolo = await ingredientesDAL.Listar();
            var ingredientesAtualizados = ingredientesDoBolo
                .Where(ingrediente => nomeIngredientesInseridos
                .Contains(ingrediente.Nome))
                .ToList();

            var erroNosIngredientes = new BoloServices(context).VerificarIngredientesNaoEncontrados(nomeIngredientesInseridos, ingredientesAtualizados);
            if (erroNosIngredientes != "")
                return Results.Problem(erroNosIngredientes);

            var boloAtualizado = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, ingredientesAtualizados, bolo.Preco, 1)
            {
                Id = id,
            };
            await dal.Editar(boloAtualizado);
            return Results.Ok();
        })
        .WithTags("Bolos")
        .WithMetadata(new SwaggerOperationAttribute(summary: "Atualiza um bolo", description: "Atualiza os dados de um bolo."));

        app.MapDelete("/bolos/{id}", async ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, int id) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
                return Results.Unauthorized();

            var boloAExcluir = await dal.BuscarPorId(id);
            if (boloAExcluir == null)
                return Results.NotFound();

            dal.Excluir(boloAExcluir);
            return Results.NoContent();
        })
        .WithTags("Bolos")
        .WithMetadata(new SwaggerOperationAttribute(summary: "Exclui bolo", description: "Exclui um bolo."));
    }

    
}