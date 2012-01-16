using System.Collections.Generic;
using System.Linq;
using ProductivityTracker.Services.Services.Helpers;
using Raven.Client.Linq;

namespace ProductivityTracker.Services.Services.Client
{
    public class ClientService : ServiceBase, IClientService
    {
        public IEnumerable<Domain.Model.Client> GetAll()
        {
            return CurrentSession.Query<Domain.Model.Client>()
                .Include(c => c.Industry)
                .Include(c => c.Positions).ToList();
        }

        public IEnumerable<Domain.Model.Client> Find(string query)
        {
            return CurrentSession.Advanced.LuceneQuery<Domain.Model.Client>("ClientIndex")
                .Include(c => c.Industry)
                .Include(c => c.Positions)
                .WhereContains("Name", QueryExtensions.CleanseAndSplit(query)).ToList();
        }

        public void Add(string name, string industryId, string address, string website, string contacts, List<string> positionIds)
        {
            var client = new Domain.Model.Client();
            client.Name = name;
            client.Positions = new List<Domain.Model.Position>();
            foreach (var positionId in positionIds)
                client.Positions.Add(CurrentSession.Load<Domain.Model.Position>(positionId));
            client.Website = website;
            client.Industry = CurrentSession.Load<Domain.Model.Industry>(industryId);
            client.Address = address;
            client.Contacts = contacts;

            CurrentSession.Store(client);
        }

        public void Remove(string id)
        {
            var client = CurrentSession.Load<Domain.Model.Client>(id);
            CurrentSession.Delete(client);
        }

        public void Update(string id, string name, string industryId, string address, string website, string contacts, List<string> positionIds)
        {
            var client = CurrentSession.Load<Domain.Model.Client>(id);
            client.Name = name;
            client.Positions = new List<Domain.Model.Position>();
            foreach (var positionId in positionIds)
                client.Positions.Add(CurrentSession.Load<Domain.Model.Position>(positionId));
            client.Website = website;
            client.Industry = CurrentSession.Load<Domain.Model.Industry>(industryId);
            client.Address = address;
            client.Contacts = contacts;
        }
    }
}