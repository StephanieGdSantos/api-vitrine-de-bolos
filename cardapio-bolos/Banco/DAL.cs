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

    public virtual void Adicionar(T objeto)
    {
        context.Set<T>().Add(objeto);
        context.SaveChanges();
    }

    public virtual T? Buscar(Func<T, bool> condicao)
    {
        return context.Set<T>().FirstOrDefault(condicao);
    }

    public virtual T? BuscarPorId(int id)
    {
        return context.Set<T>().Find(id);
    }

    public virtual IEnumerable<T> Listar()
    {
        return context.Set<T>().ToList();
    }

    public virtual void Excluir(T objeto)
    {
        context.Set<T>().Remove(objeto);
        context.SaveChanges();
    }

    public virtual async Task Editar(T objeto)
    {
        var chaveDaPropriedade = typeof(T).GetProperty("Id");

        if (chaveDaPropriedade == null)
            throw new InvalidOperationException("A entidade não possui uma propriedade 'Id'.");

        var chave = chaveDaPropriedade.GetValue(objeto);
        /*var entidadeExiste = await context.Set<T>().FindAsync(chave);

        if (entidadeExiste != null)
            context.Entry(entidadeExiste).State = EntityState.Detached;*/

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
        else
        {
            context.Set<T>().Update(objeto);
        }

        await context.SaveChangesAsync();
    }
}
