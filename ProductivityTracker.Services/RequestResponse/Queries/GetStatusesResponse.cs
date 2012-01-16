using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetStatusesResponse : Response
    {
        public IEnumerable<StatusDto> Statuses { get; set; }
    }
}