using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;

namespace ProductivityTracker.Candidate.Interfaces
{
    public interface IAddViewModel : IDataErrorInfo
    {
        string Name { get; set; }
        string Company { get; set; }
        string Contact { get; set; }
        string CurrentCtc { get; set; }
        string ExpectedCtc { get; set; }
        string NoticePeriod { get; set; }
        string Id { get; set; }
        bool IsEditing { get; set; }
        void Reset();

        DelegateCommand SaveCommand { get; set; }
    }
}