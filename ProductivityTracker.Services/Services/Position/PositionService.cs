using System.Collections.Generic;
using System.Linq;
using Raven.Client.Linq;

namespace ProductivityTracker.Services.Services.Position
{
    public class PositionService : ServiceBase, IPositionService
    {
        public IEnumerable<Domain.Model.Position> GetAll()
        {
            return CurrentSession.Query<Domain.Model.Position>();
        }

        public void Add(string name)
        {
            var position = new Domain.Model.Position {Name = name};

            CurrentSession.Store(position);
        }

        public void Remove(string id)
        {
            var position = CurrentSession.Load<Domain.Model.Position>(id);

            var productivities = CurrentSession.Query<Domain.Model.Productivity>()
                .Where(p => p.Position == position);

            var clients = CurrentSession.Query<Domain.Model.Client>()
                .Where(c => c.Positions.Any(p => p.Id == position.Id));

            foreach(var productivity in productivities)
                CurrentSession.Delete(productivity);

            foreach(var client in clients)
            {
                client.Positions.ToList().Remove(position);
            }

            CurrentSession.Delete(position);
        }
    }
}