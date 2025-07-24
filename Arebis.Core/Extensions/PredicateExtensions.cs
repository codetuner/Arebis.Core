using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arebis.Core.Extensions
{
    /// <summary>
    /// Extensions for combining predicates in LINQ expressions.
    /// </summary>
    public static class PredicateExtensions
    {
        /// <summary>
        /// Ands two predicates together using a logical AND operation.
        /// </summary>
        public static Expression<Func<T, U>> AndAlso<T, U>(this Expression<Func<T, U>> first, Expression<Func<T, U>> second)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var left = ReplaceParameter(first.Body, first.Parameters[0], parameter);
            var right = ReplaceParameter(second.Body, second.Parameters[0], parameter);

            return Expression.Lambda<Func<T, U>>(
                Expression.AndAlso(left, right), parameter);
        }

        /// <summary>
        /// Ors two predicates together using a logical OR operation.
        /// </summary>
        public static Expression<Func<T, U>> OrElse<T, U>(this Expression<Func<T, U>> first, Expression<Func<T, U>> second)
        {
            var parameter = Expression.Parameter(typeof(T), "x");

            var left = ReplaceParameter(first.Body, first.Parameters[0], parameter);
            var right = ReplaceParameter(second.Body, second.Parameters[0], parameter);

            return Expression.Lambda<Func<T, U>>(
                Expression.OrElse(left, right), parameter);
        }

        private static Expression ReplaceParameter(Expression expr, ParameterExpression toReplace, ParameterExpression replaceWith)
        {
            return new ReplaceParameterVisitor(toReplace, replaceWith).Visit(expr);
        }

        private class ReplaceParameterVisitor : ExpressionVisitor
        {
            private readonly ParameterExpression _from;
            private readonly ParameterExpression _to;

            public ReplaceParameterVisitor(ParameterExpression from, ParameterExpression to)
            {
                _from = from;
                _to = to;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == _from ? _to : base.VisitParameter(node);
            }
        }
    }
}
