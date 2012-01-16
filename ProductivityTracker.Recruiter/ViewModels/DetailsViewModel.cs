using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agatha.Common;
using ProductivityTracker.Recruiter.Interfaces;
using ProductivityTracker.Recruiter.Models;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Recruiter.ViewModels
{
    [Export(typeof(IDetailsViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class DetailsViewModel : ChildWindowViewModelBase, IDetailsViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private IEnumerable<SummaryModel> _summary;

        [ImportingConstructor]
        public DetailsViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
        }

        public IEnumerable<SummaryModel> Summary
        {
            get { return _summary; }
            set
            {
                _summary = value;
                RaisePropertyChanged(() => Summary);
            }
        }

        public void Load(string id)
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetPositionsCoveredRequest {RecruiterId = id});
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      var positions = r.Get<GetPositionsCoveredResponse>().Positions;
                                                      var summary = positions.GroupBy(p => p.Client.Name, (k, g) => new SummaryModel
                                                                                                         {
                                                                                                             Client = k,
                                                                                                             Positions = g.Select(s => s.Name).Aggregate((a, b) => a + ", " + b)
                                                                                                         });
                                                      Summary = summary;
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }
    }
}