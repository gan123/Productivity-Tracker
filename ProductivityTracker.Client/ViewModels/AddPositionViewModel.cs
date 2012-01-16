using System.ComponentModel.Composition;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Client.Interfaces;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Client.ViewModels
{
    [Export(typeof(IAddPositionViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddPositionViewModel : ChildWindowViewModelBase, IAddPositionViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private string _name;

        [ImportingConstructor]
        public AddPositionViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

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

        public DelegateCommand SaveCommand { get; set; }

        private void Save()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new AddPositionRequest {Name = Name});
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Result = true;
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }

    [Export(typeof(IAddIndustryViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AddIndustryViewModel : ChildWindowViewModelBase, IAddIndustryViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private string _name;

        [ImportingConstructor]
        public AddIndustryViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

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

        public DelegateCommand SaveCommand { get; set; }

        private void Save()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new AddIndustryRequest { Name = Name });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Result = true;
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }

        private bool CanSave()
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}