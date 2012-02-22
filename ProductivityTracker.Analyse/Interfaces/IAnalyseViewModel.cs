using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Practices.Prism.Regions;
using ProductivityTracker.Analyse.Models;
using ProductivityTracker.Controls.Search;
using Visifire.Charts;

namespace ProductivityTracker.Analyse.Interfaces
{
    public interface IAnalyseViewModel : INavigationAware
    {
        CollectionViewSource Productivities { get; set; }
        ClientSearch Client { get; set; }
        CandidateSearch Candidate { get; set; }
        IEnumerable<StatusModel> Statuses { get; set; }
        StatusModel Status { get; set; }
        IEnumerable<RecruiterModel> Recruiters { get; set; }
        RecruiterModel Recruiter { get; set; }
        IEnumerable<string> Months { get; set; }
        string Month { get; set; }
        IEnumerable<string> Weeks { get; }
        DataSeriesCollection ChartSource { get; set; }
        string Week { get; set; }
        bool IsWeekEnabled { get; }
        ICommand UpdateStatusCommand { get; set; }
        ICommand SearchCommand { get; set; }
        ICommand ClearCommand { get; set; }
        event EventHandler ChartUpdated;
    }
}