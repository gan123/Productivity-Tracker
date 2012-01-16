using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using ProductivityTracker.Interfaces;

namespace ProductivityTracker.Views
{
    [Export]
    public partial class ShellView : Window
    {
        [ImportingConstructor]
        public ShellView(
            IShellViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.Dispatcher = Dispatcher;
        }

        public IShellViewModel Model
        {
            get { return (IShellViewModel) DataContext; }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Model.Password = ((PasswordBox) sender).Password;
        }
    }
}
