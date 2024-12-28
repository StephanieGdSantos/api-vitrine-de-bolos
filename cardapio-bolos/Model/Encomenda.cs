using System.Text.Json.Serialization;

namespace CardapioBolos.Model
{
    public class Encomenda
    {
        public int Id { get; set; }
        public DateTime DataDoPedido { get; set; }
        public string NomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public double ValorFinal { get; set; }
        public bool Finalizado { get; set; }
        public DateTime DataDaEntrega { get; set; }
        public virtual List<Bolo> Bolos { get; set; }

        public Encomenda() { }
        public Encomenda(DateTime dataDoPedido, string nomeCliente, string telefoneCliente, List<Bolo> bolos, double valorFinal, DateTime dataDaEntrega)
        {
            DataDoPedido = dataDoPedido;
            NomeCliente = nomeCliente;
            TelefoneCliente = telefoneCliente;
            Bolos = bolos;
            ValorFinal = valorFinal;
            DataDaEntrega = dataDaEntrega;
        }
    }
}
