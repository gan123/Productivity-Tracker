using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Status;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetStatusesHandler : RavenRequestHandler<GetStatusesRequest, GetStatusesResponse>
    {
        private readonly IStatusService _statusService;

        public GetStatusesHandler(IStatusService statusService)
        {
            _statusService = statusService;
        }

        public override Response Handle(GetStatusesRequest request)
        {
            var response = CreateTypedResponse();
            response.Statuses = Mapper.Map<IEnumerable<Domain.Model.Status>, IEnumerable<StatusDto>>(_statusService.GetAll());
            return response;
        }
    }
}