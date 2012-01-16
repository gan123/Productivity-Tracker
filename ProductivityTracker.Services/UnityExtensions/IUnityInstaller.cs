using Microsoft.Practices.Unity;

namespace ProductivityTracker.Services.UnityExtensions
{
    public interface IUnityInstaller
    {
        void Install(IUnityContainer container);
    }
}