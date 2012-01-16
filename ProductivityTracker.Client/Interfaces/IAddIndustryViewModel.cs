using Microsoft.Practices.Prism.Commands;

namespace ProductivityTracker.Client.Interfaces
{
    public interface IAddIndustryViewModel
    {
        string Name { get; set; }
        DelegateCommand SaveCommand { get; set; }
    }
}