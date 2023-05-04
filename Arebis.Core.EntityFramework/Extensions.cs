using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Arebis.Core.Source;

namespace Arebis.Core.EntityFramework
{
    /// <summary>
    /// Entity Framework extension methods.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Get the DbContext of a DbSet.
        /// </summary>
        [CodeSource("https://dev.to/j_sakamoto/how-to-get-a-dbcontext-from-a-dbset-in-entityframework-core-c6m", "jsakamoto")]
        public static DbContext? GetDbContext<T>(this DbSet<T> dbSet) where T : class
        {
            var infrastructure = dbSet as IInfrastructure<IServiceProvider>;
            var serviceProvider = infrastructure.Instance;
            var currentDbContext = serviceProvider.GetService(typeof(ICurrentDbContext)) as ICurrentDbContext;
            return currentDbContext?.Context;
        }

        /// <summary>
        /// Marks a contextual entity as modified.
        /// </summary>
        public static void MarkModified<TDbContext>(this IContextualEntity<TDbContext> entity)
            where TDbContext : DbContext
        {
            var context = entity.Context;
            if (context != null)
            {
                var entry = context.Entry(entity);
                switch (entry.State)
                {
                    case EntityState.Unchanged:
                        entry.State = EntityState.Modified;
                        break;
                    case EntityState.Detached:
                        entry.State = EntityState.Modified;
                        break;
                }
            }
        }

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
