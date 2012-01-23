using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Agatha.Common;
using AutoMapper;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Common;
using ProductivityTracker.Controls.Search;
using ProductivityTracker.Productivity.Data.Interfaces;
using ProductivityTracker.Productivity.Data.Models;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Services.RequestResponse.Queries;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Productivity.Data.ViewModels
{
    [Export(typeof(IDataEntryViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    [RegionMemberLifetime(KeepAlive = false)]
    public class DataEntryViewModel : ViewModelBase, IDataEntryViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private IEnumerable<StatusModel> _statuses;
        private StatusModel _statusModel;
        private IEnumerable<RecruiterModel> _recruiters;
        private RecruiterModel _recruiter;
        private ClientSearch _client;
        private DateTime? _dateSent;
        private DateTime? _expectedFeedbackDate;
        private ClientPosition _position;
        private CandidateSearch _candidate;
        private bool _isPositionEnabled;
        private string _comments;

        [ImportingConstructor]
        public DataEntryViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        private void Load()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new GetStatusesRequest());
            requestDispatcher.Add(new GetRecruitersRequest());
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Statuses = Mapper.Map<IEnumerable<StatusDto>, IEnumerable<StatusModel>>(r.Get<GetStatusesResponse>().Statuses);
                                                      Recruiters = Mapper.Map<IEnumerable<RecruiterDto>, IEnumerable<RecruiterModel>>(r.Get<GetRecruitersResponse>().Recruiters);
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
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

        public IEnumerable<StatusModel> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                RaisePropertyChanged(() => Statuses);
            }
        }

        public StatusModel StatusModel
        {
            get { return _statusModel; }
            set
            {
                _statusModel = value;
                RaisePropertyChanged(() => StatusModel);
                SaveCommand.RaiseCanExecuteChanged();
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
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public ClientSearch Client
        {
            get { return _client; }
            set
            {
                _client = value;
                RaisePropertyChanged(() => Client);
                IsPositionEnabled = Client != null && Client.Positions.Any();
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime? DateSent
        {
            get { return _dateSent; }
            set
            {
                _dateSent = value;
                RaisePropertyChanged(() => DateSent);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime? ExpectedFeedbackDate
        {
            get { return _expectedFeedbackDate; }
            set
            {
                _expectedFeedbackDate = value;
                RaisePropertyChanged(() => ExpectedFeedbackDate);
            }
        }

        public ClientPosition Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged(() => Position);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public CandidateSearch Candidate
        {
            get { return _candidate; }
            set
            {
                _candidate = value;
                RaisePropertyChanged(() => Candidate);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Comments
        {
            get { return _comments; }
            set
            {
                _comments = value;
                RaisePropertyChanged(() => Comments);
            }
        }

        public bool IsPositionEnabled
        {
            get { return _isPositionEnabled; }
            set
            {
                _isPositionEnabled = value;
                RaisePropertyChanged(() => IsPositionEnabled);
            }
        }

        public DelegateCommand SaveCommand { get; set; }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Client":
                        if (Client == null)
                            error = "Please select a client";
                        break;
                    case "Recruiter":
                        if (Recruiter == null)
                            error = "Please select a recruiter";
                        break;
                    case "Position":
                        if (Position == null)
                            error = "Please select a position";
                        break;
                    case "StatusModel":
                        if (StatusModel == null)
                            error = "Please select a status";
                        break;
                    case "Candidate":
                        if (Candidate == null)
                            error = "Please select a candidate";
                        break;
                    case "DateSent":
                        if (DateSent == null)
                            error = "Please select the date";
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { return null; }
        }

        private void Save()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new AddProductivityRequest
                                      {
                                          CandidateId = Candidate.Id,
                                          ClientId = Client.Id,
                                          Comments = Comments,
                                          DateSent = DateSent,
                                          ExpectedFeedbackDate = ExpectedFeedbackDate,
                                          PositionId = Position.Id,
                                          RecruiterId = Recruiter.Id,
                                          StatusId = StatusModel.Id
                                      });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      IsBusy = false;
                                                      Client = null;
                                                      Candidate = null;
                                                      Recruiter = null;
                                                      Position = null;
                                                      StatusModel = null;
                                                      Comments = null;
                                                      DateSent = null;
                                                      ExpectedFeedbackDate = null;
                                                  }, e => IsBusy = false);
        }

        private bool CanSave()
        {
            return Client != null && Position != null && Recruiter != null && Candidate != null && DateSent != null &&
                   StatusModel != null;
        }
    }
}
