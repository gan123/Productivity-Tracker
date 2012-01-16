using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Candidate;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetCandidatesHandler : RavenRequestHandler<GetCandidatesRequest, GetCandidatesResponse>
    {
        private readonly ICandidateService _candidateService;

        public GetCandidatesHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public override Response Handle(GetCandidatesRequest request)
        {
            var response = CreateTypedResponse();
            if (string.IsNullOrEmpty(request.Query))
                response.Candidates = Mapper.Map<IEnumerable<Domain.Model.Candidate>, IEnumerable<CandidateDto>>(_candidateService.GetAll());
            else response.Candidates = Mapper.Map<IEnumerable<Domain.Model.Candidate>, IEnumerable<CandidateDto>>(_candidateService.Find(request.Query));
            return response;
        }
    }
}