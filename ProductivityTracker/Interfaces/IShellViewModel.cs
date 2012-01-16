using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;

namespace ProductivityTracker.Interfaces
{
    public interface IShellViewModel
    {
        bool IsAuthenticated { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        IEnumerable<TabNavigationItem> TabItems { get; set; }
        void Load();
        DelegateCommand LoginCommand { get; set; }
        ICommand LoadDataMiningViewCommand { get; set; }
        ICommand LoadRecruitersViewCommand { get; set; }
        ICommand LoadClientsViewCommand { get; set; }
        ICommand LoadAddDataViewCommand { get; set; }
        ICommand LoadCandidatesViewCommand { get; set; }

        System.Windows.Threading.Dispatcher Dispatcher { get; set; }
    }
}