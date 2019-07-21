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
    public class TelefonoProveedoresController: ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public TelefonoProveedoresController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoProveedorDTO>>> Get()
        {
            var telefonoProveedores = await contexto.TelefonoProveedores.ToListAsync();
            var telefonoProveedoresDTO = mapper.Map<List<TelefonoProveedorDTO>>(telefonoProveedores);
            return telefonoProveedoresDTO;
        }
        [HttpGet("{id}", Name ="GetTelefonoProveedores")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Get(int id)
        {
            var telefonoProveedores = await contexto.TelefonoProveedores.FirstOrDefaultAsync(x => x.CodigoTelefono== id);
            if(telefonoProveedores== null)
            {
                return  NotFound();
            }
            var telefonoProveedoresDTO = mapper.Map<TelefonoProveedorDTO>(telefonoProveedores);
            return telefonoProveedoresDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoProveedoresCreacionDTO telefonoProveedoresCreacion)
        {
            var proveedor = contexto.Proveedores.FirstOrDefault(x => x.CodigoProveedor == telefonoProveedoresCreacion.CodigoProveedor);
            var telefonoProveedores = mapper.Map<TelefonoProveedor>(telefonoProveedoresCreacion);
            telefonoProveedores.Proveedores = proveedor;
            contexto.Add(telefonoProveedores);
            await contexto.SaveChangesAsync();
            var telefonoProveedoresDTO = mapper.Map<TelefonoProveedorDTO>(telefonoProveedores);
            return new CreatedAtRouteResult("GetTelefonoProveedores", new { id = telefonoProveedores.CodigoTelefono }, telefonoProveedoresDTO);
        } 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoProveedoresCreacionDTO telefonoProveedorActualizar)
        {
            var telefonoProveedores = mapper.Map<TelefonoProveedor>(telefonoProveedorActualizar);
            telefonoProveedores.CodigoTelefono = id;
            contexto.Entry(telefonoProveedores).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoProveedorDTO>> Delete(int id)
        {
            var codigoTelefono = await contexto.TelefonoProveedores.Select(x => x.CodigoTelefono).FirstOrDefaultAsync(x => x == id);
            if (codigoTelefono == default(int))
            {
                return NoContent();

            }
            contexto.Remove(new TelefonoProveedor { CodigoTelefono = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
