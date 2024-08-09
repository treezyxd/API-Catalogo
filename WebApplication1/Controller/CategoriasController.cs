using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Context;
using WebApplication1.DTOs;
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
        private readonly IUnitOfWork _uof;
        private readonly ILogger<CategoriasController> _logger;

        public CategoriasController(ILogger<CategoriasController> logger,
                                     IUnitOfWork uof)
        {
            _logger = logger;
            _uof = uof;
        }

        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))] // Filtro que executa antes da Action e depois da Action
        public ActionResult<IEnumerable<CategoriaDTO>> Get()
        {
            var categorias = _uof.CategoriaRepository.GetAll();

            if (categorias is null)
            {
                return NotFound("Categorias nao encontradas");
            }

            return Ok(categorias);
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<CategoriaDTO>> Get(int id)
        {
            // throw new Exception("Excecao ao retornar a categoria pelo id");
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria com id = {id} nao encontrada");
            }

            return Ok(categoria);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados Invalidos");
            }

            var categoriaCriada = _uof.CategoriaRepository.Create(categoria);
            _uof.Commit();
            return new CreatedAtRouteResult("ObterCategoria", new { id = categoriaCriada.CategoriaId }, categoriaCriada);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoria)
        {
            if (categoria is null)
            {
                return BadRequest("Dados Invalidos");
            }

            _uof.CategoriaRepository.Update(categoria);
            _uof.Commit();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id)
        {
            var categoria = _uof.CategoriaRepository.Get(c => c.CategoriaId == id);

            if (categoria is null)
            {
                return NotFound($"Categoria com id = {id} nao foi encontrada");
            }

            var categoriaExcluida = _uof.CategoriaRepository.Delete(categoria);
            _uof.Commit();
            return Ok(categoriaExcluida);
        }
    }
}