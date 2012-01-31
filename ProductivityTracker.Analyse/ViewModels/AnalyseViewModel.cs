using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;
using Agatha.Common;
using AutoMapper;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Analyse.Interfaces;
using ProductivityTracker.Analyse.Models;
using ProductivityTracker.Analyse.Views;
using ProductivityTracker.Common;
using ProductivityTracker.Controls.Search;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Ui.Common;
using Visifire.Charts;

namespace ProductivityTracker.Analyse.ViewModels
{
    [Export(typeof(IAnalyseViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public class AnalyseViewModel : ViewModelBase, IAnalyseViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IUpdateStatusViewModel _updateStatusViewModel;
        private CollectionViewSource _productivities;
        private IEnumerable<StatusModel> _statuses;
        private readonly InteractionRequest<ResponseNotification> _updateStatusRequest;
        private StatusModel _status;
        private IEnumerable<RecruiterModel> _recruiters;
        private RecruiterModel _recruiter;
        private IEnumerable<string> _months;
        private ClientSearch _client;
        private CandidateSearch _candidate;
        private string _month;
        private IEnumerable<string> _weeks;
        private string _week;
        private DataSeriesCollection _chartSource;

        [ImportingConstructor]
        public AnalyseViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUpdateStatusViewModel updateStatusViewModel)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _updateStatusViewModel = updateStatusViewModel;
            _updateStatusRequest = new InteractionRequest<ResponseNotification>();

            UpdateStatusCommand = new DelegateCommand<ProductivityModel>(UpdateStatus);
            SearchCommand = new DelegateCommand(Search);
            ClearCommand = new DelegateCommand(Clear);
            Productivities = new CollectionViewSource();
            Productivities.GroupDescriptions.Add(new PropertyGroupDescription("ClientName"));
        }

        public void Load()
        {
            IsBusy = false;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetProductivitiesRequest());
            requestDispatcher.Add(new GetRecruitersRequest());
            requestDispatcher.Add(new GetStatusesRequest());
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Statuses = Mapper.Map<IEnumerable<StatusDto>, IEnumerable<StatusModel>>(r.Get<GetStatusesResponse>().Statuses);
                                                      Recruiters = Mapper.Map<IEnumerable<RecruiterDto>, IEnumerable<RecruiterModel>>(r.Get<GetRecruitersResponse>().Recruiters);
                                                      var productivities = Mapper.Map<IEnumerable<ProductivityDto>, IEnumerable<ProductivityModel>>(r.Get<GetProductivitiesResponse>().Productivities);
                                                      LoadProductivities(productivities);
                                                      Months = productivities.Select(p => p.Month).Distinct();
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private void LoadProductivities(IEnumerable<ProductivityModel> productivities)
        {
            Productivities.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() =>
            {
                Productivities.Source = productivities;
                MonthChart(productivities);
            }));
        }

        private void MonthChart(IEnumerable<ProductivityModel> productivityModels)
        {
            var dataSeries = new DataSeriesCollection();
            var recruiters = Recruiter == null ? Recruiters : Recruiters.Where(r => r.Id == Recruiter.Id);
            foreach (var recruiter in recruiters)
            {
                var items = productivityModels.Where(p => p.RecruiterFullName == recruiter.FullName);
                if (items.Any())
                {
                    var series = new DataSeries { LegendText = recruiter.FullName, RenderAs = RenderAs.Column };
                    IEnumerable<DataPoint> dataPoints;
                    dataPoints = !string.IsNullOrEmpty(Week)
                                         ? items.GroupBy(i => i.Week,
                                                         (k, g) => new DataPoint
                                                         {
                                                             YValue = g.Count(),
                                                             AxisXLabel = k
                                                         })
                                         : items.GroupBy(i => i.Month,
                                                         (k, g) => new DataPoint
                                                         {
                                                             YValue = g.Count(),
                                                             AxisXLabel = k
                                                         });
                    foreach (var point in dataPoints) series.DataPoints.Add(point);
                    dataSeries.Add(series);
                }
            }

            ChartSource = dataSeries;
            if (ChartUpdated != null) ChartUpdated(this, EventArgs.Empty);
        }

        public IInteractionRequest UpdateStatusRequest
        {
            get { return _updateStatusRequest; }
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Load();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public CollectionViewSource Productivities
        {
            get { return _productivities; }
            set
            {
                _productivities = value;
                RaisePropertyChanged(() => Productivities);
            }
        }

        public ClientSearch Client
        {
            get { return _client; }
            set
            {
                _client = value;
                RaisePropertyChanged(() => Client);
            }
        }

        public CandidateSearch Candidate
        {
            get { return _candidate; }
            set
            {
                _candidate = value;
                RaisePropertyChanged(() => Candidate);
            }
        }

        public IEnumerable<StatusModel> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                RaisePropertyChanged(() => Statuses);
            }
        }

        public StatusModel Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
            }
        }

        public IEnumerable<RecruiterModel> Recruiters
        {
            get { return _recruiters; }
            set
            {
                _recruiters = value;
                RaisePropertyChanged(() => Recruiters);
            }
        }

        public RecruiterModel Recruiter
        {
            get { return _recruiter; }
            set
            {
                _recruiter = value;
                RaisePropertyChanged(() => Recruiter);
            }
        }

        public IEnumerable<string> Months
        {
            get { return _months; }
            set
            {
                _months = value;
                RaisePropertyChanged(() => Months);
            }
        }

        public string Month
        {
            get { return _month; }
            set
            {
                _month = value;
                RaisePropertyChanged(() => Month);
                RaisePropertyChanged(() => IsWeekEnabled);
            }
        }

        public IEnumerable<string> Weeks
        {
            get { return _weeks; }
            set
            {
                _weeks = value;
                RaisePropertyChanged(() => Weeks);
            }
        }

        public DataSeriesCollection ChartSource
        {
            get { return _chartSource; }
            set
            {
                _chartSource = value;
                RaisePropertyChanged(() => ChartSource);
            }
        }

        public string Week
        {
            get { return _week; }
            set
            {
                _week = value;
                RaisePropertyChanged(() => Week);
            }
        }

        public bool IsWeekEnabled
        {
            get { return !string.IsNullOrEmpty(Month); }
        }

        public ICommand UpdateStatusCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand ClearCommand { get; set; }

        public event EventHandler ChartUpdated;

        private void UpdateStatus(ProductivityModel productivity)
        {
            _updateStatusViewModel.Id = productivity.Id;
            _updateStatusViewModel.Statuses = Statuses;
            _updateStatusViewModel.Status = new StatusModel {Id = productivity.Status.Id, Name = productivity.Status.Name};
            _updateStatusRequest.Raise(new ResponseNotification { ChildWindow = new UpdateStatusView(), Content = _updateStatusViewModel},
                r =>
                    {
                        if (!r.Result.HasValue)return;
                        if (r.Result.Value)
                        {
                            Thread.Sleep(500);
                            Load();
                        }
                    });
        }

        private void Clear()
        {
            Candidate = null;
            Client = null;
            Month = null;
            Week = null;
            Status = null;
            Recruiter = null;
            Search();
        }

        private void Search()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new SearchProductivitiesRequest
                                      {
                                          CandidateId = Candidate != null ? Candidate.Id : null,
                                          ClientId = Client != null ? Client.Id : null,
                                          Month = Month,
                                          RecruiterId = Recruiter != null ? Recruiter.Id : null,
                                          StatusId = Status != null ? Status.Id : null,
                                          Week = Week
                                      });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      var productivities = Mapper.Map<IEnumerable<ProductivityDto>, IEnumerable<ProductivityModel>>(r.Get<SearchProductivitiesRsponse>().Results);
                                                      LoadProductivities(productivities);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }
    }
}