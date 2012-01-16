using Raven.Client;

namespace ProductivityTracker.Services.UnitOfWork
{
    public interface IRavenSessionManager
    {
        IDocumentSession GetActiveSession();
        void SetActiveSession(IDocumentSession session);
        void ClearActiveSession();
        bool HasActiveSession { get; }
    }
}