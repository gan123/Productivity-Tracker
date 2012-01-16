using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Industry
{
    public interface IIndustryService
    {
        IEnumerable<Domain.Model.Industry> GetAll();
        void Add(string name);
        void Remove(string id);
    }
}