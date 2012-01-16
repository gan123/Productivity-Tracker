using System.ComponentModel.Composition;
using System.Windows.Controls;
using ProductivityTracker.Recruiter.Interfaces;

namespace ProductivityTracker.Recruiter.Views
{
    [Export("ProductivityTracker.Recruiter.Views.RecruitersView")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class RecruitersView : UserControl
    {
        [ImportingConstructor]
        public RecruitersView(
            IRecruitersViewModel recruitersViewModel)
        {
            InitializeComponent();
            DataContext = recruitersViewModel;
            LayoutUpdated += (s, e) => Model.DialogService.ParentGrid = ParentGrid;
        }

        public IRecruitersViewModel Model
        {
            get { return (IRecruitersViewModel) DataContext; }
        }
    }
}
