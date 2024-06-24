using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Model;

namespace WebApplication1.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
           return _context.Categorias.AsNoTracking().ToList();
        }

        public IEnumerable<Categoria> GetCategoriaProdutos()
        {
            return _context.Categorias.AsNoTracking().Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();            
        }

        public Categoria GetCategoria(int id)
        {
            return _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
        }
        public Categoria Create(Categoria categoria)
        {
            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Add(categoria);
            _context.SaveChanges();

            return categoria;
        }
        public Categoria Update(Categoria categoria)
        {
            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            return categoria;
        }
        public Categoria Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);

            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            
            return categoria;
        }
    }
}