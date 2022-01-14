using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Core.Specifications
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Criteria { get; }

        List<Expression<Func<TEntity, object>>> Includes { get; }
    }
}