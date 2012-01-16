using System;
using System.Transactions;
using Agatha.Common;
using Agatha.ServiceLayer;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Raven.Client;

namespace ProductivityTracker.Services.UnitOfWork
{
    public abstract class RavenRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
        where TRequest : Request
        where TResponse : Response, new()
    {
        [Dependency]
        public IDocumentStore DocumentStore { get; set; }

        [Dependency]
        public IRavenSessionManager RavenSessionManager { get; set; }

        public override Response Handle(Request request)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions{ IsolationLevel = IsolationLevel.ReadCommitted}))
            {
                using(var session = DocumentStore.OpenSession())
                {
                    RavenSessionManager.SetActiveSession(session);
                    Response response;

                    try
                    {
                        response = base.Handle(request);
                        session.SaveChanges();  
                    }
                    catch (Exception)
                    {
                        //TODO:
                        throw;
                    }

                    transactionScope.Complete();
                    return response;
                }
            }
        }

        protected override void DisposeManagedResources()
        {
            RavenSessionManager.ClearActiveSession();
        }
    }
}