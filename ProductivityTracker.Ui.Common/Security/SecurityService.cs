using System;
using System.ComponentModel.Composition;
using Agatha.Common;
using ProductivityTracker.Common;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.Services.Authroisation;

namespace ProductivityTracker.Ui.Common.Security
{
    [Export(typeof(ISecurityService))]
    public class SecurityService : ISecurityService
    {
        private readonly IAsyncRequestDispatcherFactory _asycnRequestDispatcherFactory;
        private readonly ICurrentUser _currentUser;
        private RecruiterDto _currentRecruiter;

        [ImportingConstructor]
        public SecurityService(
            IAsyncRequestDispatcherFactory asycnRequestDispatcherFactory,
            ICurrentUser currentUser)
        {
            _asycnRequestDispatcherFactory = asycnRequestDispatcherFactory;
            _currentUser = currentUser;
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
                                                      _currentUser.Login = _currentRecruiter.Login;
                                                      onAuthenticated();
                                                  }, onError);
        }
    }
}