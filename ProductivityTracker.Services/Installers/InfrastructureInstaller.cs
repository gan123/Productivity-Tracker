using Microsoft.Practices.Unity;
using ProductivityTracker.Services.UnitOfWork;
using ProductivityTracker.Services.UnityExtensions;

namespace ProductivityTracker.Services.Installers
{
    public class InfrastructureInstaller : IUnityInstaller
    {
        public void Install(IUnityContainer container)
        {
            container.RegisterType<IRavenSessionManager, RavenSessionManager>(new ContainerControlledLifetimeManager());
            container.RegisterType<IRequestState, RequestState>(new ContainerControlledLifetimeManager());
        }
    }
}