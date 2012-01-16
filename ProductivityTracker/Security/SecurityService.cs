using System;
using System.ComponentModel.Composition;
using Agatha.Common;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;

namespace ProductivityTracker.Security
{
    [Export(typeof(ISecurityService))]
    public class SecurityService : ISecurityService
    {
        private readonly IAsyncRequestDispatcherFactory _asycnRequestDispatcherFactory;
        private RecruiterDto _currentRecruiter;

        [ImportingConstructor]
        public SecurityService(
            IAsyncRequestDispatcherFactory asycnRequestDispatcherFactory)
        {
            _asycnRequestDispatcherFactory = asycnRequestDispatcherFactory;
        }

        public RecruiterDto CurrentRecruiter
        {
            get { return _currentRecruiter; }
        }

        public bool IsAuthenticated
        {
            get { return CurrentRecruiter != null; }
        }

        public void Authenticate(string login, string password, Action onAuthenticated, Action<ExceptionInfo> onError)
        {
            var requestDispatcher = _asycnRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetUserProfileRequest {Login = login, Password = password});
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      _currentRecruiter = r.Get<GetUserProfileResponse>().Recruiter;
                                                      onAuthenticated();
                                                  }, onError);
        }
    }
}