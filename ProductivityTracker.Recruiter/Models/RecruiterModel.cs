using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Agatha.Common;
using Microsoft.Practices.Prism.ViewModel;
using ProductivityTracker.Controls;
using ProductivityTracker.Services.RequestResponse.Commands;

namespace ProductivityTracker.Recruiter.Models
{
    public class RecruiterModel : NotificationObject, IEditableObject, IDataErrorInfo, ICanSave
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private bool _isEditing;
        private string _fullName;
        private string _email;
        private string _contact;
        private string _designation;
        private bool _isBusy;
        private string _fullNameBackup;
        private string _emailBackup;
        private string _contactBackup;
        private string _designationBackup;

        public RecruiterModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
        }

        public string Id { get; set; }
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                RaisePropertyChanged(() => FullName);
            }
        }

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        public int? Age
        {
            get
            {
                if (DateOfBirth.HasValue)
                    return DateTime.Now.Year - DateOfBirth.Value.Year;
                return null;
            }
        }

        public DateTime? DateOfBirth { get; set; }
        public string Contact
        {
            get { return _contact; }
            set
            {
                _contact = value;
                RaisePropertyChanged(() => Contact);
            }
        }

        public string Designation
        {
            get { return _designation; }
            set
            {
                _designation = value;
                RaisePropertyChanged(() => Designation);
            }
        }

        public DateTime DateOfJoining { get; set; }
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public void BeginEdit()
        {
            if (!_isEditing)
            {
                _fullNameBackup = FullName;
                _emailBackup = Email;
                _contactBackup = Contact;
                _designationBackup = Designation;
                _isEditing = true;
            }
        }

        public void EndEdit()
        {
            if (_isEditing)
            {
                IsBusy = false;
                var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
                requestDispatcher.Add(new UpdateRecruiterRequest
                                          {
                                              Contact = Contact,
                                              Designation = Designation,
                                              Email = Email,
                                              Id = Id,
                                              Name = FullName
                                          });
                requestDispatcher.ProcessRequests(r => IsBusy = false, e => IsBusy = false);
                _isEditing = false;
            }
        }

        public void CancelEdit()
        {
            if (_isEditing)
            {
                FullName = _fullNameBackup;
                Contact = _contactBackup;
                Email = _emailBackup;
                Designation = _designationBackup;
                _isEditing = false;
            }
        }

        public string this[string columnName]
        {
            get
            {
                string error = null;
                switch (columnName)
                {
                    case "Name":
                        if (string.IsNullOrEmpty(FullName))
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
                    case "Contact":
                        if (string.IsNullOrEmpty(Contact))
                            error = "Please provide a phone number";
                        break;
                }
                return error;
            }
        }

        public string Error
        {
            get { return null; }
        }

        public bool CanSave()
        {
            return !string.IsNullOrEmpty(FullName) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Contact) && !string.IsNullOrEmpty(Designation);
        }
    }
}