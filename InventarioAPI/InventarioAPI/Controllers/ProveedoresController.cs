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
    public class ProveedoresController: ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public ProveedoresController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorDTO>>> Get()
        {
            var proveedores = await contexto.Proveedores.ToListAsync();
            var proveedoresDTO = mapper.Map<List<ProveedorDTO>>(proveedores);
            return proveedoresDTO;
        }
        [HttpGet("{id}",Name ="GetProveedor")]
        public async Task<ActionResult<ProveedorDTO>> Get(int id)
        {
            var proveedores = await contexto.Proveedores.FirstOrDefaultAsync(x => x.CodigoProveedor == id);
            if (proveedores== null)
            {
                return NotFound();
            }
            var proveedorDTO = mapper.Map<ProveedorDTO>(proveedores);
            return proveedorDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ProveedorCreacionDTO ProveedorCreacion)
        {
            var proveedor = mapper.Map<Proveedor>(ProveedorCreacion);//mapea el objeto categoria 
            contexto.Add(proveedor);//se almacena en la DB
            await contexto.SaveChangesAsync();
            var proveedorDTO = mapper.Map<ProveedorDTO>(proveedor);//setea a categoria dto
            return new CreatedAtRouteResult("GetCategoria", new { id = proveedor.CodigoProveedor }, proveedorDTO);//devuelve el id que le asigno la DB
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ProveedorCreacionDTO proveedorActualizar)
        {
            var proveedor = mapper.Map<Proveedor>(proveedorActualizar);
            proveedor.CodigoProveedor = id;
            contexto.Entry(proveedor).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProveedorDTO>> Delete(int id)
        {
            var codigoProveedor = await contexto.Proveedores.Select(x => x.CodigoProveedor)
                .FirstOrDefaultAsync(x => x == id);
            if (codigoProveedor == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new Proveedor { CodigoProveedor = id });
            await contexto.SaveChangesAsync();
            return NoContent();

        }


    }
}
