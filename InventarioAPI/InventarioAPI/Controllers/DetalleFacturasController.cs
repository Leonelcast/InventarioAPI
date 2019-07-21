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
    public class DetalleFacturasController: ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public DetalleFacturasController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;


        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleFacturaDTO>>> Get()
        {
            var detalleFacturas = await contexto.DetalleFacturas.ToListAsync();
            var detallesFacturasDTO = mapper.Map<List<DetalleFacturaDTO>>(detalleFacturas);
            return detallesFacturasDTO;
        }
        [HttpGet("{id}", Name ="GetDetalleFactura")]
        public async Task<ActionResult<DetalleFacturaDTO>> Get(int id)
        {
            var detalleFacturas = await contexto.DetalleFacturas.FirstOrDefaultAsync(x => x.CodigoDetalle == id);
            if(detalleFacturas == null)
            {
                return NoContent();
            }
            var detalleFacturaDTO = mapper.Map<DetalleFacturaDTO>(detalleFacturas);
            return detalleFacturaDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DetalleFacturasCreacionDTO detalleFacturasCreacion)
        {
            var detalleFacturas = mapper.Map<DetalleFactura>(detalleFacturasCreacion);
            contexto.Add(detalleFacturas);
            await contexto.SaveChangesAsync();
            var detalleFacturasDTO = mapper.Map<DetalleFacturaDTO>(detalleFacturas);
            return new CreatedAtRouteResult("GetDetalleFactura", new { id = detalleFacturas.CodigoDetalle }, detalleFacturasDTO);
        } 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] DetalleFacturasCreacionDTO detalleFacturaActualizar)
        {
            var detalleFactura = mapper.Map<DetalleFactura>(detalleFacturaActualizar);
            detalleFactura.CodigoDetalle = id;
            contexto.Entry(detalleFactura).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<DetalleFacturaDTO>> Delete(int id)
        {
            var codigoDetalle = await contexto.DetalleFacturas.Select(x => x.CodigoDetalle).FirstOrDefaultAsync(x => x == id);
            if(codigoDetalle == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new DetalleFactura { CodigoDetalle = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }

    }
}
