using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
   
    public class CategoriasController: ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public CategoriasController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet("{numeroDePagina}",Name ="GetCategoriaPage")]
        [Route("page/{numeroDePagina}")]
        public async Task<ActionResult<CategoriaPaginacionDTO>> GetCategoriasPage(int numeroDePagina = 0)
        {
            int cantidadDeRegistros = 5;
            var categoriaPaginacionDTO = new CategoriaPaginacionDTO();
            var query = contexto.Categorias.AsQueryable();
            int totalDeRegitros = query.Count();
            int totalPaginas = (int)Math.Ceiling((Double)totalDeRegitros / cantidadDeRegistros);
            categoriaPaginacionDTO.Number = numeroDePagina;
            var categorias = await query.Skip(cantidadDeRegistros * (categoriaPaginacionDTO.Number)).Take(cantidadDeRegistros).ToListAsync();//objetos
            categoriaPaginacionDTO.content = mapper.Map<List<CategoriaDTO>>(categorias);
            var categoriasDTO = mapper.Map<List<CategoriaDTO>>(categorias);//se convierten en DTO
            categoriaPaginacionDTO.TotalPages = totalPaginas;
          
            
            if(numeroDePagina ==0)
            {
                categoriaPaginacionDTO.First = true;
            }
            else if(numeroDePagina == totalPaginas )
            {
                categoriaPaginacionDTO.Last = true;
            }
            
            return categoriaPaginacionDTO;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await this.contexto.Categorias.ToListAsync();
            var categoriasDTO = this.mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }
        //obtiene un elemento 
        [HttpGet("{id}", Name ="GetCategoria")]
        public async Task<ActionResult<CategoriaDTO>> Get(int id)//solicita un id 
        {
            var categoria = await contexto.Categorias.FirstOrDefaultAsync(x => x.CodigoCategoria == id );//busca id en la base de datos 
                if (categoria == null)
                {
                    return NotFound();
                }
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);
            return categoriaDTO;

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CategoriasCreacionDTO categoriaCreacion)
        {
            var categoria = mapper.Map<Categoria>(categoriaCreacion);//mapea el objeto categoria 
            contexto.Add(categoria);//se almacena en la DB
            await contexto.SaveChangesAsync();
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);//setea a categoria dto
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.CodigoCategoria }, categoriaDTO);//devuelve el id que le asigno la DB
        } 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoriasCreacionDTO categoriaActualizar)
        {
            var categoria = mapper.Map<Categoria>(categoriaActualizar);
            categoria.CodigoCategoria = id;
            contexto.Entry(categoria).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CategoriaDTO>> Delete(int id)
        {
            var codigoCategoria = await contexto.Categorias.Select(x => x.CodigoCategoria)
                .FirstOrDefaultAsync(x => x == id);
            if(codigoCategoria == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Categoria { CodigoCategoria = id });
            await contexto.SaveChangesAsync();
            return NoContent();

        }
        
    }
}
