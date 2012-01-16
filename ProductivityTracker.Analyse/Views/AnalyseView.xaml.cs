using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using ProductivityTracker.Analyse.Interfaces;

namespace ProductivityTracker.Analyse.Views
{
    [Export("ProductivityTracker.Analyse.Views.AnalyseView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class AnalyseView : UserControl
    {
        [ImportingConstructor]
        public AnalyseView(
            IAnalyseViewModel analyseViewModel)
        {
            InitializeComponent();
            DataContext = analyseViewModel;
        }
    }
}
