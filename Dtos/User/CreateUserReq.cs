using System.ComponentModel.DataAnnotations;

namespace cookbook_api.Dtos.User;

public class CreateUserReq
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Email { get; set; }
    
    [Required]
    [StringLength(60, MinimumLength = 6)]
    public string Passord { get; set; }
}
