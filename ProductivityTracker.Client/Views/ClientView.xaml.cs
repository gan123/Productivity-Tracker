using System.ComponentModel.Composition;
using System.Windows.Controls;
using ProductivityTracker.Client.Interfaces;

namespace ProductivityTracker.Client.Views
{
    [Export("ProductivityTracker.Client.Views.ClientView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ClientView : UserControl
    {
        [ImportingConstructor]
        public ClientView(
            IClientViewModel clientViewModel)
        {
            InitializeComponent();
            DataContext = clientViewModel;
            LayoutUpdated += (s, e) => Model.DialogService.ParentGrid = MainGrid;
        }

        public IClientViewModel Model
        {
            get { return (IClientViewModel) DataContext; }
        }
    }
}
