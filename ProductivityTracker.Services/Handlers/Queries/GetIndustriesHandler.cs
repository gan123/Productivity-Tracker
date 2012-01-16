using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Industry;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetIndustriesHandler : RavenRequestHandler<GetIndustriesRequest, GetIndustriesResponse>
    {
        private readonly IIndustryService _industryService;

        public GetIndustriesHandler(IIndustryService industryService)
        {
            _industryService = industryService;
        }

        public override Response Handle(GetIndustriesRequest request)
        {
            var response = CreateTypedResponse();
            response.Industries = Mapper.Map<IEnumerable<Domain.Model.Industry>, IEnumerable<IndustryDto>>(_industryService.GetAll());
            return response;
        }
    }
}