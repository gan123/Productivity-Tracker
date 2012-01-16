using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Candidate.Models;
using ProductivityTracker.Controls;

namespace ProductivityTracker.Candidate.Interfaces
{
    public interface ICandidatesViewModel : INavigationAware
    {
        ObservableCollection<CandidateModel> Candidates { get; set; }
        bool IsAdmin { get; set; }
        IDialogService DialogService { get; }
        ICommand AddCommand { get; set; }
        ICommand RemoveCommand { get; set; }
    }
}