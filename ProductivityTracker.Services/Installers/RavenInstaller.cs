using System;
using System.ServiceProcess;
using Microsoft.Practices.Unity;
using ProductivityTracker.Services.Indexes;
using ProductivityTracker.Services.UnityExtensions;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace ProductivityTracker.Services.Installers
{
    public class RavenInstaller : IUnityInstaller
    {
        public void Install(IUnityContainer container)
        {
            container.RegisterInstance(CreateDocumentStore());
        }

        private static IDocumentStore CreateDocumentStore()
        {
            var service = new ServiceController("ProductivityTracker_Raven");
            if (service.Status == ServiceControllerStatus.Stopped)
            {
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(60));
            }

            var store = new DocumentStore {Url = "http://localhost:8080"};
            store.Initialize();

            IndexCreation.CreateIndexes(typeof(RecruiterIndex).Assembly, store);
            return store;
        }
    }
}