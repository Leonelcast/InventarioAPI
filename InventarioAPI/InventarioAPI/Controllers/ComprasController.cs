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
    public class ComprasController:ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public ComprasController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraDTO>>> Get()
        {
            var compras = await contexto.Compras.ToListAsync();
            var comprasDTo = mapper.Map<List<CompraDTO>>(compras);
            return comprasDTo;
        }
        [HttpGet("{id}", Name = "GetCompra")]
        public async Task<ActionResult<CompraDTO>> Get(int id)
        {
            var compra = await contexto.Compras.FirstOrDefaultAsync(z => z.IdCompra == id);
            if (compra == null)
            {
                return NotFound();
            }
            var compraDTO = mapper.Map<CompraDTO>(compra);
            return compraDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ComprasCreacionDTO comprasCreacion)
        {
            var compra = mapper.Map<Compra>(comprasCreacion);
            contexto.Add(compra);
            await contexto.SaveChangesAsync();
            var compraDTO = mapper.Map<CompraDTO>(compra);
            return new CreatedAtRouteResult("GetCompra", new { id = compra.IdCompra }, compraDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ComprasCreacionDTO compraActualizar)
        {
            var compra = mapper.Map<Compra>(compraActualizar);
            compra.IdCompra = id;
            contexto.Entry(compra).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompraDTO>> Delete(int id)
        {
            var idCompra = await contexto.Compras.Select(z => z.IdCompra)
                .FirstOrDefaultAsync(z => z == id);
            if (idCompra == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Compra { IdCompra = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
