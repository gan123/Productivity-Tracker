using System.Security;
using Microsoft.Practices.Unity;
using ProductivityTracker.Services.AgathaUtilities;
using ProductivityTracker.Services.Handlers.Queries;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Services.UnityExtensions;

namespace ProductivityTracker.Services.Installers
{
    public class AgathaInstaller : IUnityInstaller
    {
        public void Install(IUnityContainer container)
        {
            new Agatha.ServiceLayer.ServiceLayerAndClientConfiguration(
                typeof(GetUserProfileHandler).Assembly,
                typeof(GetUserProfileRequest).Assembly,
                new Agatha.Unity.Container(container))
                {
                    SecurityExceptionType = typeof(SecurityException),
                    AsyncRequestDispatcherImplementation = typeof(CustomAsyncRequestDispatcher)
                    
                }.Initialize();
        }
    }
}
