using System.Collections.Generic;

namespace ProductivityTracker.Common
{
    public class ClientDto: EntityDto
    {
        public string Name { get; set; }
        public string IndustryName { get; set; }
        public string Website { get; set; }
        public string Contacts { get; set; }
        public string Address { get; set; }
        public IEnumerable<PositionDto> Positions { get; set; }
    }
}