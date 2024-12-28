using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CardapioBolos.EndPoints
{
    public static class EncomendasExtensions
    {
        public static void AddEndpointsEncomendas(this WebApplication app)
        {
            app.MapGet("/encomendas", ([FromServices] DAL<Encomenda> dal) =>
            {
                return Results.Ok(dal.Listar());
            });

            app.MapGet("/encomendas/{id}", ([FromServices] DAL<Encomenda> dal, int id) =>
            {
                var encomenda = dal.Buscar(encomenda => encomenda.Id.Equals(id));
                if (encomenda == null)
                {
                    return Results.NotFound();
                }

                return Results.Ok(encomenda);
            });

            app.MapPost("/encomendas", ([FromServices] DAL<Encomenda> dalEncomenda, [FromServices] DAL<Bolo> dalBolos, [FromBody] EncomendaRequest encomenda) =>
            {
                var bolosId = encomenda.Bolos.Select(b => b.Id).ToList();
                var bolosExistentes = dalBolos.Listar().Where(b => bolosId.Contains(b.Id)).ToList();
                var todosOsBolosExistem = bolosExistentes.Count() == bolosId.Count;

                if (!todosOsBolosExistem)
                {
                    return Results.Problem("Houve uma falha com os bolos escolhidos.");
                }

                bolosExistentes.ForEach(bolo => bolo.Peso = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Peso);
                bolosExistentes.ForEach(bolo => bolo.Formato = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Formato);
                bolosExistentes.ForEach(bolo => bolo.Observacao = encomenda.Bolos.First(b => b.Id.Equals(bolo.Id)).Observacao);

                var valorFinal = bolosExistentes.Sum(bolo => bolo.Preco * bolo.Peso);

                var novaEncomenda = new Encomenda(encomenda.DataDoPedido, encomenda.NomeCliente, encomenda.TelefoneCliente, bolosExistentes, valorFinal, encomenda.DataDaEntrega);

                dalEncomenda.Adicionar(novaEncomenda);
                return Results.Ok();
            });

            app.MapPatch("/encomendas", ([FromServices] DAL<Encomenda> dal, [FromBody] EncomendaRequestEdit encomenda) =>
            {
                var encomendaExistente = dal.Buscar(encomenda => encomenda.Id.Equals(encomenda.Id));
                if (encomendaExistente == null)
                {
                    return Results.NotFound();
                }

                var novaEncomenda = new Encomenda()
                {
                    TelefoneCliente = encomenda.TelefoneCliente,
                    Bolos = encomenda.Bolos,
                    ValorFinal = encomenda.ValorFinal,
                    Finalizado = encomenda.Finalizado,
                    DataDaEntrega = encomenda.DataDaEntrega
                };

                dal.Editar(novaEncomenda);
                return Results.Ok();
            });
        }
    }
}
