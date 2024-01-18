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

            var faker = new Faker();
            var types = Enumerable.Range(1, 5)
                .Select(_ => faker.Commerce.Product())
                .ToArray();

            await Clients.Caller.SendAsync("GetTypes", types);
        }

        public async Task GetTypeDescription(TypeDescriptionRequest request)
        {
            if (!IsUserAuthorized())
            {
                throw new HubException("Unauthorized access.");
            }
            
            var faker = new Faker();
            var description = faker.Commerce.ProductDescription();

            await Clients.Caller.SendAsync("GetTypeDescription", description);
        }

        public async Task GetObjects(LenelObjectsRequest request)
        {
            if (!IsUserAuthorized())
            {
                throw new HubException("Unauthorized access.");
            }
            
            var faker = new Faker();
            var objects = Enumerable.Range(1, request.PageSize)
                .Select(index => new
                {
                    Id = index,
                    Name = faker.Commerce.ProductName(),
                    Description = faker.Commerce.ProductDescription()
                })
                .ToList();

            await Clients.Caller.SendAsync("GetObjects", objects);
        }
        
        private bool IsUserAuthorized()
        {
            // Implement your authorization logic
            // For example, check if the user is authenticated
            return Context.User is { Identity.IsAuthenticated: true };
        }
    }

}
