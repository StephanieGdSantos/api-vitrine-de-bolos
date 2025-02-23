using CardapioBolos.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace CardapioBolos.Banco;

public class DAL<T> where T : class
{
    private readonly CardapioBolosContext context;

    public DAL(CardapioBolosContext context)
    {
        this.context = context;
    }

    public virtual async Task Adicionar(T objeto)
    {
        context.Set<T>().Add(objeto);
        await context.SaveChangesAsync();
    }

    public virtual async Task<T?> Buscar(Func<T, bool> condicao)
    {
        return await Task.Run(() => context.Set<T>().FirstOrDefault(condicao));
    }

    public virtual async Task<T?> BuscarPorId(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> Listar()
    {
        return await context.Set<T>().ToListAsync();
    }

    public virtual async void Excluir(T objeto)
    {
        context.Set<T>().Remove(objeto);
        await context.SaveChangesAsync();
    }

    public virtual async Task Editar(T objeto)
    {
        var chaveDaPropriedade = typeof(T).GetProperty("Id");

        if (chaveDaPropriedade == null)
            throw new InvalidOperationException("A entidade não possui uma propriedade 'Id'.");

        var chave = chaveDaPropriedade.GetValue(objeto);

        var entidadeExistente = await context.Set<T>().FindAsync(chave);
        if (entidadeExistente != null)
        {
            context.Entry(entidadeExistente).State = EntityState.Detached;
        }

        if (objeto is Bolo bolo)
        {
            var ingredientesExistentesNoBolo = context.Set<BoloIngrediente>().Where(bi => bi.BoloId == bolo.Id).ToList();
            var novosIngredientesDoBolo = bolo.Ingredientes.Select(ingrediente => new BoloIngrediente { BoloId = bolo.Id, IngredienteId = ingrediente.Id }).ToList();
            var ingredientesQueIraoSairDoBolo = ingredientesExistentesNoBolo.Except(novosIngredientesDoBolo).ToList();

            if (ingredientesQueIraoSairDoBolo.Any())
                context.Set<BoloIngrediente>().RemoveRange(ingredientesQueIraoSairDoBolo);

            context.Set<BoloIngrediente>().AddRange(novosIngredientesDoBolo);
        }
        else if (objeto is Encomenda encomenda)
        {
            var bolosExistentesNaEncomenda = context.Set<BoloEncomenda>().Where(be => be.EncomendaId == encomenda.Id).ToList();
            var novosBolosDaEncomenda = encomenda.Bolos.Select(bolo => new BoloEncomenda { BoloId = bolo.Id, EncomendaId = encomenda.Id }).ToList();
            var bolosQueIraoSairDaEncomenda = bolosExistentesNaEncomenda.Except(novosBolosDaEncomenda).ToList();
            if (bolosQueIraoSairDaEncomenda.Any())
                context.Set<BoloEncomenda>().RemoveRange(bolosQueIraoSairDaEncomenda);

            context.Set<BoloEncomenda>().AddRange(novosBolosDaEncomenda);
        }

        context.Set<T>().Update(objeto);
        await context.SaveChangesAsync();
    }
}
