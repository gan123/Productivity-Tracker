using System.Collections.Generic;

namespace ProductivityTracker.Analyse.Models
{
    public class StatusModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MonthChartModel
    {
        public string Recruiter { get; set; }
        public IEnumerable<Data> Data { get; set; }
    }

    public class Data
    {
        public string Month { get; set; }
        public int Value { get; set; }
    }
}