using System.ComponentModel.Composition.Hosting;
using System.Windows;
using AutoMapper;
using MefContrib.Integration.Unity;
using Microsoft.Practices.Prism.MefExtensions;
using Microsoft.Practices.Unity;
using ProductivityTracker.Analyse.Views;
using ProductivityTracker.Candidate.Views;
using ProductivityTracker.Client.Views;
using ProductivityTracker.Common;
using ProductivityTracker.Controls;
using ProductivityTracker.Controls.Search;
using ProductivityTracker.Productivity.Data.Views;
using ProductivityTracker.Recruiter.Views;
using ProductivityTracker.Services;
using ProductivityTracker.Ui.Common.Security;
using ProductivityTracker.Views;

namespace ProductivityTracker
{
    public class Bootstrapper : MefBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.GetExportedValue<ShellView>();
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(RecruitersView).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ClientView).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(CandidatesView).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(DataEntryView).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(AnalyseView).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(IDialogService).Assembly));
            AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ISecurityService).Assembly));
        }

        protected override void ConfigureContainer()
        {
            InitialiseMappings();

            var unityContainer = new UnityContainer();
            unityContainer.RegisterCatalog(AggregateCatalog);

            ServiceConfiguration.Initialise(unityContainer);
            
            Container = unityContainer.Resolve<CompositionContainer>();
            base.ConfigureContainer();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow = (ShellView) Shell;
            Application.Current.MainWindow.Show();
        }

        private static void InitialiseMappings()
        {
            Recruiter.ObjectMapper.Initialise();
            Client.ObjectMapper.Initialise();
            Candidate.ObjectMapper.Initialise();
            Productivity.Data.ObjectMapper.Initialise();
            Analyse.ObjectMapper.Initialise();

            Mapper.CreateMap<ClientDto, ClientSearch>();
            Mapper.CreateMap<PositionDto, ClientPosition>();
            Mapper.CreateMap<CandidateDto, CandidateModel>();
        }
    }
}
