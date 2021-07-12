//Sai: 07/08/2021: Created the utilities class for calling the common functions.
//Its a static class / method. so we can directly access it.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CodingChallenge.Utilities
{
    public static class LinqUtilities 
    {
        public const string ORDER_BY = "OrderBy";

        public const string ORDER_BY_DESCENDING = "OrderByDescending";

        public const string PARAMETER_TYPE_STRING = "p";

        //It takes IQueryable as Input and Output
        //The OrderBy can be used for sorting the respective list column in Descending or Ascending Order. 
        //It sorts with case sensitive so if we want to sort without case sensitive then please use linq query for the respective column.
        public static IQueryable<T> OrderByField<T>(this IQueryable<T> q, string SortField, bool Ascending = true)

        {
            ConstantExpression comparisonType = Expression.Constant(StringComparison.OrdinalIgnoreCase);

            var param = Expression.Parameter(typeof(T), PARAMETER_TYPE_STRING);

            var prop = Expression.Property(param, SortField);

            var exp = Expression.Lambda(prop, param);

            string method = Ascending ? ORDER_BY : ORDER_BY_DESCENDING;

            Type[] types = new Type[] { q.ElementType, exp.Body.Type };

            var mce = Expression.Call(typeof(Queryable), method, types, q.Expression, exp, comparisonType);

            return q.Provider.CreateQuery<T>(mce);

        }
        //It takes IEnumerable as Input and Output
        //The OrderBy can be used for sorting the respective list column in Descending or Ascending Order. 
        //It sorts with case sensitive so if we want to sort without case sensitive then please use linq query for the respective column.
        public static IEnumerable<TEntity> OrderByField<TEntity>(this IEnumerable<TEntity> source,
                                                          string columnID, bool Ascending = true)
        {
            
            string command = (Ascending ? ORDER_BY : ORDER_BY_DESCENDING);            

            var type = typeof(TEntity);
            var property = type.GetProperty(columnID);
            var parameter = Expression.Parameter(type, PARAMETER_TYPE_STRING);
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);           

            var resultExpression = Expression.Call(typeof(Queryable), command,
                                                   new[] { type, property.PropertyType },
                                                   source.AsQueryable().Expression,
                                                   Expression.Quote(orderByExpression)
                                                   );
            return source.AsQueryable().Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}

