using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using Agatha.Common;
using AutoMapper;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Client.Extensions;
using ProductivityTracker.Client.Interfaces;
using ProductivityTracker.Client.Models;
using ProductivityTracker.Client.Views;
using ProductivityTracker.Common;
using ProductivityTracker.Controls;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Client.ViewModels
{
    [Export(typeof(IClientViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public class ClientViewModel : ViewModelBase, IClientViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IDialogService _dialogService;
        private readonly IAddClientViewModel _addClientViewModel;
        private readonly IAddPositionViewModel _addPositionViewModel;
        private readonly IAddIndustryViewModel _addIndustryViewModel;
        private ObservableCollection<ClientModel> _clients;
        private ObservableCollection<IndustryModel> _industries;
        private ObservableCollection<PositionModel> _positions;
        private readonly InteractionRequest<ResponseNotification> _addClientRequest;
        private readonly InteractionRequest<ResponseNotification> _addPositionRequest;
        private readonly InteractionRequest<ResponseNotification> _addIndustryRequest;

        [ImportingConstructor]
        public ClientViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IDialogService dialogService,
            IAddClientViewModel addClientViewModel,
            IAddPositionViewModel addPositionViewModel,
            IAddIndustryViewModel addIndustryViewModel)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _dialogService = dialogService;
            _addClientViewModel = addClientViewModel;
            _addPositionViewModel = addPositionViewModel;
            _addIndustryViewModel = addIndustryViewModel;

            _addClientRequest = new InteractionRequest<ResponseNotification>();
            _addPositionRequest = new InteractionRequest<ResponseNotification>();
            _addIndustryRequest = new InteractionRequest<ResponseNotification>();

            AddClientCommand = new DelegateCommand(AddClient);
            RemoveClientCommand = new DelegateCommand(RemoveClient);
            AddPositionCommand = new DelegateCommand(AddPosition);
            RemovePositionCommand = new DelegateCommand(RemovePosition);
            AddIndustryCommand = new DelegateCommand(AddIndustry);
            RemoveIndustryCommand = new DelegateCommand(RemoveIndustry);
            EditClientCommand = new DelegateCommand<ClientModel>(EditClient);
        }

        private void Load()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetClientsCoveredRequest());
            requestDispatcher.Add(new GetIndustriesRequest());
            requestDispatcher.Add(new GetPositionsRequest());

            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Clients = Mapper.Map<IEnumerable<ClientDto>, ObservableCollection<ClientModel>>(r.Get<GetClientsCoveredResponse>().Clients);
                                                      Industries = Mapper.Map<IEnumerable<IndustryDto>, ObservableCollection<IndustryModel>>(r.Get<GetIndustriesResponse>().Industries);
                                                      Positions = Mapper.Map<IEnumerable<PositionDto>, ObservableCollection<PositionModel>>(r.Get<GetPositionsResponse>().Positions);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private void LoadClients()
        {
            IsBusy = true;
            Clients.Clear();
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetClientsCoveredRequest());

            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Clients =
                                                          Mapper.Map
                                                              <IEnumerable<ClientDto>, ObservableCollection<ClientModel>
                                                                  >(r.Get<GetClientsCoveredResponse>().Clients);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private void LoadIndustries()
        {
            IsBusy = true;
            Industries.Clear();
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetIndustriesRequest());

            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Industries =
                                                          Mapper.Map
                                                              <IEnumerable<IndustryDto>,
                                                                  ObservableCollection<IndustryModel>>(
                                                                      r.Get<GetIndustriesResponse>().Industries);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private void LoadPositions()
        {
            IsBusy = true;
            Positions.Clear();
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetPositionsRequest());

            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Positions =
                                                          Mapper.Map
                                                              <IEnumerable<PositionDto>,
                                                                  ObservableCollection<PositionModel>>(
                                                                      r.Get<GetPositionsResponse>().Positions);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        public IInteractionRequest AddClientRequest
        {
            get { return _addClientRequest; }
        }

        public IInteractionRequest AddPositionRequest
        {
            get { return _addPositionRequest; }
        }

        public IInteractionRequest AddIndustryRequest
        {
            get { return _addIndustryRequest; }
        }

        public ObservableCollection<ClientModel> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                RaisePropertyChanged(() => Clients);
            }
        }

        public ObservableCollection<IndustryModel> Industries
        {
            get { return _industries; }
            set
            {
                _industries = value;
                RaisePropertyChanged(() => Industries);
            }
        }

        public ObservableCollection<PositionModel> Positions
        {
            get { return _positions; }
            set
            {
                _positions = value;
                RaisePropertyChanged(() => Positions);
            }
        }

        public IDialogService DialogService
        {
            get { return _dialogService; }
        }

        public IAddClientViewModel AddClientViewModel
        {
            get { return _addClientViewModel; }
        }

        public ICommand AddClientCommand { get; set; }
        public ICommand AddIndustryCommand { get; set; }
        public ICommand AddPositionCommand { get; set; }
        public ICommand RemoveClientCommand { get; set; }
        public ICommand RemoveIndustryCommand { get; set; }
        public ICommand RemovePositionCommand { get; set; }
        public ICommand EditClientCommand { get; set; }

        private void AddClient()
        {
            _addClientViewModel.Reset();
            _addClientViewModel.AllPositions = new ObservableCollection<PositionModel>(Positions.Clone());
            _addClientViewModel.Industries = Industries;
            _addClientRequest.Raise(
                new ResponseNotification
                    {ChildWindow = new AddClientView(), Content = _addClientViewModel, Title = "Add new client"},
                r =>
                    {
                        if (r.Result.HasValue)
                        {
                            if (r.Result.Value) LoadClients();
                        }
                    });
        }

        private void EditClient(ClientModel client)
        {
            _addClientViewModel.IsEdit = true;
            _addClientViewModel.Id = client.Id;
            _addClientViewModel.Name = client.Name;
            _addClientViewModel.Industry = Industries.Single(i => i.Name == client.IndustryName);
            _addClientViewModel.Website = client.Website;
            _addClientViewModel.Address = client.Address;
            _addClientViewModel.Contacts = client.Contacts;
            _addClientViewModel.Positions = new ObservableCollection<PositionModel>(client.Positions.Clone());
            _addClientViewModel.AllPositions = new ObservableCollection<PositionModel>(Positions.Clone().Except(client.Positions));
            _addClientViewModel.Industries = Industries;
            _addClientRequest.Raise(
                new ResponseNotification
                    {
                        ChildWindow = new AddClientView(),
                        Content = _addClientViewModel,
                        Title = string.Format("Edit {0}", client.Name)
                    },
                r =>
                    {
                        if (r.Result.HasValue)
                        {
                            if (r.Result.Value) LoadClients();
                        }
                    });
        }

        private void RemoveClient()
        {
            if (!Clients.Any(c => c.IsChecked)) return;
            _dialogService.Show(
                "Question",
                "Are you sure you want to remove the selected clients?",
                DialogType.Question,
                true,
                true,
                r =>
                    {
                        if (r)
                        {
                            IsBusy = true;
                            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
                            foreach (var client in Clients.Where(c => c.IsChecked))
                            {
                                requestDispatcher.Add(Guid.NewGuid().ToString(), new RemoveClientRequest
                                                                                     {
                                                                                         Id = client.Id
                                                                                     });
                            }
                            requestDispatcher.ProcessRequests(res => LoadClients(), e => IsBusy = false);
                        }
                    });
        }

        private void AddPosition()
        {
            _addPositionViewModel.Name = null;
            _addPositionRequest.Raise(new ResponseNotification {Content = _addPositionViewModel}, r =>
                                                                                                      {
                                                                                                          if (r.Result.HasValue)
                                                                                                          {
                                                                                                              if (r.Result.Value) LoadPositions();
                                                                                                          }
                                                                                                      });
        }

        private void RemovePosition()
        {
            if (!Positions.Any(c => c.IsChecked)) return;
            _dialogService.Show(
                "Question",
                "Are you sure you want to remove the selected positions?",
                DialogType.Question,
                true,
                true,
                r =>
                    {
                        if (r)
                        {
                            IsBusy = true;
                            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
                            foreach (var position in Positions.Where(c => c.IsChecked))
                            {
                                requestDispatcher.Add(Guid.NewGuid().ToString(), new RemovePositionRequest
                                                                                     {
                                                                                         Id = position.Id
                                                                                     });
                            }
                            requestDispatcher.ProcessRequests(res => LoadPositions(), e => IsBusy = false);
                        }
                    });
        }

        private void AddIndustry()
        {
            _addIndustryViewModel.Name = null;
            _addIndustryRequest.Raise(new ResponseNotification {Content = _addIndustryViewModel}, r =>
                                                                                                      {
                                                                                                          if (
                                                                                                              r.Result.
                                                                                                                  HasValue)
                                                                                                          {
                                                                                                              if (
                                                                                                                  r.
                                                                                                                      Result
                                                                                                                      .
                                                                                                                      Value)
                                                                                                                  LoadIndustries
                                                                                                                      ();
                                                                                                          }
                                                                                                      });
        }

        private void RemoveIndustry()
        {
            if (!Industries.Any(c => c.IsChecked)) return;
            _dialogService.Show(
                "Question",
                "Are you sure you want to remove the selected industries?",
                DialogType.Question,
                true,
                true,
                r =>
                    {
                        if (r)
                        {
                            IsBusy = true;
                            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
                            foreach (var industry in Industries.Where(c => c.IsChecked))
                            {
                                requestDispatcher.Add(Guid.NewGuid().ToString(), new RemoveIndustryRequest
                                                                                     {
                                                                                         Id = industry.Id
                                                                                     });
                            }
                            requestDispatcher.ProcessRequests(res => LoadIndustries(), e => IsBusy = false);
                        }
                    });
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
    }
}