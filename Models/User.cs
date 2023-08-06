using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cookbook_api.Models;

public class User
{
    [Required]
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Name { get; set; }

    [Required]
    [Column(TypeName = "varchar(50)")]
    public string Email { get; set; }

    [Required]
    [Column(TypeName = "varchar(60)")]
    public string Password { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    //navigation property 
    public List<Recipe> Recipes { get; set; }
    public Codes Code { get; set; }
    public List<Connection> Connections { get; set; }

}
