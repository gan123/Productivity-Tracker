using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Client
{
    public interface IClientService
    {
        IEnumerable<Domain.Model.Client> GetAll();
        IEnumerable<Domain.Model.Client> Find(string query);
        void Add(string name, string industryId, string address, string website, string contacts, List<string> positionIds);
        void Remove(string id);
        void Update(string id, string name, string industryId, string address, string website, string contacts, List<string> positionIds);
    }
}