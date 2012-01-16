using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Position;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class RemovePositionHandler : RavenRequestHandler<RemovePositionRequest, RemovePositionResponse>
    {
        private readonly IPositionService _positionService;

        public RemovePositionHandler(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public override Response Handle(RemovePositionRequest request)
        {
            _positionService.Remove(request.Id);
            return CreateDefaultResponse();
        }
    }
}