using System.Collections.Generic;
using System.Linq;
using ProductivityTracker.Client.Models;

namespace ProductivityTracker.Client.Extensions
{
    public static class Extensions
    {
        public static IEnumerable<PositionModel> Clone(this IEnumerable<PositionModel> source)
        {
            return source.Select(s => new PositionModel { Id = s.Id, Name = s.Name}).ToList();
        }
    }
}