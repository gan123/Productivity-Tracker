using System;
using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Productivity
{
    public interface IProductivityService
    {
        IEnumerable<Domain.Model.Productivity> Get(string clientId);
        IEnumerable<Domain.Model.Productivity> Search(string clientId, string candidateId, string recruiterId, string statusId, string month, string week);
        void Add(string recruiterId, string recruiterName, DateTime? dateSent, string positionId, string positionName, string clientId, string clientName, string statusId, string statusName, DateTime? expectedFeedbackDate, string comments, string candidateId, string candidateName, string windowsLogin);
        void UpdateStatusAndComments(string id, string statusId, string statusName, string comment, string windowsLogin);
    }
}