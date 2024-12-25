namespace CardapioBolos.Model
{
    public class Bolo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public virtual ICollection<Ingrediente> ListaIngredientes { get; set; }
        public double Preco { get; set; }
    }
}
