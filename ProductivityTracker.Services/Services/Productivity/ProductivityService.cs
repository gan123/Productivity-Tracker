using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Linq;

namespace ProductivityTracker.Services.Services.Productivity
{
    public class ProductivityService : ServiceBase, IProductivityService
    {
        public IEnumerable<Domain.Model.Productivity> Get(string clientId)
        {
            List<Domain.Model.FlatProductivity> flatProductivity;

            if (!string.IsNullOrEmpty(clientId))
                flatProductivity = CurrentSession.Query<Domain.Model.FlatProductivity>()
                    .Include(p => p.Position.Id)
                    .Include(p => p.Recruiter.Id)
                    .Include(p => p.Client.Id)
                    .Include(p => p.Candidate.Id)
                    .Include(p => p.Status.Id)
                    .Where(p => p.Client.Id == clientId).ToList();
            else flatProductivity = CurrentSession.Query<Domain.Model.FlatProductivity>()
                .Include(p => p.Position.Id)
                .Include(p => p.Recruiter.Id)
                .Include(p => p.Client.Id)
                .Include(p => p.Candidate.Id)
                .Include(p => p.Status.Id).ToList();

            return flatProductivity.Select(f => new Domain.Model.Productivity
                                                    {
                                                        Id = f.Id,
                                                        Candidate = CurrentSession.Load<Domain.Model.Candidate>(f.Candidate.Id),
                                                        Client = CurrentSession.Load<Domain.Model.Client>(f.Client.Id),
                                                        Comments = f.Comments,
                                                        DateSent = f.DateSent,
                                                        ExpectedFeedbackDate = f.ExpectedFeedbackDate,
                                                        Position = CurrentSession.Load<Domain.Model.Position>(f.Position.Id),
                                                        Recruiter = CurrentSession.Load<Domain.Model.Recruiter>(f.Recruiter.Id),
                                                        Status = CurrentSession.Load<Domain.Model.Status>(f.Status.Id)
                                                    }).OrderBy(p => p.Client.Name);
        }

        public void Add(string recruiterId, string recruiterName, DateTime? dateSent, string positionId, string positionName, string clientId, string clientName, string statusId, string statusName, DateTime? expectedFeedbackDate, string comments, string candidateId, string candidateName, string windowsLogin)
        {
            var productivity = new Domain.Model.FlatProductivity
                                   {
                                       ExpectedFeedbackDate = expectedFeedbackDate,
                                       DateSent = dateSent,
                                       Comments = new List<Domain.Model.Comment> { new Domain.Model.Comment { Description = comments, EnteredBy = windowsLogin } },
                                       Candidate = new Domain.Model.DenormalizedReference { Id = candidateId, Name = candidateName },
                                       Client = new Domain.Model.DenormalizedReference { Id = clientId, Name = clientName },
                                       Recruiter = new Domain.Model.DenormalizedReference { Id = recruiterId, Name = recruiterName },
                                       Position = new Domain.Model.DenormalizedReference { Id = positionId, Name = positionName },
                                       Status = new Domain.Model.DenormalizedReference { Id = statusId, Name = statusName }
                                   };

            CurrentSession.Store(productivity);
        }

        public void UpdateStatusAndComments(string id, string statusId, string statusName, string comment, string windowsLogin)
        {
            var productivity = CurrentSession.Load<Domain.Model.FlatProductivity>(id);
            if (!string.IsNullOrEmpty(statusId))
                productivity.Status = new Domain.Model.DenormalizedReference {Id = statusId, Name = statusName};
            if (!string.IsNullOrEmpty(comment))
                productivity.Comments.Add(new Domain.Model.Comment { Description = comment, EnteredBy = windowsLogin });
        }
    }
}