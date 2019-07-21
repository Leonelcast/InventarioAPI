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
    public class FacturasController:ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public FacturasController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FacturaDTO>>> Get()
        {
            var facturas = await contexto.Facturas.ToListAsync();
            var facturasDTO = mapper.Map<List<FacturaDTO>>(facturas);
            return facturasDTO;
        }
        [HttpGet("{id}", Name ="GetFactura")]
        public async Task<ActionResult<FacturaDTO>> Get(int id)
        {
            var faturas = await contexto.Facturas.FirstOrDefaultAsync(x => x.NumeroFactura == id);
            if(faturas == null)
            {
                return NotFound();
            }
            var facturasDTO = mapper.Map<FacturaDTO>(faturas);
            return facturasDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FacturasCreacionDTO facturasCreacion)
        {
            var facturas = mapper.Map<Factura>(facturasCreacion);
            contexto.Add(facturas);
            await contexto.SaveChangesAsync();
            var facturasDTO = mapper.Map<FacturaDTO>(facturas);
            return new CreatedAtRouteResult("GetFactura", new { id = facturas.NumeroFactura }, facturasDTO);
        } 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] FacturasCreacionDTO facturaActualizar)
        {
            var facturas = mapper.Map<DetalleFacturasCreacionDTO>(facturaActualizar);
            facturas.NumeroFactura = id;
            contexto.Entry(facturas).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<FacturaDTO>> Delete(int id)
        {
            var numeroFactura = await contexto.Facturas.Select(x => x.NumeroFactura).FirstOrDefaultAsync(x => x == id);
            if(numeroFactura== default(int))
            {
                return NoContent();
            }
            contexto.Remove(new Factura { NumeroFactura = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }

    }
    
}
