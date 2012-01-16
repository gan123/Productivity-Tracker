using System;
using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Position
{
    public interface IPositionService
    {
        IEnumerable<Domain.Model.Position> GetAll();
        void Add(string name);
        void Remove(string id);
    }
}