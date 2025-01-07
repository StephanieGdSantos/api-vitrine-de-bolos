using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
using Microsoft.AspNetCore.Mvc;
using subsequencia_mais_longa.Services;
using System.Linq;
using System.Security.Claims;

namespace CardapioBolos.EndPoints;

public static class BolosExtensions
{
    public static void AddEndpointsBolos(this WebApplication app)
    {
        app.MapGet("/bolos", ([FromServices] DAL<Bolo> dal) =>
        {
            return Results.Ok(dal.Listar().OrderBy(bolo => bolo.Nome));
        });

        app.MapGet("/bolos/id={id}", ([FromServices] DAL<Bolo> dal, int id) =>
        {
            var boloSelecionado = dal.BuscarPorId(id);
            if (boloSelecionado == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(boloSelecionado);
        });

        app.MapGet("/bolos/{nome}", ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, string nome) =>
        {
            var nomeProcurado = new BoloServices(context).FormatarNomeParaBusca(nome);
            var bolosExistentes = dal.Listar().Select(bolo => bolo.Nome).ToArray();
            var bolosEncontrados = new Buscador().BuscarNomesSemelhantes(bolosExistentes, nomeProcurado);

            var bolos = dal.Listar().Where(bolo => bolosEncontrados.Contains(bolo.Nome.ToLower()));
            if (!bolos.Any())
            {
                return Results.NotFound();
            }

            return Results.Ok(bolos);
        });

        app.MapPost("/bolos", ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, [FromBody] BoloRequest bolo) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
            {
                return Results.Unauthorized();
            }

            var novoBolo = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, bolo.ListaDeIngredientes, bolo.Preco);
            dal.Adicionar(novoBolo);
            return Results.Ok();
        });

        app.MapPatch("/bolos", ([FromServices] DAL<Bolo> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario,  [FromBody] BoloRequestEdit bolo) =>
        {
            var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
            if (!usuarioEhAdmin)
            {
                return Results.Unauthorized();
            }

            var boloAAtualizar = dal.BuscarPorId(bolo.Id);
            if (boloAAtualizar == null)
            {
                return Results.NotFound();
            }

            var boloAtualizado = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, bolo.ListaDeIngredientes, bolo.Preco)
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
            {
                return Results.Unauthorized();
            }

            var boloAExcluir = dal.BuscarPorId(id);
            if (boloAExcluir == null)
            {
                return Results.NotFound();
            }

            dal.Excluir(boloAExcluir);
            return Results.NoContent();
        });
    }
}