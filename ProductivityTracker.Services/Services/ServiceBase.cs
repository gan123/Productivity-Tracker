using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ProductivityTracker.Services.UnitOfWork;
using Raven.Client;

namespace ProductivityTracker.Services.Services
{
    public class ServiceBase
    {
        [Dependency]
        public IRavenSessionManager RavenSessionManager { get; set; }
        public IDocumentSession CurrentSession
        {
            get { return RavenSessionManager.GetActiveSession(); }
        }
    }

    public class FilterParam
    {
        
    }
}