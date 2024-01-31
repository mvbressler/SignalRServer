using Bogus;

namespace GSIMSignalRServerProject.Application.Hub
{
    using GSIMSignalRServerProject.Domain.Models;
    using Microsoft.AspNetCore.SignalR;

    public class GConnectHub : Hub
    {
        public async Task GetTypes()
        {
            if (!IsUserAuthorized())
            {
                throw new HubException("Unauthorized access.");
            }

            await Clients.Caller.SendAsync("GetTypes", new { Server = "serverSignalR" });
        }

        public async Task GetTypeDescription(TypeDescriptionRequest request)
        {
            if (!IsUserAuthorized())
            {
                throw new HubException("Unauthorized access.");
            }
            await Clients.Caller.SendAsync("GetTypeDescription", new { Server = "serverSignalR", TypeName = "type"});
        }

        public async Task GetObjects(LenelObjectsRequest request)
        {
            if (!IsUserAuthorized())
            {
                throw new HubException("Unauthorized access.");
            }

            await Clients.Caller.SendAsync("GetObjects", new {
                Server = "serverSignalR", 
                TypeName = "type",
                Page = 1,
                PageSize = 50
            });
        }

        public async Task ExecuteMethod(LenelCommandRequest request)
        {
            if (!IsUserAuthorized())
            {
                throw new HubException("Unauthorized access.");
            }

            await Clients.Caller.SendAsync("ExecuteMethod", new
            {
                MethodName = "doExecute",
                TypeName = "type",
                Parameters = new Dictionary<string,object>(),
                InParams = new Dictionary<string, object>()
            });
        }

        private bool IsUserAuthorized()
        {
            // Implement your authorization logic
            // For example, check if the user is authenticated
            return Context.User is { Identity.IsAuthenticated: true };
        }
    }

}
