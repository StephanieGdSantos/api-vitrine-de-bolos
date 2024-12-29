using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
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

            app.MapPost("/encomendas", ([FromServices] DAL<Encomenda> dalEncomenda, [FromServices] DAL<Bolo> bolosDAL, [FromServices] CardapioBolosContext context, [FromBody] EncomendaRequest encomenda) =>
            {
                var novaEncomenda = new EncomendaServices(context).ObterEncomenda(encomenda);

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
