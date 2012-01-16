using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Recruiter;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetPositionsCoveredHandler : RavenRequestHandler<GetPositionsCoveredRequest, GetPositionsCoveredResponse>
    {
        private readonly IRecruiterService _recruiterService;

        public GetPositionsCoveredHandler(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        public override Response Handle(GetPositionsCoveredRequest request)
        {
            var response = CreateTypedResponse();
            response.Positions = Mapper.Map<IEnumerable<Domain.Model.PositionCovered>, IEnumerable<PositionCoveredDto>>(_recruiterService.GetPositionsCovered(request.RecruiterId));
            return response;
        }
    }
}