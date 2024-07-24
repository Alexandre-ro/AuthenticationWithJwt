using AuthenticationWithJwt.DTO;
using AuthenticationWithJwt.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWithJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _auth;

        public AuthController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UsuarioDTO usuarioDTO)
        {
            var response = await _auth.Registrar(usuarioDTO);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UsuarioLoginDTO usuarioLogin) 
        {
            var response = await _auth.Login(usuarioLogin);

            return Ok(response);            
        }
    }
}
