namespace ProductivityTracker.Domain.Model
{
    public class PositionCovered : EntityBase
    {
        public string Name { get; set; }
        public Client Client { get; set; }
    }
}