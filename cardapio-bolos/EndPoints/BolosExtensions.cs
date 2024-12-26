using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CardapioBolos.EndPoints;

public static class BolosExtensions
{
    public static void AddEndpointsBolos(this WebApplication app)
    {
        app.MapGet("/bolos", ([FromServices] DAL<Bolo> dal) =>
        {
            return Results.Ok(dal.Listar());
        });

        app.MapGet("/bolos/id={id}", ([FromServices] DAL<Bolo> dal, string id) =>
        {
            var bolo = dal.Buscar(bolo => bolo.Id.Equals(id));
            if (bolo == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(bolo);
        });

        app.MapGet("/bolos/{nome}", ([FromServices] DAL<Bolo> dal, string nome) =>
        {
            var bolo = dal.Buscar(bolo => bolo.Nome.Equals(nome));
            if (bolo == null)
            {
                return Results.NotFound();
            }

            return Results.Ok(bolo);
        });

        app.MapPost("/bolos", ([FromServices] DAL<Bolo> dal, [FromBody] BoloRequest bolo) =>
        {
            var novoBolo = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, bolo.ListaDeIngredientes, bolo.Preco);
            dal.Adicionar(novoBolo);
            return Results.Ok();
        });

        app.MapPut("/bolos", ([FromServices] DAL<Bolo> dal, [FromBody] BoloRequestEdit bolo) =>
        {
            var boloAAtualizar = dal.Buscar(bolos => bolos.Id.Equals(bolo.Id));
            if (boloAAtualizar == null)
            {
                return Results.NotFound();
            }

            var boloAtualizado = new Bolo(bolo.Nome, bolo.Imagem, bolo.Descricao, bolo.ListaDeIngredientes, bolo.Preco);
            dal.Editar(boloAtualizado);
            return Results.Ok();
        });

        app.MapDelete("/bolo/{id}", ([FromServices] DAL<Bolo> dal, int id) =>
        {
            var boloAExcluir = dal.Buscar(bolos => bolos.Id.Equals(id));
            if (boloAExcluir == null)
            {
                return Results.NotFound();
            }

            dal.Excluir(boloAExcluir);
            return Results.NoContent();
        });
    }
}
