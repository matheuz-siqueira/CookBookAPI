using System.ComponentModel.DataAnnotations;

namespace cookbook_api.Dtos.User;

public class CreateUserReq
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(
    50, MinimumLength = 3, ErrorMessage = "Nome precisa ter no mínimo {2} e no máximo {1} caracteres")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email é obrigatório")]
    [StringLength(50)]
    [RegularExpression("^\\S+@\\S+\\.\\S+$", ErrorMessage = "Insira um email válido")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Senha é obrigatório")]
    [StringLength(
    60, MinimumLength = 6, ErrorMessage = "Senha precisa ter entre {2} e {1} caracteres")]
    public string Password { get; set; }
}
