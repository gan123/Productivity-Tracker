using Microsoft.Practices.Unity;

namespace ProductivityTracker.Services.UnityExtensions
{
    public static class UnityExtensions
    {
        public static void Install(this IUnityContainer container, params IUnityInstaller[] installers)
        {
            foreach(var installer in installers)
                installer.Install(container);
        }
    }
}