using AuthenticationWithJwt.Enum;
using System.ComponentModel.DataAnnotations;

namespace AuthenticationWithJwt.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage ="Campo obrigatório não preenchido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não preenchido")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não preenchido")]
        public CargoEnum Cargo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não preenchido")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório não preenchido")]
        public string ConfirmacaoSenha { get; set; }        
    }
}
