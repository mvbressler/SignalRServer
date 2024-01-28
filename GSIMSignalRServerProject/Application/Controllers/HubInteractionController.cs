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
        var typesRequestFaker = new Faker<TypesRequest>()
            // Rule for generating fake Server data
            .RuleFor(tr => tr.Server, f => f.Internet.DomainName());

        // Generate a list of 10 fake TypesRequest objects
        TypesRequest fakeTypesRequest = typesRequestFaker.Generate();

        // Using the hub context to call a method on the connected clients
        await _hubContext.Clients.All.SendAsync("GetTypes", fakeTypesRequest);

        return Ok();
    }

    [HttpPost("sendTypeDescription")]
    public async Task<IActionResult> SendTypeDescription()
    {
        var typeDescriptionRequestFaker = new Faker<TypeDescriptionRequest>()
            // Rule for generating fake Server data
            .RuleFor(tdr => tdr.Server, f => f.Internet.DomainName())
            // Rule for generating fake TypeName data
            .RuleFor(tdr => tdr.TypeName, f => f.Lorem.Word());

        // Generate a list of 10 fake TypeDescriptionRequest objects
        TypeDescriptionRequest fakeTypeDescriptionRequest = typeDescriptionRequestFaker.Generate();

        await _hubContext.Clients.All.SendAsync("GetTypeDescription", fakeTypeDescriptionRequest);

        return Ok();
    }

    [HttpPost("sendObjects")]
    public async Task<IActionResult> SendObjects(LenelObjectsRequest request)
    {
        var lenelObjectsRequestFaker = new Faker<LenelObjectsRequest>()
           // Rule for generating fake TypeName data
           .RuleFor(lo => lo.TypeName, f => f.Lorem.Word())
           // Rule for generating fake Page data
           .RuleFor(lo => lo.Page, f => 2) // Assuming pages range from 1 to 100
                                                              // Rule for generating fake PageSize data
           .RuleFor(lo => lo.PageSize, f => 10) // Assuming page sizes range from 10 to 50
                                                                  // Rule for generating fake Server data
           .RuleFor(lo => lo.Server, f => f.Internet.DomainName());

        // Generate a single fake LenelObjectsRequest object
        LenelObjectsRequest fakeLenelObjectsRequest = lenelObjectsRequestFaker.Generate();

        await _hubContext.Clients.All.SendAsync("GetObjects", fakeLenelObjectsRequest);

        return Ok();
    }
}