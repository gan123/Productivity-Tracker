using System.Collections.Generic;
using Agatha.Common;
using AutoMapper;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Position;
using ProductivityTracker.Services.UnitOfWork;

namespace ProductivityTracker.Services.Handlers.Queries
{
    public class GetPositionsHandler : RavenRequestHandler<GetPositionsRequest, GetPositionsResponse>
    {
        private readonly IPositionService _positionService;

        public GetPositionsHandler(IPositionService positionService)
        {
            _positionService = positionService;
        }

        public override Response Handle(GetPositionsRequest request)
        {
            var response = CreateTypedResponse();
            response.Positions = Mapper.Map<IEnumerable<Domain.Model.Position>, IEnumerable<PositionDto>>(_positionService.GetAll());
            return response;
        }
    }
}