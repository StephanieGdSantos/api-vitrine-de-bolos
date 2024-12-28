namespace CadapioBolos.DTO;

public class BoloDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Imagem { get; set; }
    public string Descricao { get; set; }
    public string ListaIngredientes { get; set; }
    public double Preco { get; set; }
    public string? Observacao { get; set; }
    public string? Formato { get; set; }
    public double Peso { get; set; }
}
