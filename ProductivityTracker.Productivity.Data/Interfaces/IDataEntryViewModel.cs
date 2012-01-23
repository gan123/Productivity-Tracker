using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Controls.Search;
using ProductivityTracker.Productivity.Data.Models;

namespace ProductivityTracker.Productivity.Data.Interfaces
{
    public interface IDataEntryViewModel : INavigationAware, IDataErrorInfo
    {
        IEnumerable<StatusModel> Statuses { get; set; }
        StatusModel StatusModel { get; set; }
        IEnumerable<RecruiterModel> Recruiters { get; set; }
        RecruiterModel Recruiter { get; set; }
        ClientSearch Client { get; set; }
        DateTime? DateSent { get; set; }
        DateTime? ExpectedFeedbackDate { get; set; }
        ClientPosition Position { get; set; }
        CandidateSearch Candidate { get; set; }
        string Comments { get; set; }
        bool IsPositionEnabled { get; set; }

        DelegateCommand SaveCommand { get; set; }
    }
}