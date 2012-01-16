using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Client;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class UpdateClientHandler : RavenRequestHandler<UpdateClientRequest, UpdateClientResponse>
    {
        private readonly IClientService _clientService;

        public UpdateClientHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public override Response Handle(UpdateClientRequest request)
        {
            _clientService.Update(request.Id, request.Name, request.IndustryId, request.Address, request.Website, request.Contacts, request.PositionIds);
            return CreateDefaultResponse();
        }
    }
}