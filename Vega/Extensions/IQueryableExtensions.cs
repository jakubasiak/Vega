using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vega.Core.Models;

namespace Vega.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, IQueryObject queryObject, Dictionary<string, Expression<Func<T, object>>> columnMap)
        {
            if (String.IsNullOrWhiteSpace(queryObject.SortBy) || !columnMap.ContainsKey(queryObject.SortBy))
                return query;

            if (queryObject.IsSortAscending)
                return query = query.OrderBy(columnMap[queryObject.SortBy]);
            else
                return query = query.OrderByDescending(columnMap[queryObject.SortBy]);
        }
        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, IQueryObject queryObject)
        {
            if (queryObject.Page <= 0)
                queryObject.Page = 1;
            if (queryObject.PageSize <= 0)
                queryObject.PageSize = 10;
            return query.Skip((queryObject.Page - 1) * queryObject.PageSize).Take(queryObject.PageSize);
        }
    }
}
