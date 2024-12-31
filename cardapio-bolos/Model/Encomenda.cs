using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CardapioBolos.Model
{
    public class Encomenda
    {
        [JsonIgnore]
        public int Id { get; set; }
        public DateTime DataDoPedido { get; set; }
        public string NomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public double ValorFinal { get; set; }
        public bool Finalizado { get; set; }
        public DateTime DataDaEntrega { get; set; }
        public virtual List<Bolo> Bolos { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }

        public Encomenda() { }
        public Encomenda(DateTime dataDoPedido, string nomeCliente, string telefoneCliente, List<Bolo> bolos, double valorFinal, DateTime dataDaEntrega, string cidade, string bairro, string logradouro, string numero, string? complemento = null)
        {
            DataDoPedido = dataDoPedido;
            NomeCliente = nomeCliente;
            TelefoneCliente = telefoneCliente;
            Bolos = bolos;
            ValorFinal = valorFinal;
            DataDaEntrega = dataDaEntrega;
            Cidade = cidade;
            Bairro = bairro;
            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
        }
    }
}
