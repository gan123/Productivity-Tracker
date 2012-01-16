using Microsoft.Practices.Prism.ViewModel;

namespace ProductivityTracker.Ui.Common
{
    public class ViewModelBase : NotificationObject
    {
        private bool _isBusy;
        private bool _isDirty;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                RaisePropertyChanged(() => IsDirty);
            }
        }
    }

    public class ChildWindowViewModelBase : ViewModelBase
    {
        private bool? _result;

        public bool? Result
        {
            get { return _result; }
            set
            {
                _result = value;
                RaisePropertyChanged(() => Result);
            }
        }
    }
}
