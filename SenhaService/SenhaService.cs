using AuthenticationWithJwt.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthenticationWithJwt.SenhaService
{
    public class SenhaService : ISenhaService
    {
        private readonly IConfiguration _configuration;

        public SenhaService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CriarHashSenha(string senha, out byte[] senhaHash, out byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                senhaSalt = hmac.Key;
                senhaHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(senha));
            }
        }

        public bool VerificarSenha(string senha, byte[] senhaHash, byte[] senhaSalt)
        {
            using (var hmac = new HMACSHA512(senhaSalt))
            {
                var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(senha));

                var result = computeHash.SequenceEqual(senhaHash);

                return result;
            }
        }


        public string CriarToken(UsuarioModel usuarioModel)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("Cargo", usuarioModel.Cargo.ToString()),
                new Claim("Email", usuarioModel.Email.ToString()),
                new Claim("UserName", usuarioModel.Usuario.ToString())
            };

            //Chave de autenticação
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            //credenciais
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //cria o token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            //cria o jwt token
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

    }
}
