using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ProductivityTracker.Services.Installers;
using ProductivityTracker.Services.Mappings;
using ProductivityTracker.Services.UnityExtensions;

namespace ProductivityTracker.Services
{
    public class ServiceConfiguration
    {
        public static void Initialise(IUnityContainer unityContainer)
        {
            unityContainer.Install(
                new ApplicationServicesInstaller(),
                new InfrastructureInstaller(),
                new AgathaInstaller(),
                new RavenInstaller()
                );

            ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
            ObjectMapper.Initalise();
        }
    }
}