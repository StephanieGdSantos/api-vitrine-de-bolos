using System.Text.Json.Serialization;

namespace CardapioBolos.Model
{
    public class Bolo
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public string ListaIngredientes { get; set; }
        public double Preco { get; set; }
        public bool? Topper { get; set; }
        public bool? PapelDeArroz { get; set; }
        public bool? Presente { get; set; }
        public string? Observacao { get; set; }
        public string? Formato { get; set; }
        public double Peso { get; set; }
        [JsonIgnore]
        public int? AdministradorId { get; set; }
        [JsonIgnore]
        public virtual Administrador Administrador { get; set; }
        [JsonIgnore]
        public virtual List<Encomenda> Encomendas { get; set; }

        public Bolo() { }
        public Bolo(string nome, string imagem, string descricao, string listaDeIngredientes, double preco, double? peso = 1)
        {
            Nome = nome;
            Imagem = imagem;
            Descricao = descricao;
            ListaIngredientes = listaDeIngredientes;
            Preco = preco;
            Peso = peso ?? 1;
        }
    }
}
