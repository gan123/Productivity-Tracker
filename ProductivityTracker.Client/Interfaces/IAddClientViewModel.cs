using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Client.Models;

namespace ProductivityTracker.Client.Interfaces
{
    public interface IAddClientViewModel : IDataErrorInfo
    {
        string Name { get; set; }
        IndustryModel Industry { get; set; }
        string Address { get; set; }
        string Website { get; set; }
        string Contacts { get; set; }
        bool IsEdit { get; set; }
        string Id { get; set; }
        ObservableCollection<PositionModel> Positions { get; set; }
        ObservableCollection<IndustryModel> Industries { get; set; }
        ObservableCollection<PositionModel> AllPositions { get; set; }

        void Reset();
        DelegateCommand SaveCommand { get; set; }
    }
}