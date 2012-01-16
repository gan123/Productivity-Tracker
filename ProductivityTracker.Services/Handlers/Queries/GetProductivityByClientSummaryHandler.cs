using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Productivity;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetProductivityByClientSummaryHandler : RavenRequestHandler<GetProductivityByClientSummaryRequest, GetProductivityByClientSummaryResponse>
    {
        private readonly IProductivityService _productivityService;

        public GetProductivityByClientSummaryHandler(IProductivityService productivityService)
        {
            _productivityService = productivityService;
        }

        public override Response Handle(GetProductivityByClientSummaryRequest request)
        {
            var response = CreateTypedResponse();
            response.Summary = Mapper.Map<IEnumerable<Domain.Model.Productivity>, IEnumerable<ClientProductivitySummaryDto>>(_productivityService.Get(request.ClientId));
            return response;
        }
    }
}