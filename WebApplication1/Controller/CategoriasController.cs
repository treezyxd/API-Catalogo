using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.Filters;
using WebApplication1.Model;
using WebApplication1.Repositories;
using WebApplication1.Services;

namespace WebApplication1.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _repository;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ICategoriaRepository repository,
                                    ILogger<CategoriasController> logger)
        {
            _repository       = repository;
            _logger        = logger;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            var produtos = _repository.GetCategoriaProdutos();

            if (produtos is null)
            {
                return NotFound("Produtos nao encontrados");
            }

            return Ok();
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))] // Filtro que executa antes da Action e depois da Action
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categorias = _repository.GetCategorias();

            if (categorias is null)
            {
                return NotFound("Categorias nao encontradas");
            }

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get(int id)
        {
            // throw new Exception("Excecao ao retornar a categoria pelo id");
            var categoria = _repository.GetCategoria(id);

            if (categoria is null)
            {
                return NotFound($"Categoria com id = {id} nao encontrada");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados Invalidos");
            }

            var categoriaCriada = _repository.Create(categoria);
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados Invalidos");
            }

            _repository.Update(categoria);
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<Categoria> Delete(int id)
        {
            var categoria = _repository.GetCategoria(id);

            if (categoria is null)
            {
                return NotFound($"Categoria com id = {id} nao foi encontrada");
            }

            var categoriaExcluida = _repository.Delete(id);
            return Ok(categoriaExcluida);
        }
    }
}