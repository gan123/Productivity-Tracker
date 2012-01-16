using Agatha.Common;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.Services.Position;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Commands
{
    public class AddPositionHandler : RavenRequestHandler<AddPositionRequest, AddPositionResponse>
    {
        private readonly IPositionService _positionService;

        public AddPositionHandler(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public override Response Handle(AddPositionRequest request)
        {
            _positionService.Add(request.Name);
            return CreateDefaultResponse();
        }
    }
}