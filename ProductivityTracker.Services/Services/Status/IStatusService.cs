using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Status
{
    public interface IStatusService
    {
        IEnumerable<Domain.Model.Status> GetAll();
    }
}