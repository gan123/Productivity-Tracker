using System.Collections.Generic;
using System.Windows.Input;
using ProductivityTracker.Analyse.Models;

namespace ProductivityTracker.Analyse.Interfaces
{
    public interface IUpdateStatusViewModel
    {
        IEnumerable<StatusModel> Statuses { get; set; }
        StatusModel Status { get; set; }
        string Comments { get; set; }
        string Id { get; set; }

        ICommand SaveCommand { get; set; }
    }
}