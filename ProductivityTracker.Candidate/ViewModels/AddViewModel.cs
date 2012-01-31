using System;
using System.ComponentModel.Composition;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Candidate.Interfaces;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Candidate.ViewModels
{
    [Export(typeof(IAddViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddViewModel : ChildWindowViewModelBase, IAddViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private bool _isEditing;
        private string _noticePeriod;
        private string _name;
        private string _company;
        private string _contact;
        private string _currentCtc;
        private string _expectedCtc;
        private string _position;

        [ImportingConstructor]
        public AddViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            SaveCommand = new DelegateCommand(Save, CanSave);
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(Name))
                            error = "Please provide a name";
                        break;
                    case "Company":
                        if (string.IsNullOrEmpty(Company))
                            error = "Please provide a company name";
                        break;
                    case "Position":
                        if (string.IsNullOrEmpty(Position))
                            error = "Please provide a position";
                        break;
                    case "Contact":
                        if (string.IsNullOrEmpty(Contact))
                            error = "Please provide a contact number";
                        break;
                    case "CurrentCtc":
                        if (string.IsNullOrEmpty(CurrentCtc))
                            error = "Please provide the current ctc";
                        break;
                    case "ExpectedCtc":
                        if (string.IsNullOrEmpty(ExpectedCtc))
                            error = "Please provide the expected ctc";
                        break;
                    case "NoticePeriod":
                        if (string.IsNullOrEmpty(NoticePeriod))
                            error = "Please enter the notice period";
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { return null; }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Company
        {
            get { return _company; }
            set
            {
                _company = value;
                RaisePropertyChanged(() => Company);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                RaisePropertyChanged(() => Contact);
            }
        }

        public string CurrentCtc
        {
            get { return _currentCtc; }
            set
            {
                _currentCtc = value;
                RaisePropertyChanged(() => CurrentCtc);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string ExpectedCtc
        {
            get { return _expectedCtc; }
            set
            {
                _expectedCtc = value;
                RaisePropertyChanged(() => ExpectedCtc);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string NoticePeriod
        {
            get { return _noticePeriod; }
            set
            {
                _noticePeriod = value;
                RaisePropertyChanged(() => NoticePeriod);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                RaisePropertyChanged(() => Position);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Id { get; set; }

        public bool IsEditing
        {
            get { return _isEditing; }
            set
            {
                _isEditing = value;
                RaisePropertyChanged(() => IsEditing);
            }
        }

        public void Reset()
        {
            Name = null;
            Company = null;
            Contact = null;
            CurrentCtc = null;
            ExpectedCtc = null;
            NoticePeriod = null;
        }

        public DelegateCommand SaveCommand { get; set; }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Company) && !string.IsNullOrEmpty(Contact)
                   && !string.IsNullOrEmpty(ExpectedCtc) && !string.IsNullOrEmpty(CurrentCtc) &&
                   !string.IsNullOrEmpty(NoticePeriod) && !string.IsNullOrEmpty(Position);
        }

        private void Save()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            if (IsEditing)
                requestDispatcher.Add(new UpdateCandidateRequest
                                          {
                                              Id = Id,
                                              Company = Company,
                                              Contact = Contact,
                                              CurrentCtc = CurrentCtc,
                                              ExpectedCtc = ExpectedCtc,
                                              Name = Name,
                                              NoticePeriod = NoticePeriod,
                                              Position = Position
                                          });
            else requestDispatcher.Add(new AddCandidateRequest
                                           {
                                               Company = Company,
                                               Contact = Contact,
                                               CurrentCtc = CurrentCtc,
                                               ExpectedCtc = ExpectedCtc,
                                               Name = Name,
                                               NoticePeriod = NoticePeriod,
                                               Position = Position
                                           });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Result = true;
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }
    }
}