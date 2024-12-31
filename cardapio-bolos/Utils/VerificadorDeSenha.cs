using System.Security.Cryptography;
using System.Text;

namespace CardapioBolos.Utils
{
    public class VerificadorDeSenha
    {
        public static string GerarHashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool VerificarSenha(string senhaInserida, string hashArmazenado)
        {
            var hashDaSenhaInserida = GerarHashSenha(senhaInserida);
            return hashDaSenhaInserida == hashArmazenado;
        }
    }
}