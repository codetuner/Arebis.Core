using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.EntityFramework
{
    public static class Extensions
    {
        /// <summary>
        /// Performs ordering given a string expression.
        /// </summary>
        /// <param name="query">The query to order.</param>
        /// <param name="orderByExpression">The property (path) to order by. Append " ASC" or " DESC" to be explicit about the ordering direction.
        /// Can contain multiple terms separated by comma, i.e. "Name, Town ASC, Country DESC".
        /// </param>
        /// <see href="https://stackoverflow.com/a/64085775"/>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderByExpression)
        {
            string propertyName, orderByMethod;
            string[] terms = orderByExpression.Split(',', 2);
            string[] strs = terms[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            propertyName = strs[0];

            if (strs.Length == 1)
                orderByMethod = "OrderBy";
            else
                orderByMethod = strs[1].Equals("DESC", StringComparison.OrdinalIgnoreCase) ? "OrderByDescending" : "OrderBy";

            ParameterExpression pe = Expression.Parameter(query.ElementType);
            MemberExpression me = Expression.Property(pe, propertyName);

            MethodCallExpression orderByCall = Expression.Call(typeof(Queryable), orderByMethod, new Type[] { query.ElementType, me.Type }, query.Expression
                , Expression.Quote(Expression.Lambda(me, pe)));

            if (terms.Length == 1)
            {
                return (IOrderedQueryable<T>)query.Provider.CreateQuery(orderByCall);
            }
            else
            {
                return ((IOrderedQueryable<T>)query.Provider.CreateQuery(orderByCall)).ThenBy(terms[1]);
            }
        }

        /// <summary>
        /// Performs further ordering given a string expression.
        /// </summary>
        /// <param name="query">The query to order.</param>
        /// <param name="orderByExpression">The property (path) to order by. Append " ASC" or " DESC" to be explicit about the ordering direction.
        /// Can contain multiple terms separated by comma, i.e. "Name, Town ASC, Country DESC".
        /// </param>
        /// <see href="https://stackoverflow.com/a/64085775"/>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> query, string orderByExpression)
        {
            string propertyName, orderByMethod;
            string[] terms = orderByExpression.Split(',', 2);
            string[] strs = terms[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            propertyName = strs[0];

            if (strs.Length == 1)
                orderByMethod = "ThenBy";
            else
                orderByMethod = strs[1].Equals("DESC", StringComparison.OrdinalIgnoreCase) ? "ThenByDescending" : "ThenBy";

            ParameterExpression pe = Expression.Parameter(query.ElementType);
            MemberExpression me = Expression.Property(pe, propertyName);

            MethodCallExpression orderByCall = Expression.Call(typeof(Queryable), orderByMethod, new Type[] { query.ElementType, me.Type }, query.Expression
                , Expression.Quote(Expression.Lambda(me, pe)));


            if (terms.Length == 1)
            {
                return (IOrderedQueryable<T>)query.Provider.CreateQuery(orderByCall);
            }
            else
            {
                return ((IOrderedQueryable<T>)query.Provider.CreateQuery(orderByCall)).ThenBy(terms[1]);
            }
        }
    }
}
