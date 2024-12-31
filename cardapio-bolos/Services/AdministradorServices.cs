using CardapioBolos.Banco;
using CardapioBolos.Model;
using CardapioBolos.Utils;
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
    }
}
