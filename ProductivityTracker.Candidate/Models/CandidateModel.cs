using Microsoft.Practices.Prism.ViewModel;

namespace ProductivityTracker.Candidate.Models
{
    public class CandidateModel : NotificationObject
    {
        private bool _isChecked;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string CurrentCtc { get; set; }
        public string ExpectedCtc { get; set; }
        public string NoticePeriod { get; set; }
        public string Position { get; set; }
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