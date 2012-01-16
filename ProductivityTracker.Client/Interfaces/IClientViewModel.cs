using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Client.Models;
using ProductivityTracker.Controls;

namespace ProductivityTracker.Client.Interfaces
{
    public interface IClientViewModel : INavigationAware
    {
        ObservableCollection<ClientModel> Clients { get; set; }
        ObservableCollection<IndustryModel> Industries { get; set; }
        ObservableCollection<PositionModel> Positions { get; set; }
        IDialogService DialogService { get; }
        IAddClientViewModel AddClientViewModel { get; }

        ICommand AddClientCommand { get; set; }
        ICommand AddIndustryCommand { get; set; }
        ICommand AddPositionCommand { get; set; }
        ICommand RemoveClientCommand { get; set; }
        ICommand RemoveIndustryCommand { get; set; }
        ICommand RemovePositionCommand { get; set; }
        ICommand EditClientCommand { get; set; }
    }
}