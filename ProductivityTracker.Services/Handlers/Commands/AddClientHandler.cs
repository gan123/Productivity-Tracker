using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Client;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class AddClientHandler : RavenRequestHandler<AddClientRequest, AddClientResponse>
    {
        private readonly IClientService _clientService;

        public AddClientHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public override Response Handle(AddClientRequest request)
        {
            _clientService.Add(request.Name, request.IndustryId, request.Address, request.Website, request.Contacts, request.PositionIds);
            return CreateDefaultResponse();
        }
    }
}