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
                                                   PositionName = p.Position.Name,
                                                   PositionId = p.Position.Id,
                                                   RecruiterName = p.Recruiter.Name,
                                                   RecruiterId = p.Recruiter.Id,
                                                   p.DateSent,
                                                   ClientName = p.Client.Name,
                                                   ClientId = p.Client.Id,
                                                   StatusName = p.Status.Name,
                                                   StatusId = p.Status.Id
                                               };
            Index(r => r.Recruiter.Name, FieldIndexing.Analyzed);
            Index(r => r.Position.Name, FieldIndexing.Analyzed);
            Index(r => r.Client.Name, FieldIndexing.Analyzed);
            Index(r => r.Status.Name, FieldIndexing.Analyzed);
        }
    }
}