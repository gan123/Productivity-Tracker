using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProductivityTracker.Services.Services.Helpers
{
    public static class QueryExtensions
    {
        public static string CleanseAndSplit(string query)
        {
            var sb = new StringBuilder();
            var splits = query.Trim().Split(' ');
            sb.Append("*");
            foreach(var q in splits)
            {
                sb.Append(q);
                sb.Append("*");
            }
            return sb.ToString();
        }

        public static string CreateTokenizingFilter(string field, IEnumerable<string> splitQuery)
        {
            if (splitQuery == null || !splitQuery.Any() || string.IsNullOrEmpty(field)) return string.Empty;
            return "(" + string.Join(" AND ", splitQuery.Select(f => string.Format("{0}: {1}", field, f)).ToArray()) +
                   ")";

        }

        public static string ConcatQueriesAsAnd(IEnumerable<string> queries)
        {
            if (queries == null || !queries.Any()) return string.Empty;
            return "(" + string.Join(" AND ", queries) + ")";
        }

        public static string ConcatQueriesAsOr(IEnumerable<string> queries)
        {
            if (queries == null || !queries.Any()) return string.Empty;
            return "(" + string.Join(" OR ", queries) + ")";
        }

        public static IEnumerable<string> CleanseAndSplitQuery(string query)
        {
            var trimmedQuery = query.Trim().ToLowerInvariant();
            return trimmedQuery.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
        }
    }
}