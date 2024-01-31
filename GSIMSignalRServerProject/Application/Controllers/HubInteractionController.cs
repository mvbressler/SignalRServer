using Bogus;
using GSIMSignalRServerProject.Application.Hub;
using GSIMSignalRServerProject.Domain.Enums;
using GSIMSignalRServerProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        // Using the hub context to call a method on the connected clients
        await _hubContext.Clients.All.SendAsync("GetTypes", new TypesRequest{ Server = "localhost" });

        return Ok();
    }

    [HttpPost("sendTypeDescription")]
    public async Task<IActionResult> SendTypeDescription()
    {
        await _hubContext.Clients.All.SendAsync("GetTypeDescription", new 
        {
            Server="localhost", 
            TypeName="Doors"
        });

        return Ok();
    }

    [HttpPost("sendObjects")]
    public async Task<IActionResult> SendObjects(LenelObjectsRequest request)
    {        
        await _hubContext.Clients.All.SendAsync("GetObjects", new 
        {
            Server="localhost", 
            TypeName="Doors",
            Page=1,
            PageSize=100
        });

        return Ok();
    }

    /// <summary>
    /// Sends a ExecuteMethod message by SignalR channel based on the specified type.
    /// </summary>
    /// <param name="TypeName">TypeName will define the parameters to by sent </param>
    [HttpPost("sendCommand")]    
    public async Task<IActionResult> SendCommand(TestingTypeNamesEnum TypeName)
    {

        var dict = new Dictionary<string, object>();
        dict.Add("Description", "Test event from OpenAccess");
        dict.Add("Source", "Logical Source 6");
        await _hubContext.Clients.All.SendAsync("ExecuteMethod", new
        {
            MethodName = "SendIncomingEvent",
            TypeName = "Lnl_IncomingEvent",
            InParams = dict

        });

        return Ok();
    }
}
