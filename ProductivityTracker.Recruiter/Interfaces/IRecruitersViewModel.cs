using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Controls;
using ProductivityTracker.Recruiter.Models;

namespace ProductivityTracker.Recruiter.Interfaces
{
    public interface IRecruitersViewModel : INavigationAware
    {
        ObservableCollection<RecruiterModel> Recruiters { get; set; }
        ICommand AddCommand { get; set; }
        ICommand RemoveCommand { get; set; }
        ICommand EditCommand { get; set; }
        ICommand MaximizeCommand { get; set; }
        IDialogService DialogService { get; }
        bool IsAdmin { get; set; }
    }
}
