using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Recruiter;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class AddRecruiterHandler : RavenRequestHandler<AddRecruiterRequest, AddRecruiterResponse>
    {
        private readonly IRecruiterService _recruiterService;

        public AddRecruiterHandler(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        public override Response Handle(AddRecruiterRequest request)
        {
            _recruiterService.Add(request.FullName, request.Email, request.DateOfBirth, request.DateOfJoining, request.Contact, request.Designation, request.IsAdmin);
            return CreateDefaultResponse();
        }
    }
}