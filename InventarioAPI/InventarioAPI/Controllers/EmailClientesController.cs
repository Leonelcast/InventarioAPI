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
    public class EmailClientesController: ControllerBase
    {
        private readonly InventarioDBContext contexto;
        private readonly IMapper mapper;
        public EmailClientesController(InventarioDBContext contexto, IMapper mapper)
        {
            this.contexto = contexto;
            this.mapper = mapper;


        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmailClienteDTO>>> Get()
        {
            var emailClientes = await contexto.EmailClientes.ToListAsync();
            var emailClienteDTO = mapper.Map<List<EmailClienteDTO>>(emailClientes);
            return emailClienteDTO;
        }
        [HttpGet("{id}", Name ="GetEmailClientes")]
        public async Task<ActionResult<EmailClienteDTO>> Get(int id)
        {
            var emailClientes = await contexto.EmailClientes.FirstOrDefaultAsync(x => x.CodigoEmail == id);
            if(emailClientes == null)
            {
                return NotFound();
            }
            var emailCLientesDTO = mapper.Map<EmailClienteDTO>(emailClientes);
            return emailCLientesDTO;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmailCLientesCreacionDTO emailCLientesCreacion)
        {
            var emailCliente = mapper.Map<EmailCliente>(emailCLientesCreacion);
            contexto.Add(emailCliente);
            await contexto.SaveChangesAsync();
            var emailClienteDTO = mapper.Map<EmailClienteDTO>(emailCliente);
            return new CreatedAtRouteResult("GetEmailClientes", new { id = emailCliente.CodigoEmail }, emailClienteDTO);
        } 
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] EmailCLientesCreacionDTO emailCLienteActualizar)
        {
            var emailCliente = mapper.Map<EmailCliente>(emailCLienteActualizar);
            emailCliente.CodigoEmail = id;
            contexto.Entry(emailCliente).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmailClienteDTO>> Delete(int id)
        {
            var codigoEmail = await contexto.EmailClientes.Select(x => x.CodigoEmail).FirstOrDefaultAsync(x => x == id);
            if (codigoEmail == default(int))
            {
                return NotFound();
            }
            contexto.Remove(new EmailCliente { CodigoEmail = id });
            await contexto.SaveChangesAsync();
            return NoContent();
        }

    }
   
}
