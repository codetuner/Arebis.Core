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
        /// Tests whether proxies are enabled on the context.
        /// </summary>
        public static bool AreProxiesEnabled(this DbContext context)
        {
            var lazyLoadingEnabled = context.ChangeTracker.LazyLoadingEnabled;
            if (lazyLoadingEnabled == false)
                return false;
            else
            {
                var serviceProvider = ((IInfrastructure<IServiceProvider>)context).Instance;
#pragma warning disable EF1001 // Internal EF Core API usage.
                var proxyFactory = serviceProvider.GetService(typeof(Microsoft.EntityFrameworkCore.Proxies.Internal.IProxyFactory));
#pragma warning restore EF1001 // Internal EF Core API usage.
                return proxyFactory != null && lazyLoadingEnabled;
            }
        }

        /// <summary>
        /// Finds an entity by primary key or throws NullReferenceException if not found.
        /// </summary>
        public static TEntity FindOrFail<TEntity>(this DbSet<TEntity> set, params object[] keyValues)
            where TEntity : class
        {
            var entity = set.Find(keyValues);
            if (entity == null)
            {
                throw new NullReferenceException($"Entity of type {typeof(TEntity).Name} with key ({string.Join(", ", keyValues)}) not found.");
            }
            return entity;
        }

        /// <summary>
        /// Finds an entity by primary key or throws NullReferenceException if not found.
        /// </summary>
        public static async Task<TEntity> FindOrFailAsync<TEntity>(this DbSet<TEntity> set, params object[] keyValues)
            where TEntity : class
        {
            var entity = await set.FindAsync(keyValues);
            if (entity == null)
            {
                throw new NullReferenceException($"Entity of type {typeof(TEntity).Name} with key ({string.Join(", ", keyValues)}) not found.");
            }
            return entity;
        }

        /// <summary>
        /// Creates new entity or entity proxy and adds it to the context.
        /// </summary>
        public static TEntity AddNew<TEntity>(this DbSet<TEntity> set, params object[] constructorArgs)
            where TEntity : class
        {
            TEntity? entity;
            var context = set.GetDbContext();
            if (context == null)
            {
                throw new InvalidOperationException("Could not get context from DbSet.");
            }
            else if (context.AreProxiesEnabled())
            {
                entity = set.CreateProxy(constructorArgs);
            }
            else
            {
                object? obj = Activator.CreateInstance(typeof(TEntity), constructorArgs);
                if (obj is not TEntity casted)
                {
                    throw new InvalidOperationException($"Could not create instance of {typeof(TEntity).Name}.");
                }
                entity = casted;
            }
            context.Add(entity);
            return entity;
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
