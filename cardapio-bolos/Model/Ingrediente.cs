namespace CardapioBolos.Model
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public ICollection<Bolo> Bolos { get; set; }
    }
}
