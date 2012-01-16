using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Client.Interfaces;
using ProductivityTracker.Client.Models;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Client.ViewModels
{
    [Export(typeof(IAddClientViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddClientViewModel : ChildWindowViewModelBase, IAddClientViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private string _name;
        private IndustryModel _industry;
        private string _address;
        private string _website;
        private string _contacts;
        private ObservableCollection<PositionModel> _positions;
        private ObservableCollection<IndustryModel> _industries;
        private ObservableCollection<PositionModel> _allPositions;

        [ImportingConstructor]
        public AddClientViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;
            AllPositions = new ObservableCollection<PositionModel>();
            Positions = new ObservableCollection<PositionModel>();

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
                    case "Industry":
                        if (Industry == null)
                            error = "Please select an industry";
                        break;
                    case "Website":
                        if (string.IsNullOrEmpty(Website))
                            error = "Please provide a website";
                        break;
                    case "Address":
                        if (string.IsNullOrEmpty(Address))
                            error = "Please provide a address";
                        break;
                    case "Contacts":
                        if (string.IsNullOrEmpty(Contacts))
                            error = "Please provide some contact";
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

        public IndustryModel Industry
        {
            get { return _industry; }
            set
            {
                _industry = value;
                RaisePropertyChanged(() => Industry);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                RaisePropertyChanged(() => Address);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Website
        {
            get { return _website; }
            set
            {
                _website = value;
                RaisePropertyChanged(() => Website);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public string Contacts
        {
            get { return _contacts; }
            set
            {
                _contacts = value;
                RaisePropertyChanged(() => Contacts);
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsEdit { get; set; }

        public string Id { get; set; }

        public ObservableCollection<PositionModel> Positions
        {
            get { return _positions; }
            set
            {
                _positions = value;
                RaisePropertyChanged(() => Positions);
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

        public ObservableCollection<PositionModel> AllPositions
        {
            get { return _allPositions; }
            set
            {
                _allPositions = value;
                RaisePropertyChanged(() => AllPositions);
            }
        }

        public void Reset()
        {
            Name = null;
            Industry = null;
            Website = null;
            Address = null;
            Contacts = null;
            Positions = new ObservableCollection<PositionModel>();
        }

        public DelegateCommand SaveCommand { get; set; }

        private void Save()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            if (!IsEdit)
                requestDispatcher.Add(new AddClientRequest
                                          {
                                              Name = Name,
                                              Address = Address,
                                              Contacts = Contacts,
                                              IndustryId = Industry.Id,
                                              PositionIds = Positions.Select(p => p.Id).ToList(),
                                              Website = Website
                                          });
            else
                requestDispatcher.Add(new UpdateClientRequest
                                          {
                                              Id = Id,
                                              Name = Name,
                                              Address = Address,
                                              Contacts = Contacts,
                                              IndustryId = Industry.Id,
                                              PositionIds = Positions.Select(p => p.Id).ToList(),
                                              Website = Website
                                          });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Result = true;
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Website) && Industry != null &&
                !string.IsNullOrEmpty(Address) && !string.IsNullOrEmpty(Contacts);
        }
    }
}