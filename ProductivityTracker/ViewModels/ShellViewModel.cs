using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Input;
using System.Windows.Threading;
using Microsoft.Practices.Prism.Commands;
using ProductivityTracker.Common;
using ProductivityTracker.Interfaces;
using ProductivityTracker.Navigation;
using ProductivityTracker.Ui.Common;
using ProductivityTracker.Ui.Common.Security;

namespace ProductivityTracker.ViewModels
{
    [Export(typeof(IShellViewModel))]
    public class ShellViewModel : ViewModelBase, IShellViewModel
    {
        private readonly ISecurityService _securityService;
        private readonly INavigationService _navigationService;
        private bool _isAuthenticated;
        private string _login;
        private string _password;
        private IEnumerable<TabNavigationItem> _tabItems;
        private bool _hasLoginFailed;
        private RecruiterDto _loggedInUser;

        private const string RecruitersViewExportName = "ProductivityTracker.Recruiter.Views.RecruitersView";
        private const string ClientViewExportName = "ProductivityTracker.Client.Views.ClientView";
        private const string CandidatesViewExportName = "ProductivityTracker.Candidate.Views.CandidatesView";
        private const string DataEntryViewExportName = "ProductivityTracker.Productivity.Data.Views.DataEntryView";
        private const string AnalyseViewExportName = "ProductivityTracker.Analyse.Views.AnalyseView";
        private const string MainRegion = "MainRegion";
        
        [ImportingConstructor]
        public ShellViewModel(
            ISecurityService securityService,
            INavigationService navigationService)
        {
            _securityService = securityService;
            _navigationService = navigationService;

            LoadRecruitersViewCommand = new DelegateCommand(LoadRecruitersView);
            LoadClientsViewCommand = new DelegateCommand(LoadClientsView);
            LoadCandidatesViewCommand = new DelegateCommand(LoadCandidatesView);
            LoadAddDataViewCommand = new DelegateCommand(LoadAddDataView);
            LoginCommand = new DelegateCommand(UserLogin, CanLogin);
            LoadDataMiningViewCommand = new DelegateCommand(LoadDataMiningView);
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
            set
            {
                _isAuthenticated = value;
                RaisePropertyChanged(() => IsAuthenticated);
            }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public IEnumerable<TabNavigationItem> TabItems
        {
            get { return _tabItems; }
            set
            {
                _tabItems = value;
                RaisePropertyChanged(() => TabItems);
            }
        }

        public bool HasLoginFailed
        {
            get { return _hasLoginFailed; }
            set
            {
                _hasLoginFailed = value;
                RaisePropertyChanged(() => HasLoginFailed);
            }
        }

        public RecruiterDto LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                RaisePropertyChanged(() => LoggedInUser);
            }
        }

        public void Load()
        {
            
        }

        public DelegateCommand LoginCommand { get; set; }
        public ICommand LoadDataMiningViewCommand { get; set; }
        public ICommand LoadRecruitersViewCommand { get; set; }
        public ICommand LoadClientsViewCommand { get; set; }
        public ICommand LoadAddDataViewCommand { get; set; }
        public ICommand LoadCandidatesViewCommand { get; set; }
        public ICommand ChangePasswordCommand { get; set; }

        public Dispatcher Dispatcher { get; set; }

        private void LoadRecruitersView()
        {
            var parameters = new Dictionary<string, string> { { "IsAdmin", _securityService.CurrentRecruiter.IsAdmin.ToString() } };
            _navigationService.NavigateTo(MainRegion, RecruitersViewExportName, parameters);
        }

        private void LoadClientsView()
        {
            _navigationService.NavigateTo(MainRegion, ClientViewExportName, new Dictionary<string, string>());
        }

        private void LoadAddDataView()
        {
            var parameters = new Dictionary<string, string> { { "IsAdmin", _securityService.CurrentRecruiter.IsAdmin.ToString() } };
            _navigationService.NavigateTo(MainRegion, DataEntryViewExportName, parameters);
        }

        private void LoadCandidatesView()
        {
            var parameters = new Dictionary<string, string> { { "IsAdmin", _securityService.CurrentRecruiter.IsAdmin.ToString() } };
            _navigationService.NavigateTo(MainRegion, CandidatesViewExportName, parameters);
        }

        private void LoadDataMiningView()
        {
            _navigationService.NavigateTo(MainRegion, AnalyseViewExportName, new Dictionary<string, string>());
        }

        private void UserLogin()
        {
            IsBusy = true;
            _securityService.Authenticate(Login, Password, () =>
                                                               {
                                                                   LoggedInUser = _securityService.CurrentRecruiter;
                                                                   IsAuthenticated = true;
                                                                   IsBusy = false;
                                                                   HasLoginFailed = false;
                                                                   Dispatcher.BeginInvoke(new Action(BuildTabItems));
                                                               }, e =>
                                                                      {
                                                                          HasLoginFailed = true;
                                                                          IsBusy = false;
                                                                      });
        }

        private bool CanLogin()
        {
            return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
        }

        private void BuildTabItems()
        {
            var items = new List<TabNavigationItem>
                            {
                                new TabNavigationItem{Content = "Analyse", Command = LoadDataMiningViewCommand},
                                new TabNavigationItem{Content = "Team", Command = LoadRecruitersViewCommand},
                                new TabNavigationItem{Content = "Clients", Command = LoadClientsViewCommand, IsChecked = true},
                                new TabNavigationItem{Content = "Candidates", Command = LoadCandidatesViewCommand},
                                new TabNavigationItem{Content = "Data", Command = LoadAddDataViewCommand}
                            };
            
            TabItems = items;
            LoadClientsView();
        }
    }
}