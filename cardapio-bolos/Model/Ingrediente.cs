namespace CardapioBolos.Model
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Bolo> Bolos { get; set; }
    }
}
