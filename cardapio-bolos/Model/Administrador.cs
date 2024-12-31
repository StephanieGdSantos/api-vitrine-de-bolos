using System.Text.Json.Serialization;

namespace CardapioBolos.Model
{
    public class Administrador
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        [JsonIgnore]
        public virtual ICollection<Bolo>? Bolos { get; set; }

        public Administrador()
        {
            Bolos = new List<Bolo>();
        }
        public Administrador(string nome, string telefone, string email, string senha)
        {
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Senha = senha;
            Bolos = new List<Bolo>();
        }
    }
}