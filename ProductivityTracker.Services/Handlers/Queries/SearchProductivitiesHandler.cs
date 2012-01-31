using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Productivity;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class SearchProductivitiesHandler : RavenRequestHandler<SearchProductivitiesRequest, SearchProductivitiesRsponse>
    {
        private readonly IProductivityService _productivityService;

        public SearchProductivitiesHandler(IProductivityService productivityService)
        {
            _productivityService = productivityService;
        }

        public override Response Handle(SearchProductivitiesRequest request)
        {
            var response = CreateTypedResponse();
            var results = _productivityService.Search(
                request.ClientId,
                request.CandidateId,
                request.RecruiterId,
                request.StatusId,
                request.Month,
                request.Week);
            response.Results = Mapper.Map<IEnumerable<Domain.Model.Productivity>, IEnumerable<ProductivityDto>>(results);
            return response;
        }
    }
}