using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Productivity;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class UpdateStatusAndCommentsHandler : RavenRequestHandler<UpdateStatusAndCommentsRequest, UpdateStatusAndCommentsResponse>
    {
        private readonly IProductivityService _productivityService;

        public UpdateStatusAndCommentsHandler(IProductivityService productivityService)
        {
            _productivityService = productivityService;
        }

        public override Response Handle(UpdateStatusAndCommentsRequest request)
        {
            _productivityService.UpdateStatusAndComments(request.Id, request.StatusId, request.StatusName, request.Comments, request.CurrentUserLogin);
            return CreateDefaultResponse();
        }
    }
}