using System;
using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Recruiter;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class UpdatePasswordHandler : RavenRequestHandler<UpdatePasswordRequest, UpdatePasswordResponse>
    {
        private readonly IRecruiterService _recruiterService;

        public UpdatePasswordHandler(IRecruiterService recruiterService)
        {
            _recruiterService = recruiterService;
        }

        public override Response Handle(UpdatePasswordRequest request)
        {
            _recruiterService.UpdatePassword(request.Login, request.Password);
            return CreateDefaultResponse();
        }
    }
}