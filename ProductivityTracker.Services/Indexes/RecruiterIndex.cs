using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace ProductivityTracker.Services.Indexes
{
    public class RecruiterIndex : AbstractIndexCreationTask<Domain.Model.Recruiter>
    {
        public RecruiterIndex()
        {
            Map = recruiters => from r in recruiters
                                select new {r.FullName};
            
            Index(r => r.FullName, FieldIndexing.Analyzed);
        }
    }
}