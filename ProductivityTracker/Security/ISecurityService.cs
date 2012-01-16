using System;
using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Security
{
    public interface ISecurityService
    {
        RecruiterDto CurrentRecruiter { get; }
        bool IsAuthenticated { get; }
        void Authenticate(string login, string password, Action onAuthenticated, Action<ExceptionInfo> onError);
    }
}