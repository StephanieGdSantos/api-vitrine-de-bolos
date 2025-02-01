using System.Text.Json.Serialization;

namespace CardapioBolos.Model
{
    public class Ingrediente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [JsonIgnore]
        public virtual List<Bolo> Bolos { get; set; }
        public Ingrediente() { }
        public Ingrediente(string nome)
        {
            Nome = nome;
        }
    }
}