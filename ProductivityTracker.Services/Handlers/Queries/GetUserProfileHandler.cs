using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Authroisation;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetUserProfileHandler : RavenRequestHandler<GetUserProfileRequest, GetUserProfileResponse>
    {
        private readonly IAuthorisationService _authorisationService;

        public GetUserProfileHandler(IAuthorisationService authorisationService)
        {
            _authorisationService = authorisationService;
        }

        public override Response Handle(GetUserProfileRequest request)
        {
            var response = CreateTypedResponse();
            response.Recruiter = Mapper.Map<Domain.Model.Recruiter, RecruiterDto>(_authorisationService.GetUserProfile(request.Login, request.Password));
            return response;
        }
    }
}