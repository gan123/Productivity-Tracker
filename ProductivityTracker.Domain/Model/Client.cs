using System.Collections.Generic;

namespace ProductivityTracker.Domain.Model
{
    public class Client : EntityBase
    {
        public string Name { get; set; }
        public Industry Industry { get; set; }
        public ICollection<Position> Positions { get; set; }
        public string Website { get; set; }
        public string Contacts { get; set; }
        public string Address { get; set; }
    }
}