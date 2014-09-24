using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace NoVacancy.Common
{
    /// <summary>
    /// Class to Translate Lamba Expressions 
    /// </summary>
    public class AutoMapperExtensions
    {
        public static Expression<Func<TEntity, bool>> GetMappedSelector<TDto, TEntity>(Expression<Func<TDto, bool>> selector)
        {
            Expression<Func<TEntity, TDto>> mapper = Mapper.Engine.CreateMapExpression<TEntity, TDto>();
            Expression<Func<TEntity, bool>> mappedSelector = selector.Compose(mapper);
            return mappedSelector;
        }
    }
    public static class FunctionCompositionExtensions
    {
        public static Expression<Func<X, Y>> Compose<X, Y, Z>(this Expression<Func<Z, Y>> outer, Expression<Func<X, Z>> inner)
        {
            return Expression.Lambda<Func<X, Y>>(
                ParameterReplacer.Replace(outer.Body, outer.Parameters[0], inner.Body),
                inner.Parameters[0]);
        }
    }

    class ParameterReplacer : ExpressionVisitor
    {
        private ParameterExpression _parameter;
        private Expression _replacement;

        private ParameterReplacer(ParameterExpression parameter, Expression replacement)
        {
            _parameter = parameter;
            _replacement = replacement;
        }

        public static Expression Replace(Expression expression, ParameterExpression parameter, Expression replacement)
        {
            return new ParameterReplacer(parameter, replacement).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (parameter == _parameter)
            {
                return _replacement;
            }
            return base.VisitParameter(parameter);
        }
    }
}
