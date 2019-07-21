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
    public class InventariosController: ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public InventariosController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventarioDTO>>> Get()
        {
            var inventarios = await contexto.Inventarios.ToListAsync();
            var inventariosDTO = mapper.Map<List<InventarioDTO>>(inventarios);
            return inventariosDTO;

        }
        [HttpGet("{id}", Name = "GetInventario")]
        public async Task<ActionResult<InventarioDTO>> Get(int id)
        {
            var inventario = await contexto.Inventarios.FirstOrDefaultAsync(x => x.CodigoInventario == id);
            if(inventario == null)
            {
                return NotFound();
            }
            var inventarioDTO = mapper.Map<InventarioDTO>(inventario);
            return inventarioDTO;

        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] InventariosCreacionDTO inventariosCreacion)
        {
            var inventarios = mapper.Map<Inventario>(inventariosCreacion);
            contexto.Add(inventarios);
            await contexto.SaveChangesAsync();
            var inventariosDTO = mapper.Map<InventarioDTO>(inventarios);
            return new CreatedAtRouteResult("GetInventario", new { id = inventarios.CodigoInventario }, inventariosDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id,[FromBody] InventariosCreacionDTO inventarioActualizar)
        {
            var inventarios = mapper.Map<Inventario>(inventarioActualizar);
            inventarios.CodigoInventario = id;
            contexto.Entry(inventarios).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<InventarioDTO>> Delete(int id)
        {
            var codigoInventario = await contexto.Inventarios.Select(x => x.CodigoInventario).FirstOrDefaultAsync(x => x == id);
            if(codigoInventario == default(int))
            {
                return NoContent();
            }
            contexto.Remove(new Inventario { CodigoInventario = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
