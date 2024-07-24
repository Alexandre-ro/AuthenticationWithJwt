using System.ComponentModel.DataAnnotations;

namespace AuthenticationWithJwt.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "Campo obrigatório não preenchido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não preenchido")]
        public string Senha { get; set; }
    }
}
