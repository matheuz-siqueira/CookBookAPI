using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cookbook_api.Models;

public class Ingredients
{
    [Required]
    public int Id { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string Products { get; set; }
    
    [Column(TypeName = "varchar(50)")]
    public string Quantity { get; set; }

    //navigation property 
    public Recipe Recipe { get; set; } 

    //foreing key 
    [Required]
    public int RecipeId { get; set; }
 
}
