using AuthenticationWithJwt.DTO;
using AuthenticationWithJwt.Models;

namespace AuthenticationWithJwt.Services.Auth
{
    public interface IAuth
    {
        Task<Response<UsuarioDTO>> Registrar(UsuarioDTO usuarioDTO);
        Task<Response<string>> Login(UsuarioLoginDTO usuarioLogin);
    }
}
