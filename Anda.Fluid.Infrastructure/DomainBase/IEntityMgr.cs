using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Anda.Fluid.Infrastructure.DomainBase
{
    public interface IEntityManager<TEntity, TKey>
        where TEntity : IEntity<TKey>
    {
        TEntity this[TKey key] { get; set; }
        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void Insert(TEntity entity);
        void InsertRange(int index, IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        void Remove(TKey key);
        TEntity FindBy(TKey key);
        List<TEntity> FindAll();
        int Count { get; }
        void Clear();
    }
}
