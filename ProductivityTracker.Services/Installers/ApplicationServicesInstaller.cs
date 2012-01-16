using Microsoft.Practices.Unity;
using ProductivityTracker.Services.Services.Authroisation;
using ProductivityTracker.Services.Services.Candidate;
using ProductivityTracker.Services.Services.Client;
using ProductivityTracker.Services.Services.Industry;
using ProductivityTracker.Services.Services.Position;
using ProductivityTracker.Services.Services.Productivity;
using ProductivityTracker.Services.Services.Recruiter;
using ProductivityTracker.Services.Services.Status;
using ProductivityTracker.Services.UnityExtensions;

namespace ProductivityTracker.Services.Installers
{
    public class ApplicationServicesInstaller : IUnityInstaller
    {
        public void Install(IUnityContainer container)
        {
            container.RegisterType<ICurrentUser, CurrentUser>(new ContainerControlledLifetimeManager());
            container.RegisterType<IAuthorisationService, AuthorisationService>();
            container.RegisterType<IRecruiterService, RecruiterService>();
            container.RegisterType<IClientService, ClientService>();
            container.RegisterType<IIndustryService, IndustryService>();
            container.RegisterType<IPositionService, PositionService>();
            container.RegisterType<ICandidateService, CandidateService>();
            container.RegisterType<IStatusService, StatusService>();
            container.RegisterType<IProductivityService, ProductivityService>();
        }
    }
}