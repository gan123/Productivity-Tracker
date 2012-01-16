using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetClientsCoveredResponse : Response
    {
        public IEnumerable<ClientDto> Clients { get; set; }
    }
}