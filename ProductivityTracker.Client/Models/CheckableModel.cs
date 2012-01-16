using Microsoft.Practices.Prism.ViewModel;

namespace ProductivityTracker.Client.Models
{
    public class CheckableModel : NotificationObject
    {
        private bool _isChecked;

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                RaisePropertyChanged(() => IsChecked);
            }
        }
    }
}