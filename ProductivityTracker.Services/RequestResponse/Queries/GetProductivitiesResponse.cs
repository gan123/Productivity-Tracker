using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetProductivitiesResponse : Response
    {
        public IEnumerable<ProductivityDto> Productivities { get; set; }
    }
}