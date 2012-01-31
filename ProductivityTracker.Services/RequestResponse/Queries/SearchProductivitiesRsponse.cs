using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class SearchProductivitiesRsponse : Response
    {
        public IEnumerable<ProductivityDto> Results { get; set; }
    }
}