using System.ComponentModel.Composition;
using System.Windows.Controls;
using ProductivityTracker.Candidate.Interfaces;

namespace ProductivityTracker.Candidate.Views
{
    [Export("ProductivityTracker.Candidate.Views.CandidatesView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class CandidatesView : UserControl
    {
        [ImportingConstructor]
        public CandidatesView(
            ICandidatesViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
            LayoutUpdated += (s, e) => Model.DialogService.ParentGrid = MainGrid;
        }

        public ICandidatesViewModel Model
        {
            get { return (ICandidatesViewModel) DataContext; }
        }
    }
}
