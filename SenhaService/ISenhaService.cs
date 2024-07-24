using AuthenticationWithJwt.Models;

namespace AuthenticationWithJwt.SenhaService
{
    public interface ISenhaService
    {
        void CriarHashSenha(string senha, out byte[] senhaHash, out byte[] senhaSalt);
        bool VerificarSenha(string senha, byte[] senhaHash, byte[] senhaSalt);
        string CriarToken(UsuarioModel usuarioModel);
    }
}
