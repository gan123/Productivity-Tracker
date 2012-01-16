using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Analyse.Models;

namespace ProductivityTracker.Analyse.Interfaces
{
    public interface IAnalyseViewModel : INavigationAware
    {
        CollectionViewSource Productivities { get; set; }
        IEnumerable<StatusModel> Statuses { get; set; }

        ICommand UpdateStatusCommand { get; set; }
    }
}