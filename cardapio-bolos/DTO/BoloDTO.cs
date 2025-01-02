namespace CardapioBolos.DTO;

public class BoloDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Imagem { get; set; }
    public double Preco { get; set; }
    public bool? Topper { get; set; }
    public bool? PapelDeArroz { get; set; }
    public bool? Presente { get; set; }
    public string? Observacao { get; set; }
    public string? Formato { get; set; }
    public double Peso { get; set; }
}
