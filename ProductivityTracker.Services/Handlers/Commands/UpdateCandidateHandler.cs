using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Candidate;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class UpdateCandidateHandler : RavenRequestHandler<UpdateCandidateRequest, UpdateCandidateResponse>
    {
        private readonly ICandidateService _candidateService;

        public UpdateCandidateHandler(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public override Response Handle(UpdateCandidateRequest request)
        {
            _candidateService.Update(request.Id, request.Name, request.Contact, request.Company, request.CurrentCtc, request.ExpectedCtc, request.NoticePeriod);
            return CreateDefaultResponse();
        }
    }
}