using CardapioBolos.Model;
using Microsoft.EntityFrameworkCore;

﻿namespace CardapioBolos.Banco;

public class DAL<T> where T : class
{
    private readonly CardapioBolosContext context;

    public DAL(CardapioBolosContext context)
    {
        this.context = context;
    }

    public void Adicionar(T objeto)
    {
        context.Set<T>().Add(objeto);
        context.SaveChanges();
    }

    public T Buscar(Func<T, bool> condicao)
    {
        return context.Set<T>().FirstOrDefault(condicao);
    }

    public IEnumerable<T> Listar()
    {
        return context.Set<T>().ToList();
    }

    public void Excluir(T objeto)
    {
        context.Set<T>().Remove(objeto);
        context.SaveChanges();
    }

    public void Editar(T objeto)
    {
        var chaveDaPropriedade = typeof(T).GetProperty("Id");

        if (chaveDaPropriedade == null)
            throw new InvalidOperationException("A entidade não possui uma propriedade 'Id'.");

        var chave = chaveDaPropriedade.GetValue(objeto);
        var entidadeExiste = context.Set<T>().Find(chave);

        if (entidadeExiste != null)
            context.Entry(entidadeExiste).State = EntityState.Detached;

        context.Set<T>().Update(objeto);
        context.SaveChanges();
    }
}
