using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetCandidatesResponse : Response
    {
        public IEnumerable<CandidateDto> Candidates { get; set; }
    }
}