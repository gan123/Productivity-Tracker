using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetPositionsResponse : Response
    {
        public IEnumerable<PositionDto> Positions { get; set; }
    }
}