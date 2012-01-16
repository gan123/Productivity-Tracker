using System.Windows;

namespace ProductivityTracker.Analyse.Views
{
    /// <summary>
    /// Interaction logic for UpdateStatusView.xaml
    /// </summary>
    public partial class UpdateStatusView
    {
        public UpdateStatusView()
        {
            InitializeComponent();
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
