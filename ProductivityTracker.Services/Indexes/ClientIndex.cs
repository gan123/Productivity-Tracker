using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace ProductivityTracker.Services.Indexes
{
    public class ClientIndex : AbstractIndexCreationTask<Domain.Model.Client>
    {
        public ClientIndex()
        {
            Map = clients => from r in clients
                             select new { r.Name };

            Index(r => r.Name, FieldIndexing.Analyzed);
        }
    }
}