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
    public class DetalleComprasController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public DetalleComprasController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCompraDTO>>> Get()
        {
            var detalleCompras = await contexto.DetalleCompras.ToListAsync();
            var detalleComprasDTO = mapper.Map<List<DetalleCompraDTO>>(detalleCompras);
            return detalleComprasDTO;

        }
        [HttpGet("{id}", Name ="GetDetalleCompra")]
        public async Task<ActionResult<DetalleCompraDTO>> Get(int id)
        {
            var detalleCompra = await contexto.DetalleCompras.FirstOrDefaultAsync(x => x.IdDetalle == id);
            if (detalleCompra == null)
            {
                return NotFound();
            }
            var detalleCompraDTO = mapper.Map<DetalleCompraDTO>(detalleCompra);
            return detalleCompraDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleComprasCreacionDTO detalleComprasCreacion)
        {
            var detalleCompras = mapper.Map<DetalleCompra>(detalleComprasCreacion);
            contexto.Add(detalleCompras);
            await contexto.SaveChangesAsync();
            var detalleComprasDTO = mapper.Map<DetalleCompraDTO>(detalleCompras);
            return new CreatedAtRouteResult("GetDetalleCompra", new { id = detalleCompras.IdDetalle }, detalleComprasDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleComprasCreacionDTO detalleCompraActualizar)
        {
            var detalleCompras = mapper.Map<DetalleCompra>(detalleCompraActualizar);
            detalleCompras.IdDetalle = id;
            contexto.Entry(detalleCompras).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleCompraDTO>> Delete(int id)
        {
            var idDetalle = await contexto.DetalleCompras.Select(x => x.IdDetalle).FirstOrDefaultAsync(x => x == id);
            if(idDetalle == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new DetalleCompra { IdDetalle = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
