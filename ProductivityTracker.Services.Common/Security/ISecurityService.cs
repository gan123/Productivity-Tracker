using System;
using Agatha.Common;

namespace ProductivityTracker.Services.Common.Security
{
    public interface ISecurityService
    {
        RecruiterDto CurrentRecruiter { get; }
        bool IsAuthenticated { get; }
        void Authenticate(string login, string password, Action onAuthenticated, Action<ExceptionInfo> onError);
    }
}