namespace CardapioBolos.Banco;

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
        context.Set<T>().Update(objeto);
        context.SaveChanges();
    }
}
