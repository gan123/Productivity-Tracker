using System;
using Microsoft.Practices.ServiceLocation;
using Raven.Client;

namespace ProductivityTracker.Services.UnitOfWork
{
    public class RavenSessionManager : IRavenSessionManager
    {
        private const string _sessionKey = "_currentSession";

        public IDocumentSession GetActiveSession()
        {
            if (Current == null)
                throw new InvalidOperationException("There is no active session");
            return Current;
        }

        public void SetActiveSession(IDocumentSession session)
        {
            if (Current != null)
                throw new InvalidOperationException("There is already a active session");
            Current = session;
        }

        public void ClearActiveSession()
        {
            Current = null;
        }

        public bool HasActiveSession
        {
            get { return Current != null; }
        }

        protected virtual IDocumentSession Current
        {
            get { return ServiceLocator.Current.GetInstance<IRequestState>().Get<IDocumentSession>(_sessionKey); }
            set { ServiceLocator.Current.GetInstance<IRequestState>().Store(_sessionKey, value); }

        }
    }
}