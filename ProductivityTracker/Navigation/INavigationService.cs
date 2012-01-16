using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Regions;

namespace ProductivityTracker.Navigation
{
    public interface INavigationService
    {
        void NavigateTo(string regionName, string viewExportName, System.Collections.Generic.Dictionary<string, string> queryParams);
    }

    [Export(typeof(INavigationService))]
    public class NavigationService : INavigationService
    {
        private readonly IRegionManager _regionManager;

        [ImportingConstructor]
        public NavigationService(
            IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void NavigateTo(string regionName, string viewExportName, Dictionary<string, string> queryParams)
        {
            if (queryParams.Any())
            {
                var query = new UriQuery();
                foreach(var param in queryParams)
                    query.Add(param.Key, param.Value);
                _regionManager.RequestNavigate(regionName, new Uri(viewExportName + query, UriKind.Relative));
            }
            else _regionManager.RequestNavigate(regionName, new Uri(viewExportName, UriKind.Relative));
        }
    }
}