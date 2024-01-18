using GSIMSignalRServerProject.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GSIMSignalRServerProject.Application.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/g-sim")]
public class GSimController: ControllerBase
{
    [HttpPost("types")]
    public IActionResult GetToken([FromBody] List<TypesRequest>? types)
    {
        Console.WriteLine("Types received");
        return Ok("Types received");
    }
    
    [HttpPost("type-description")]
    public IActionResult GetToken([FromBody] TypeDescriptionRequest? typeDescription)
    {
        Console.WriteLine("TypeDescriptionRequest received");
        return Ok();
    }
    
    [HttpPost("objects")]
    public IActionResult GetToken([FromBody] List<LenelObjectsRequest>? list)
    {
        Console.WriteLine("ObjectsRequests received");
        return Ok();
    }
}