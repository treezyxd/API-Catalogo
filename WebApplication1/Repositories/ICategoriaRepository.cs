using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Model;

namespace WebApplication1.Repositories
{
    public interface ICategoriaRepository
    {
        IEnumerable<Categoria> GetCategorias();
        IEnumerable<Categoria> GetCategoriaProdutos();
        Categoria GetCategoria(int id);
        Categoria Create(Categoria categoria);
        Categoria Update(Categoria categoria);
        Categoria Delete(int id);
    }
}