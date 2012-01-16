using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Client;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetClientsCoveredHandler : RavenRequestHandler<GetClientsCoveredRequest, GetClientsCoveredResponse>
    {
        private readonly IClientService _clientService;

        public GetClientsCoveredHandler(IClientService clientService)
        {
            _clientService = clientService;
        }

        public override Response Handle(GetClientsCoveredRequest request)
        {
            var response = CreateTypedResponse();
            if (string.IsNullOrEmpty(request.Query))
                response.Clients = Mapper.Map<IEnumerable<Domain.Model.Client>, IEnumerable<ClientDto>>(_clientService.GetAll());
            else response.Clients = Mapper.Map<IEnumerable<Domain.Model.Client>, IEnumerable<ClientDto>>(_clientService.Find(request.Query));
            return response;
        }
    }
}