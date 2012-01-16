using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using Agatha.Common;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Analyse.Interfaces;
using ProductivityTracker.Analyse.Models;
using ProductivityTracker.Services.RequestResponse.Commands;
using ProductivityTracker.Ui.Common;

namespace ProductivityTracker.Analyse.ViewModels
{
    [Export(typeof(IUpdateStatusViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class UpdateStatusViewModel : ChildWindowViewModelBase, IUpdateStatusViewModel
    {
        private readonly IAsyncRequestDispatcherFactory _asyncRequestDispatcherFactory;
        private IEnumerable<StatusModel> _statuses;
        private StatusModel _status;
        private string _comments;

        [ImportingConstructor]
        public UpdateStatusViewModel(
            IAsyncRequestDispatcherFactory asyncRequestDispatcherFactory)
        {
            _asyncRequestDispatcherFactory = asyncRequestDispatcherFactory;

            SaveCommand = new DelegateCommand(Save);
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

        public StatusModel Status
        {
            get { return _status; }
            set
            {
                _status = value;
                RaisePropertyChanged(() => Status);
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

        public string Id { get; set; }

        public ICommand SaveCommand { get; set; }

        private void Save()
        {
            IsBusy = true;
            var requestDispatcher = _asyncRequestDispatcherFactory.CreateAsyncRequestDispatcher();
            requestDispatcher.Add(new UpdateStatusAndCommentsRequest
                                      {
                                          Comments = Comments,
                                          StatusId = Status.Id,
                                          StatusName = Status.Name,
                                          Id = Id
                                      });
            requestDispatcher.ProcessRequests(r =>
                                                  {
                                                      Result = true;
                                                      IsBusy = false;
                                                  }, e => IsBusy = false);
        }
    }
}