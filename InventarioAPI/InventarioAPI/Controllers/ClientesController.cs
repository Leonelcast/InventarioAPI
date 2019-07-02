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
    public class ClientesController : ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public ClientesController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDTO>>> Get()
        {
            var clientes = await contexto.Clientes.ToListAsync();
            var clienteDTO = mapper.Map<List<ClienteDTO>>(clientes);
            return clienteDTO;
        }
        [HttpGet("{id}", Name ="GetCliente")]
        public async Task<ActionResult<ClienteDTO>> Get(string id)
        {
            var cliente = await contexto.Clientes.FirstOrDefaultAsync(z => z.Nit == id);
            if (cliente == null)
            {
                return NotFound();
            }
            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            return clienteDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ClientesCreacionDTO clientesCreacion)
        {
            var cliente = mapper.Map<Cliente>(clientesCreacion);
            contexto.Add(cliente);
            await contexto.SaveChangesAsync();
            var clienteDTO = mapper.Map<ClienteDTO>(cliente);
            return new CreatedAtRouteResult("GetCliente", new { id = cliente.Nit }, clienteDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] ClientesCreacionDTO clienteActualizar)
        {
            var cliente = mapper.Map<Cliente>(clienteActualizar);
            cliente.Nit = id;
            contexto.Entry(cliente).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<ClienteDTO>> Delete(string id)
        {
            var Nit = await contexto.Clientes.Select(z => z.Nit)
                .FirstOrDefaultAsync(z => z == id);
            if(Nit == default(string))
            {
                return NotFound();
            }
            contexto.Remove(new Cliente { Nit = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }
    }
}
