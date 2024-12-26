namespace CardapioBolos.Model
{
    public class Bolo
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public string ListaIngredientes { get; set; }
        public double Preco { get; set; }

        public Bolo() { }
        public Bolo(string nome, string descricao, string listaDeIngredientes, string imagem, double preco)
        {
            Nome = nome;
            Imagem = imagem;
            Descricao = descricao;
            ListaIngredientes = listaDeIngredientes;
            Preco = preco;
        }
    }
}
