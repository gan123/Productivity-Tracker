using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace ProductivityTracker.Services.Indexes
{
    public class ProductivityIndex : AbstractIndexCreationTask<Domain.Model.FlatProductivity>
    {
        public ProductivityIndex()
        {
            Map = productivities => from p in productivities
                                    select new
                                               {
                                                   PositionId = p.Position.Id,
                                                   CandidateId = p.Candidate.Id,
                                                   RecruiterId = p.Recruiter.Id,
                                                   p.DateSent,
                                                   ClientId = p.Client.Id,
                                                   StatusId = p.Status.Id,
                                                   p.Month,
                                                   p.Week
                                               };
            Index(r => r.Recruiter.Name, FieldIndexing.Analyzed);
            Index(r => r.Position.Name, FieldIndexing.Analyzed);
            Index(r => r.Client.Name, FieldIndexing.Analyzed);
            Index(r => r.Status.Name, FieldIndexing.Analyzed);
        }
    }
}