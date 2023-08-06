namespace cookbook_api.Models;

public class Codes
{
    public int Id { get; set; }
    public string Code { get; set; }

    //navigation property 
    public User User { get; set; }
    public int UserId { get; set; }
}
