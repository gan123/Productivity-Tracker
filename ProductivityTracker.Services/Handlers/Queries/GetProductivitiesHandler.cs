using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Productivity;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetProductivitiesHandler : RavenRequestHandler<GetProductivitiesRequest, GetProductivitiesResponse>
    {
        private readonly IProductivityService _productivityService;

        public GetProductivitiesHandler(IProductivityService productivityService)
        {
            _productivityService = productivityService;
        }

        public override Response Handle(GetProductivitiesRequest request)
        {
            var response = CreateTypedResponse();
            response.Productivities = Mapper.Map<IEnumerable<Domain.Model.Productivity>, IEnumerable<ProductivityDto>>(_productivityService.Get(null));
            return response;
        }
    }
}