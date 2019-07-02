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
    public class ProductosController: ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public ProductosController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductoDTO>>> Get()
        {
            var productos = await contexto.Productos.Include("Categoria").Include("TipoEmpaque").ToListAsync();//objetos
            var productosDTO = mapper.Map<List<ProductoDTO>>(productos);//se convierten en DTO
            return productosDTO;
        }
        //obtiene un elemento 
        [HttpGet("{id}", Name = "GetProducto")]
        public async Task<ActionResult<ProductoDTO>> Get(int id)//solicita un id 
        {
            var producto = await this.contexto.Productos.Include("Categoria").Include("TipoEmpaque").FirstOrDefaultAsync(x => x.CodigoProducto == id);//busca id en la base de datos 
            if (producto == null)
            {
                return NotFound();
            }
            var productoDTO = mapper.Map<ProductoDTO>(producto);
            return productoDTO;

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProductoCreacionDTO productoCreacion)
        {
            var producto = mapper.Map<Producto>(productoCreacion);//mapea el objeto categoria 
            contexto.Add(producto);//se almacena en la DB
            await this.contexto.SaveChangesAsync();
            var productoDTO = mapper.Map<ProductoDTO>(producto);//setea a categoria dto
            return new CreatedAtRouteResult("GetProducto", new { id = producto.CodigoProducto }, productoDTO);//devuelve el id que le asigno la DB
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProductoCreacionDTO productoActualizar)
        {
            var producto = mapper.Map<Producto>(productoActualizar);
            producto.CodigoProducto = id;
            contexto.Entry(producto).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProductoDTO>> Delete(int id)
        {
            var codigoProducto = await contexto.Productos.Select(x => x.CodigoProducto)
                .FirstOrDefaultAsync(x => x == id);
            if (codigoProducto == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Producto { CodigoProducto = id });
            await contexto.SaveChangesAsync();
            return NoContent();

        }

    }
}
