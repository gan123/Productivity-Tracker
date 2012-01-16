using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Candidate;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class AddCandidateHandler : RavenRequestHandler<AddCandidateRequest, AddCandidateResponse>
    {
        private readonly ICandidateService _candidateService;

        public AddCandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public override Response Handle(AddCandidateRequest request)
        {
            _candidateService.Add(request.Name, request.Contact, request.Company, request.CurrentCtc, request.ExpectedCtc, request.NoticePeriod);
            return CreateDefaultResponse();
        }
    }
}