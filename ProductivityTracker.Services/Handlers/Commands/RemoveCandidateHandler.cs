using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Candidate;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class RemoveCandidateHandler : RavenRequestHandler<RemoveCandidateRequest, RemoveCandidateResponse>
    {
        private readonly ICandidateService _candidateService;

        public RemoveCandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public override Response Handle(RemoveCandidateRequest request)
        {
            _candidateService.Remove(request.Id);
            return CreateDefaultResponse();
        }
    }
}