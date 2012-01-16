using System;

namespace ProductivityTracker.Common
{
    public class ClientProductivitySummaryDto : EntityDto
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string PositionName { get; set; }
        public int? NoOfCvs { get; set; }
        public DateTime? DateSent { get; set; }
    }
}