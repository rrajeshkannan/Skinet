using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    {
        public BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<TEntity, bool>> Criteria { get; }

        public List<Expression<Func<TEntity, object>>> Includes { get; } = new ();

        protected void AddInclude(Expression<Func<TEntity, object>> include) => Includes.Add(include);
    }
}