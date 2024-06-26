using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Model;
using WebApplication1.Repositories;

namespace WebApplication1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IProdutoRepository _produtoRepository;
        // private readonly IRepository<Produto> _repository;
        public ProdutosController(IProdutoRepository produtoRepository)
        {
            // _repository = repository;
            _produtoRepository = produtoRepository;
        }

        [HttpGet("produtos/{id}")]
        public ActionResult<IEnumerable<Produto>> GetProdutosPorCategoria(int id)
        {
            var produtos = _produtoRepository.GetProdutosPorCategoria(id);

            if (produtos == null)
                return NotFound();
            
            return Ok(produtos);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _produtoRepository.GetAll();
            if(produtos is null)
            {
                return NotFound("Products not found");
            }
            
            return Ok(produtos);

        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public ActionResult<Produto> Get(int id)
        {
            var produto = _produtoRepository.Get(c => c.CategoriaId == id);

            if(produto == null)
            {
                return NotFound("Produto nao encontrado");
            }
            return Ok(produto);
        }

        [HttpPost]
        public ActionResult Post([FromBody]Produto produto)
        {
            if (produto is null)
                return BadRequest();

            var novoProduto = _produtoRepository.Create(produto);

            return new CreatedAtRouteResult("ObterProduto", 
                new { id = novoProduto.ProdutoId }, novoProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
            {
                return BadRequest();
            }

            var produtoAtualizado = _produtoRepository.Update(produto);

            return Ok(produtoAtualizado);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _produtoRepository.Get(c => c.CategoriaId == id);

            if (produto is null)
                return NotFound("Produto nao encontrado");

            var produtoDeletado = _produtoRepository.Delete(produto);

            return Ok(produtoDeletado);
        }
    }
}