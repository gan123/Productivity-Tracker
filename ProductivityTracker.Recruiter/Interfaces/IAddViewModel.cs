using System;
using Microsoft.Practices.Prism.Commands;

namespace ProductivityTracker.Recruiter.Interfaces
{
    public interface IAddViewModel : System.ComponentModel.IDataErrorInfo
    {
        string Name { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string Designation { get; set; }
        bool IsAdmin { get; set; }
        bool ShowAdminOption { get; }
        DateTime? DateOfBirth { get; set; }
        DateTime? DateOfJoining { get; set; }
        DelegateCommand SaveCommand { get; set; }
        void Reset();
    }
}