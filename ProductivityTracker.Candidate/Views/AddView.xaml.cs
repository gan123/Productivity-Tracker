using System.Windows;

namespace ProductivityTracker.Candidate.Views
{
    public partial class AddView
    {
        public AddView()
        {
            InitializeComponent();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
