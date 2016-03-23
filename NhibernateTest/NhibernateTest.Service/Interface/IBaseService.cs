using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NhibernateTest.Service
{
    public interface IBaseService<TId, TEntity> where TEntity : class
    {
        TEntity Get(TId id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        //IEnumerable<T> Gets(string keyword, int page, int take, string orderBy);

        //IEnumerable<T> GetByUser(string user, string keyword, int page, int take, string orderBy);
    }
}
