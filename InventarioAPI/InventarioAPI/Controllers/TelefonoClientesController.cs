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
    public class TelefonoClientesController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public TelefonoClientesController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TelefonoClienteDTO>>> Get()
        {
            var telefonoCLientes = await contexto.TelefonoClientes.ToListAsync();
            var telefonoClientesDTO = mapper.Map<List<TelefonoClienteDTO>>(telefonoCLientes);
            return telefonoClientesDTO;
        }
        [HttpGet("{id}", Name ="GetTelefonoCLientes")]
        public async Task<ActionResult<TelefonoClienteDTO>> Get(int id)
        {
            var telefonoClientes = await contexto.TelefonoClientes.FirstOrDefaultAsync(x => x.CodigoTelefono == id);
            if(telefonoClientes == null)
            {
                return NotFound();
            }
            var telefonoClientesDTO = mapper.Map<TelefonoClienteDTO>(telefonoClientes);
            return telefonoClientesDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TelefonoClientesCreacionDTO telefonoClientesCreacion)
        {
            var telefonoClientes = mapper.Map<TelefonoCliente>(telefonoClientesCreacion);
            contexto.Add(telefonoClientes);
            await contexto.SaveChangesAsync();
            var telefonoLCientesDTO = mapper.Map<TelefonoClienteDTO>(telefonoClientes);
            return new CreatedAtRouteResult("GetTelefonoCLientes", new { id = telefonoClientes.CodigoTelefono }, telefonoLCientesDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] TelefonoClientesCreacionDTO telefonoClienteActualizar)
        {
            var telefonoClientes = mapper.Map<TelefonoCliente>(telefonoClienteActualizar);
            telefonoClientes.CodigoTelefono = id;
            contexto.Entry(telefonoClientes).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<TelefonoClienteDTO>> Delete(int id)
        {
            var codigoTelefono = await contexto.TelefonoClientes.Select(x => x.CodigoTelefono).FirstOrDefaultAsync(x => x == id);
            if(codigoTelefono == default(int))
            {
                return NoContent();
            }
            contexto.Remove(new TelefonoCliente { CodigoTelefono = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        
    }
}
