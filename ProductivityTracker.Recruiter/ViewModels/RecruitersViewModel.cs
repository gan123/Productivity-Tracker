using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Agatha.Common;
using AutoMapper;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Common;
using ProductivityTracker.Controls;
using ProductivityTracker.Recruiter.Interfaces;
using ProductivityTracker.Recruiter.Models;
using ProductivityTracker.Recruiter.Views;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Recruiter.ViewModels
{
    [Export(typeof(IRecruitersViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public class RecruitersViewModel : ViewModelBase, IRecruitersViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly IDialogService _dialogService;
        private readonly IAddViewModel _addViewModel;
        private readonly IDetailsViewModel _detailsViewModel;
        private readonly InteractionRequest<ResponseNotification> _addRequest;
        private readonly InteractionRequest<Notification> _summaryRequest;
        private ObservableCollection<RecruiterModel> _recruiters;
        private bool _isAdmin;

        [ImportingConstructor]
        public RecruitersViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            IDialogService dialogService,
            IAddViewModel addViewModel,
            IDetailsViewModel detailsViewModel)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _dialogService = dialogService;
            _addViewModel = addViewModel;
            _detailsViewModel = detailsViewModel;
            _addRequest = new InteractionRequest<ResponseNotification>();
            _summaryRequest = new InteractionRequest<Notification>();

            AddCommand = new DelegateCommand(Add);
            RemoveCommand = new DelegateCommand<RecruiterModel>(Remove);
            MaximizeCommand = new DelegateCommand<RecruiterModel>(Maximize);
        }

        private void Load()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetRecruitersRequest());

            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Recruiters = Mapper.Map<IEnumerable<RecruiterDto>, ObservableCollection<RecruiterModel>>(r.Get<GetRecruitersResponse>().Recruiters);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        public IInteractionRequest AddRequest
        {
            get { return _addRequest; }
        }

        public IInteractionRequest SummaryRequest
        {
            get { return _summaryRequest; }
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

        public ObservableCollection<RecruiterModel> Recruiters
        {
            get { return _recruiters; }
            set
            {
                _recruiters = value;
                RaisePropertyChanged(() => Recruiters);
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand RemoveCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand MaximizeCommand { get; set; }

        public IDialogService DialogService
        {
            get { return _dialogService; }
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

        private void Add()
        {
            _addViewModel.Reset();
            _addRequest.Raise(new ResponseNotification {ChildWindow = new AddView(), Title = "Add new recruiter", Content = _addViewModel }, n =>
                                                                                           {
                                                                                               if (n.Result.HasValue)
                                                                                                   Load();
                                                                                           });
        }

        private void Maximize(RecruiterModel model)
        {
            _detailsViewModel.Load(model.Id);
            _summaryRequest.Raise(new Notification {Content = _detailsViewModel});
        }

        private void Remove(RecruiterModel model)
        {
            _dialogService.Show(
                "Question",
                "Are you sure you want to delete?",
                DialogType.Question,
                true,
                true,
                r =>
                    {
                        if (r)
                        {
                            IsBusy = true;
                            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
                            requestDispatcher.Add(new RemoveRecruiterRequest {RecruiterId = model.Id});
                            requestDispatcher.ProcessRequests(res => Load(), e => IsBusy = false);
                        }
                    });
        }
    }
}