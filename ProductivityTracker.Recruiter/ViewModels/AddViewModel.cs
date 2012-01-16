using System;
using System.ComponentModel.Composition;
using System.Text.RegularExpressions;
using System.Threading;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Recruiter.Interfaces;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Ui.Common;
using ProductivityTracker.Ui.Common.Security;

namespace ProductivityTracker.Recruiter.ViewModels
{
    [Export(typeof(IAddViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddViewModel : ChildWindowViewModelBase, IAddViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private readonly ISecurityService _securityService;
        private string _name;
        private string _email;
        private string _phone;
        private DateTime? _dateOfBirth;
        private DateTime? _dateOfJoining;
        private string _designation;
        private bool _isAdmin;

        [ImportingConstructor]
        public AddViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory,
            ISecurityService securityService)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            _securityService = securityService;

            SaveCommand = new DelegateCommand(Save, CanSave);
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

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged(() => Phone);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Designation
        {
            get { return _designation; }
            set
            {
                _designation = value;
                RaisePropertyChanged(() => Designation);
                SaveCommand.RaiseCanExecuteChanged();
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

        public bool ShowAdminOption
        {
            get { return _securityService.IsAuthenticated; }
        }

        public DateTime? DateOfBirth
        {
            get { return _dateOfBirth; }
            set
            {
                _dateOfBirth = value;
                RaisePropertyChanged(() => DateOfBirth);
            }
        }

        public DateTime? DateOfJoining
        {
            get { return _dateOfJoining; }
            set
            {
                _dateOfJoining = value;
                RaisePropertyChanged(() => DateOfJoining);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public DelegateCommand SaveCommand { get; set; }

        public void Reset()
        {
            Name = null;
            Email = null;
            Phone = null;
            DateOfBirth = null;
            DateOfJoining = null;
        }

        private void Save()
        {
            IsBusy = true;

            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new AddRecruiterRequest
                                      {
                                          Contact = Phone,
                                          Email = Email,
                                          DateOfBirth = DateOfBirth,
                                          DateOfJoining = DateOfJoining.Value,
                                          FullName = Name,
                                          Designation = Designation
                                      });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Thread.Sleep(500);
                                                      Result = true;
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Phone) && DateOfJoining.HasValue;
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
                    case "Designation":
                        if (string.IsNullOrEmpty(Designation))
                            error = "Please provide a designation";
                        break;
                    case "Email":
                        if (string.IsNullOrEmpty(Email))
                            error = "Please provide a email";
                        else if (!Regex.IsMatch(Email, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                            error = "Please provide a valid email";
                        break;
                    case "Phone":
                        if (string.IsNullOrEmpty(Phone))
                            error = "Please provide a phone number";
                        break;
                    case "DateOfJoining":
                        if (DateOfJoining == null)
                            error = "Please provide the date of joining";
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { return null; }
        }
    }
}