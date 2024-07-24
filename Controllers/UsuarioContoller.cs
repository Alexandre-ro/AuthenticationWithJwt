using AuthenticationWithJwt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationWithJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioContoller : ControllerBase
    {

        [Authorize]
        [HttpGet]
        public ActionResult<Response<string>> GetUsuario()
        {
            Response<string> response = new Response<string>();
            response.Mensagem = "Acesso liberado";

            return Ok(response);
        }
    }
}
