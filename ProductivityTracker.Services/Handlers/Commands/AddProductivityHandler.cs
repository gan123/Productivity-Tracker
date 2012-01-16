using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Productivity;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class AddProductivityHandler : RavenRequestHandler<AddProductivityRequest, AddProductivityResponse>
    {
        private readonly IProductivityService _productivityService;

        public AddProductivityHandler(IProductivityService productivityService)
        {
            _productivityService = productivityService;
        }

        public override Response Handle(AddProductivityRequest request)
        {
            _productivityService.Add(
                request.RecruiterId, 
                request.RecruiterName,
                request.DateSent,
                request.PositionId, 
                request.PositionName,
                request.ClientId, 
                request.ClientName,
                request.StatusId, 
                request.StatusName,
                request.ExpectedFeedbackDate, 
                request.Comments, 
                request.CandidateId,
                request.CandidateName,
                request.CurrentUserLogin);
            return CreateDefaultResponse();
        }
    }
}