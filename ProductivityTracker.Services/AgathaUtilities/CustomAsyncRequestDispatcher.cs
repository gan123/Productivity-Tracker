using Agatha.Common;
using Agatha.Common.Caching;
using ProductivityTracker.Services.RequestResponse;
using ProductivityTracker.Services.Services.Authroisation;

namespace ProductivityTracker.Services.AgathaUtilities
{
    public class CustomAsyncRequestDispatcher : AsyncRequestDispatcher
    {
        private readonly ICurrentUser _currentUser;

        public CustomAsyncRequestDispatcher(
            IAsyncRequestProcessor requestProcessor, 
            ICacheManager cacheManager,
            ICurrentUser currentUser) : base(requestProcessor, cacheManager)
        {
            _currentUser = currentUser;
        }

        protected override void BeforeSendingRequests(System.Collections.Generic.IEnumerable<Request> requestsToProcess)
        {
            foreach(var request in requestsToProcess)
            {
                if (request is AuthenticatedRequest)
                    ((AuthenticatedRequest) request).CurrentUserLogin = _currentUser.Login;
            }
            base.BeforeSendingRequests(requestsToProcess);
        }
    }
}