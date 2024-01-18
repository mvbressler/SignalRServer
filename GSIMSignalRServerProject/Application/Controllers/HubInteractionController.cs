using Bogus;
using GSIMSignalRServerProject.Application.Hub;
using GSIMSignalRServerProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace GSIMSignalRServerProject.Application.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HubInteractionController : ControllerBase
{
    private readonly IHubContext<GConnectHub> _hubContext;

    public HubInteractionController(IHubContext<GConnectHub> hubContext)
    {
        _hubContext = hubContext;
    }

    [HttpPost("sendTypes")]
    public async Task<IActionResult> SendTypes()
    {
        // Mock data or retrieve real data to send
        var faker = new Faker();
        var types = Enumerable.Range(1, 5)
            .Select(_ => faker.Commerce.Product())
            .ToArray();

        // Using the hub context to call a method on the connected clients
        await _hubContext.Clients.All.SendAsync("ReceiveTypes", types);

        return Ok();
    }

    [HttpPost("sendTypeDescription")]
    public async Task<IActionResult> SendTypeDescription()
    {
        // Mock data or retrieve real data based on typeName
        var faker = new Faker();
        var description = faker.Commerce.ProductDescription();

        await _hubContext.Clients.All.SendAsync("ReceiveTypeDescription", description);

        return Ok();
    }

    [HttpPost("sendObjects")]
    public async Task<IActionResult> SendObjects(LenelObjectsRequest request)
    {
        var faker = new Faker();
        var objects = Enumerable.Range(1, request.PageSize)
            .Select(index => new
            {
                Id = index,
                Name = faker.Commerce.ProductName(),
                Description = faker.Commerce.ProductDescription()
            })
            .ToList();

        await _hubContext.Clients.All.SendAsync("ReceiveObjects", objects);

        return Ok();
    }
}