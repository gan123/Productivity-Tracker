using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Recruiter;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class UpdateRecruiterHandler : RavenRequestHandler<UpdateRecruiterRequest, UpdateRecruiterResponse>
    {
        private readonly IRecruiterService _recruiterService;

        public UpdateRecruiterHandler(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        public override Response Handle(UpdateRecruiterRequest request)
        {
            _recruiterService.Update(request.Id, request.Name, request.Email, request.Contact, request.Designation);
            return CreateDefaultResponse();
        }
    }
}