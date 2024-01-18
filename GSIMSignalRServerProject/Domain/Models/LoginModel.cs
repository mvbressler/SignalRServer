using System.Text.Json.Serialization;

namespace GSIMSignalRServerProject.Domain.Models;

public class LoginModel
{
    [JsonPropertyName("server")]
    public string Server { get; set; } = "127.0.0.1";
    [JsonPropertyName("login")]
    public string? Username { get; set; }
    [JsonPropertyName("password")]
    public string? Password { get; set; }
}