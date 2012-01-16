namespace ProductivityTracker.Client.Models
{
    public class PositionModel : CheckableModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool Equals(PositionModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (PositionModel)) return false;
            return Equals((PositionModel) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}