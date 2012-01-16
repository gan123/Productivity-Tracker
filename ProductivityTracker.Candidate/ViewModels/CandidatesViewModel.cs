using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using Agatha.Common;
using AutoMapper;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Candidate.Interfaces;
using ProductivityTracker.Candidate.Models;
using ProductivityTracker.Candidate.Views;
using ProductivityTracker.Common;
using ProductivityTracker.Controls;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Candidate.ViewModels
{
    [Export(typeof(ICandidatesViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public class CandidatesViewModel : ViewModelBase, ICandidatesViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IAddViewModel _addViewModel;
        private readonly IDialogService _dialogService;
        private ObservableCollection<CandidateModel> _candidates;
        private readonly InteractionRequest<ResponseNotification> _addRequest;
        private bool _isAdmin;

        [ImportingConstructor]
        public CandidatesViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IAddViewModel addViewModel,
            IDialogService dialogService)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _addViewModel = addViewModel;
            _dialogService = dialogService;
            _addRequest = new InteractionRequest<ResponseNotification>();

            AddCommand = new DelegateCommand(Add);
            RemoveCommand = new DelegateCommand(Remove);
        }

        private void Load()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetCandidatesRequest());
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Candidates = Mapper.Map<IEnumerable<CandidateDto>, ObservableCollection<CandidateModel>>(r.Get<GetCandidatesResponse>().Candidates);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        public ObservableCollection<CandidateModel> Candidates
        {
            get { return _candidates; }
            set
            {
                _candidates = value;
                RaisePropertyChanged(() => Candidates);
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                RaisePropertyChanged(() => IsAdmin);
            }
        }

        public IInteractionRequest AddRequest
        {
            get { return _addRequest; }
        }

        public IDialogService DialogService
        {
            get { return _dialogService; }
        }

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }

        private void Add()
        {
            _addViewModel.Reset();
            _addRequest.Raise(new ResponseNotification{ChildWindow = new AddView(), Content =_addViewModel},
                r =>
                    {
                        if (!r.Result.HasValue) return;
                        if (r.Result.Value)
                        {
                            Thread.Sleep(500);
                            Load();
                        }
                    });
        }

        private void Remove()
        {
            if (!Candidates.Any(c => c.IsChecked)) return;
            _dialogService.Show(
                "Question",
                "Are you sure you want to remove the selected candidates?",
                DialogType.Question,
                true,
                true,
                r =>
                {
                    if (r)
                    {
                        IsBusy = true;
                        var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
                        foreach (var candidate in Candidates.Where(c => c.IsChecked))
                        {
                            requestDispatcher.Add(Guid.NewGuid().ToString(), new RemoveCandidateRequest
                            {
                                Id = candidate.Id
                            });
                        }
                        requestDispatcher.ProcessRequests(res => Load(), e => IsBusy = false);
                    }
                });
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            IsAdmin = Convert.ToBoolean(navigationContext.Parameters["IsAdmin"]);
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