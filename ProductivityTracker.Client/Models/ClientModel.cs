using System.Collections.Generic;

namespace ProductivityTracker.Client.Models
{
    public class ClientModel : CheckableModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string IndustryName { get; set; }
        public IEnumerable<PositionModel> Positions { get; set; }
        public string Website { get; set; }
        public string Contacts { get; set; }
        public string Address { get; set; }

        public bool Equals(ClientModel other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Id, Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (ClientModel)) return false;
            return Equals((ClientModel) obj);
        }

        public override int GetHashCode()
        {
            return (Id != null ? Id.GetHashCode() : 0);
        }
    }
}