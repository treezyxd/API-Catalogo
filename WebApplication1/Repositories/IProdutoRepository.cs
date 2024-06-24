using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Repositories
{
    public interface IProdutoRepository
    {
        IQueryable<Produto> GetProdutos();
        Produto GetProduto(int id);
        Produto Create(Produto produto);
        bool Update(Produto produto);
        bool Delete(int id);
    }
}