namespace ProductivityTracker.Common
{
    public class PositionCoveredDto : EntityDto
    {
        public string Name { get; set; }
        public ClientDto Client { get; set; }
    }
}