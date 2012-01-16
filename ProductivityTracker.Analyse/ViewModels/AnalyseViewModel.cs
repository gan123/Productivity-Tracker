using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
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
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Ui.Common;

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

        [ImportingConstructor]
        public AnalyseViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IUpdateStatusViewModel updateStatusViewModel)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _updateStatusViewModel = updateStatusViewModel;
            _updateStatusRequest = new InteractionRequest<ResponseNotification>();

            UpdateStatusCommand = new DelegateCommand<ProductivityModel>(UpdateStatus);
            Productivities = new CollectionViewSource();
            Productivities.GroupDescriptions.Add(new PropertyGroupDescription("ClientName"));
        }

        public void Load()
        {
            IsBusy = false;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetProductivitiesRequest());
            requestDispatcher.Add(new GetStatusesRequest());
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Statuses = Mapper.Map<IEnumerable<StatusDto>, IEnumerable<StatusModel>>(r.Get<GetStatusesResponse>().Statuses);
                                                      var productivties = Mapper.Map<IEnumerable<ProductivityDto>, IEnumerable<ProductivityModel>>(
                                                          r.Get<GetProductivitiesResponse>().Productivities);
                                                      Productivities.Dispatcher.BeginInvoke(DispatcherPriority.Render, new Action(() => { Productivities.Source = productivties; }));
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
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

        public IEnumerable<StatusModel> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                RaisePropertyChanged(() => Statuses);
            }
        }

        public ICommand UpdateStatusCommand { get; set; }

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
    }
}