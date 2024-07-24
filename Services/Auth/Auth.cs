using AuthenticationWithJwt.Data;
using AuthenticationWithJwt.DTO;
using AuthenticationWithJwt.Models;
using AuthenticationWithJwt.SenhaService;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationWithJwt.Services.Auth
{
    public class Auth : IAuth
    {
        private readonly AppDbContext _context;
        private readonly ISenhaService _senhaService;

        public Auth(AppDbContext context, ISenhaService senhaService)
        {
            _context = context;
            _senhaService = senhaService;
        }

        public async Task<Response<UsuarioDTO>> Registrar(UsuarioDTO usuarioDTO)
        {
            Response<UsuarioDTO> response = new Response<UsuarioDTO>();

            try
            {
                if (VerificaEmailEUsuario(usuarioDTO))
                {
                    response.Status = false;
                    response.Dados = null;
                    response.Mensagem = "Usuário e/ou email já cadastrados. Verifique!";

                    return response;
                }

                _senhaService.CriarHashSenha(usuarioDTO.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = new UsuarioModel()
                {
                    Usuario = usuarioDTO.Usuario,
                    Email = usuarioDTO.Email,
                    Cargo = usuarioDTO.Cargo,
                    SenhaHash = senhaHash,
                    SenhaSalt = senhaSalt
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuário criado com sucesso";

                return response;
            }
            catch (Exception ex)
            {
                response.Dados = null;
                response.Status = false;
                response.Mensagem = ex.Message;

                return response;
            }
        }

        public bool VerificaEmailEUsuario(UsuarioDTO usuarioDTO)
        {
            var usuario = _context.Usuario.FirstOrDefault(u => u.Email == usuarioDTO.Email || u.Usuario == usuarioDTO.Usuario);

            if (usuario == null) return false;

            return true;
        }


        public async Task<Response<string>> Login(UsuarioLoginDTO usuarioLogin)
        {
            Response<string> response = new Response<string>();

            try
            {
                var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Email == usuarioLogin.Email);

                if (usuario == null)
                {
                    response.Status = false;
                    response.Mensagem = "Credenciais inválidas. Email não encontrado!";
                    response.Dados = null;
                }

                if (!_senhaService.VerificarSenha(usuarioLogin.Senha, usuario.SenhaHash, usuario.SenhaSalt))
                {
                    response.Status = false;
                    response.Mensagem = "Credenciais inválidas. Verifique!";
                    response.Dados = null;

                    return response;
                }

                var token = _senhaService.CriarToken(usuario);

                response.Status = true;
                response.Mensagem = "Token de autenticação criado com sucesso";
                response.Dados = token;

            }
            catch (Exception ex)
            {
                response.Dados = null;
                response.Status = false;
                response.Mensagem = ex.Message;
            }

            return response;
        }
    }
}
