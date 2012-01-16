using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetProductivityByClientSummaryResponse : Response
    {
        public IEnumerable<ClientProductivitySummaryDto> Summary { get; set; }
    }
}