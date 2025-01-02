

namespace CardapioBolos.DTO
{
    public class EncomendaDTO
    {
        public int Id { get; set; }
        public DateTime DataDoPedido { get; set; }
        public string NomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
        public double ValorFinal { get; set; }
        public bool Finalizado { get; set; }
        public DateTime DataDaEntrega { get; set; }
        public virtual List<BoloDTO> Bolos { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string? Complemento { get; set; }
    }
}
