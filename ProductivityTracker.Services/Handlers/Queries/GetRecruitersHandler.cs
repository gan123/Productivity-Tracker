using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Recruiter;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetRecruitersHandler : RavenRequestHandler<GetRecruitersRequest, GetRecruitersResponse>
    {
        private readonly IRecruiterService _recruiterService;

        public GetRecruitersHandler(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        public override Response Handle(GetRecruitersRequest request)
        {
            var response = CreateTypedResponse();
            response.Recruiters = Mapper.Map<IEnumerable<Domain.Model.Recruiter>, IEnumerable<RecruiterDto>>(_recruiterService.GetRecruiters());
            return response;
        }
    }
}