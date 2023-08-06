using System.ComponentModel.DataAnnotations.Schema;

namespace cookbook_api.Models;

public class Connection
{
    public int Id { get; set; }

    [ForeignKey("UserId")]
    public int ConnectedWithUserId { get; set; }
    //navigation property
    public User User { get; set; }
    public int UserId { get; set; }
}
