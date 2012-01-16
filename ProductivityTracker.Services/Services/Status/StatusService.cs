using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Status
{
    public class StatusService : ServiceBase, IStatusService
    {
        public IEnumerable<Domain.Model.Status> GetAll()
        {
            return CurrentSession.Query<Domain.Model.Status>();
        }
    }
}