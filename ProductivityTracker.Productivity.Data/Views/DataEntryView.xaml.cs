using System.ComponentModel.Composition;
using System.Windows.Controls;
using ProductivityTracker.Productivity.Data.Interfaces;

namespace ProductivityTracker.Productivity.Data.Views
{
    [Export("ProductivityTracker.Productivity.Data.Views.DataEntryView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class DataEntryView : UserControl
    {
        [ImportingConstructor]
        public DataEntryView(
            IDataEntryViewModel dataEntryViewModel)
        {
            InitializeComponent();
            DataContext = dataEntryViewModel;
        }
    }
}
