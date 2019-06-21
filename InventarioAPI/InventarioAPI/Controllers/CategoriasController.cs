using AutoMapper;
using InventarioAPI.Contexts;
using InventarioAPI.Entities;
using InventarioAPI.Models;
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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> Get()
        {
            var categorias = await contexto.Categorias.ToListAsync();//objetos
            var categoriasDTO = mapper.Map<List<CategoriaDTO>>(categorias);//se convierten en DTO
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
        public async Task<ActionResult> Post([FromBody] CategoriaCreacionDTO categoriaCreacion)
        {
            var categoria = mapper.Map<Categoria>(categoriaCreacion);//mapea el objeto categoria 
            contexto.Add(categoria);//se almacena en la DB
            await contexto.SaveChangesAsync();
            var categoriaDTO = mapper.Map<CategoriaDTO>(categoria);//setea a categoria dto
            return new CreatedAtRouteResult("GetCategoria", new { id = categoria.CodigoCategoria }, categoriaDTO);//devuelve el id que le asigno la DB
        } 
        
    }
}
