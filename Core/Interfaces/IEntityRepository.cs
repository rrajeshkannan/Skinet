using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(int id);
        
        Task<IReadOnlyList<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(ISpecification<TEntity> specification);
        
        Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity> specification);
    }
}