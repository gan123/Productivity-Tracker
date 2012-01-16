using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Recruiter;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class RemoveRecruiterHandler : RavenRequestHandler<RemoveRecruiterRequest, RemoveRecruiterResponse>
    {
        private readonly IRecruiterService _recruiterService;

        public RemoveRecruiterHandler(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        public override Response Handle(RemoveRecruiterRequest request)
        {
            _recruiterService.Remove(request.RecruiterId);
            return CreateDefaultResponse();
        }
    }
}