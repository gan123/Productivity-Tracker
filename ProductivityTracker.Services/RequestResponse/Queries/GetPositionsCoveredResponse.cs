using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetPositionsCoveredResponse : Response
    {
        public IEnumerable<PositionCoveredDto> Positions { get; set; }
    }
}