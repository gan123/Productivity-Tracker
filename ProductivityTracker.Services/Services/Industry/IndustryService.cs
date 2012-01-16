using System;
using System.Collections.Generic;
using System.Linq;
using Raven.Client.Linq;

namespace ProductivityTracker.Services.Services.Industry
{
    public class IndustryService : ServiceBase, IIndustryService
    {
        public IEnumerable<Domain.Model.Industry> GetAll()
        {
            return CurrentSession.Query<Domain.Model.Industry>();
        }

        public void Add(string name)
        {
            var industy = new Domain.Model.Industry {Name = name};
            CurrentSession.Store(industy);
        }

        public void Remove(string id)
        {
            var industry = CurrentSession.Load<Domain.Model.Industry>(id);
            var clients = CurrentSession.Query<Domain.Model.Client>()
                .Where(c => c.Industry.Id == industry.Id);

            if (clients.Any())
                throw new InvalidOperationException("There clients mapped to this industry.");

            CurrentSession.Delete(industry);
        }
    }
}