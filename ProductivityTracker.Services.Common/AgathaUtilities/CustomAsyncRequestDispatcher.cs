using Agatha.Common;
using Agatha.Common.Caching;
using ProductivityTracker.Services.Common.Security;

namespace ProductivityTracker.Services.Common.AgathaUtilities
{
    public class CustomAsyncRequestDispatcher : AsyncRequestDispatcher
    {
        private readonly ISecurityService _securityService;

        public CustomAsyncRequestDispatcher(
            IAsyncRequestProcessor requestProcessor, 
            ICacheManager cacheManager,
            ISecurityService securityService) : base(requestProcessor, cacheManager)
        {
            _securityService = securityService;
        }

        protected override void BeforeSendingRequests(System.Collections.Generic.IEnumerable<Request> requestsToProcess)
        {
            foreach(var request in requestsToProcess)
            {
                if (request is AuthenticatedRequest)
                    ((AuthenticatedRequest) request).CurrentLoggedInUserLogin = _securityService.CurrentRecruiter.Login;
            }
            base.BeforeSendingRequests(requestsToProcess);
        }
    }
}