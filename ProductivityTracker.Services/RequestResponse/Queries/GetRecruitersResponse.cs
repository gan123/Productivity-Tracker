using System.Collections.Generic;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetRecruitersResponse : Response
    {
        public IEnumerable<RecruiterDto> Recruiters { get; set; }
    }
}