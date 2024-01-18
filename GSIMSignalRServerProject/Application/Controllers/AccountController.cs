using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSIMSignalRServerProject.Application.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/account")]
public class AccountController: ControllerBase
{
    [HttpGet("logout")]
    public IActionResult Logout()
    {
        // You can retrieve the user's information from the token if needed
        var userName = User.Identity?.Name;

        // Perform server-side actions, such as logging the logout event
        LogUserLogout(userName);

        return Ok("Logged out successfully.");
    }
    
    private void LogUserLogout(string userName)
    {
        Console.WriteLine($"The username {userName} is logged out successfully");
    }
}