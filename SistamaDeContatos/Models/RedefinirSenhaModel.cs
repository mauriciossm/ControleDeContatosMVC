using System.ComponentModel.DataAnnotations;

namespace SistamaDeContatos.Models
{
    public class RedefinirsenhaModel
    {
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite a e-mail do usuário")]
        public string Email { get; set; }
    }
}
