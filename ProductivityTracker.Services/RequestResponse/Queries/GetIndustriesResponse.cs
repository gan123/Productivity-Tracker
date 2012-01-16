using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetIndustriesResponse : Response
    {
        public IEnumerable<IndustryDto> Industries { get; set; }
    }
}