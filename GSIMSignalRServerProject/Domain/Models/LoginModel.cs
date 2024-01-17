namespace GSIMSignalRServerProject.Domain.Models;

public class LoginModel
{
    public string Server { get; set; } = "127.0.0.1";
    public string? Username { get; set; }
    public string? Password { get; set; }
}