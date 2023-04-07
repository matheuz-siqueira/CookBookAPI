namespace cookbook_api.Dtos.User;

public class UpdatePasswordReq
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}
