using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Utils;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace CardapioBolos.Services
{
    public class AdministradorServices
    {
        private readonly DAL<Administrador> _administradorDAL;
        private readonly CardapioBolosContext _context;

        public AdministradorServices(CardapioBolosContext context)
        {
            _administradorDAL = new DAL<Administrador>(context);
            _context = context;
        }
        public bool Autenticar(string email, string senha)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
                return false;

            var administrador = _administradorDAL.Buscar(admin => admin.Email.Equals(email));
            if (administrador == null)
                return false;

            return VerificadorDeSenha.VerificarSenha(senha, administrador.Senha);
        }

        public bool VerificarSeEmailJaEhUsado(string email)
        {
            var administrador = _administradorDAL.Buscar(admin => admin.Email.Equals(email));

            if (administrador == null)
                return false;
            return true;
        }

        public bool ValidaSeEhAdministrador(ClaimsPrincipal usuario)
        {
            var regra = usuario.FindFirst(ClaimTypes.Role);
            if (regra == null || regra.Value != "admin")
                return false;

            return true;
        }

        public IResult ValidarDadosDoAdministrador(Administrador administrador)
        {
            var emailEhUsado = VerificarSeEmailJaEhUsado(administrador.Email);
            if (emailEhUsado)
                return Results.Problem("O e-mail já está cadastrado.");

            if (administrador.Email == null || !new EmailAddressAttribute().IsValid(administrador.Email))
                return Results.Problem("O e-mail inserido é inválido.");

            if (administrador.Telefone.Length != 11 || !new PhoneAttribute().IsValid(administrador.Telefone))
                return Results.Problem("O telefone inserido é inválido.");

            return Results.Ok();
        }

        public string FormatarTelefone(string telefone)
        {
            var telefoneSemCaracteres = telefone.Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");
            return telefoneSemCaracteres;
        }
    }
}
