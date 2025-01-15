using CardapioBolos.Banco;
using CardapioBolos.DTO;
using CardapioBolos.Model;
using CardapioBolos.Requests;
using CardapioBolos.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CardapioBolos.EndPoints
{
    public static class EncomendasExtensions
    {
        public static void AddEndpointsEncomendas(this WebApplication app)
        {
            app.MapGet("/encomendas", ([FromServices] DAL<Encomenda> dal, ClaimsPrincipal usuario, [FromServices] CardapioBolosContext context) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var encomendas = dal.Listar();
                if (!encomendas.Any())
                    return Results.NoContent();

                return Results.Ok(encomendas.OrderBy(encomenda => encomenda.DataDaEntrega));
            });

            app.MapGet("/encomendas/{id}", ([FromServices] DAL<EncomendaDTO> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var encomenda = dal.BuscarPorId(id);
                if (encomenda == null)
                    return Results.NotFound();

                return Results.Ok(encomenda);
            });

            app.MapPost("/encomendas", ([FromServices] DAL<Encomenda> dalEncomenda, [FromServices] DAL<Bolo> bolosDAL, [FromServices] CardapioBolosContext context, [FromBody] EncomendaRequest encomenda) =>
            {
                var novaEncomenda = new EncomendaServices(context).ObterEncomenda(encomenda);
                if (novaEncomenda == null)
                    return Results.Problem("Erro ao criar encomenda.");

                dalEncomenda.Adicionar(novaEncomenda);
                return Results.Ok();
            });

            app.MapPatch("/encomendas", ([FromServices] DAL<Encomenda> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, [FromBody] EncomendaRequestEdit encomenda) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var encomendaExistente = dal.BuscarPorId(encomenda.Id);
                if (encomendaExistente == null)
                    return Results.NotFound();

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

            app.MapDelete("/encomendas/{id}", ([FromServices] DAL<Encomenda> dal, [FromServices] CardapioBolosContext context, ClaimsPrincipal usuario, int id) =>
            {
                var usuarioEhAdmin = new AdministradorServices(context).ValidaSeEhAdministrador(usuario);
                if (!usuarioEhAdmin)
                    return Results.Unauthorized();

                var encomendaExistente = dal.BuscarPorId(id);
                if (encomendaExistente == null)
                    return Results.NotFound();

                dal.Excluir(encomendaExistente);
                return Results.Ok();
            });
        }
    }
}