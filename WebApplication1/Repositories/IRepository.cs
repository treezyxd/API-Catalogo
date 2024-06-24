using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WebApplication1.Repositories
{
    public interface IRepository<T>
    {
        // cuidado para nao violar o principio ISP
        IEnumerable<T> GetAll();
        T? Get(Expression<Func<T, bool>> predicate); // ex _repo.Get(c => c.CategoriaId == id)

        T Create(T entity);
        T Update(T entity);
        T Delete(T entity);
    }
}