using System.Collections.Generic;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Common;

namespace ProductivityTracker.Interfaces
{
    public interface IShellViewModel
    {
        bool IsAuthenticated { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        IEnumerable<TabNavigationItem> TabItems { get; set; }
        bool HasLoginFailed { get; set; }
        RecruiterDto LoggedInUser { get; set; }
        void Load();
        DelegateCommand LoginCommand { get; set; }
        ICommand LoadDataMiningViewCommand { get; set; }
        ICommand LoadRecruitersViewCommand { get; set; }
        ICommand LoadClientsViewCommand { get; set; }
        ICommand LoadAddDataViewCommand { get; set; }
        ICommand LoadCandidatesViewCommand { get; set; }
        ICommand ChangePasswordCommand { get; set; }

        System.Windows.Threading.Dispatcher Dispatcher { get; set; }
    }
}