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
    public class TipoEmpaquesController: ControllerBase
    {
            private readonly InventarioDBContext contexto;
            private readonly IMapper mapper;

            public TipoEmpaquesController(InventarioDBContext contexto, IMapper mapper)
            {
                this.contexto = contexto;
                this.mapper = mapper;

            }
            [HttpGet]
            public async Task<ActionResult<IEnumerable<TipoEmpaqueDTO>>> Get()
            {
                var tipoEmpaques = await contexto.TipoEmpaques.ToListAsync();
                var tipoEmpaqueDTO = mapper.Map<List<TipoEmpaqueDTO>>(tipoEmpaques);
                return tipoEmpaqueDTO;

            }
            [HttpGet("{id}, Name = GetTipoEmpaque")]
            public async Task<ActionResult<TipoEmpaqueDTO>> Get(int id)
            {
                var tipoEmpaque = await contexto.TipoEmpaques.FirstOrDefaultAsync(z => z.CodigoEmpaque == id);
                if (tipoEmpaque == null)
                {
                    return NotFound();
                }
                var tipoEmpaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoEmpaque);
                return tipoEmpaqueDTO;
            }
            [HttpPost]
            public async Task<ActionResult> Post([FromBody] TipoEmpaqueCreacionDTO tipoEmpaqueCreacion)
            {
                var tipoEmpaque = mapper.Map<TipoEmpaque>(tipoEmpaqueCreacion);
                contexto.Add(tipoEmpaque);
                await contexto.SaveChangesAsync();
                var tipoEmpaqueDTO = mapper.Map<TipoEmpaqueDTO>(tipoEmpaque);
                return new CreatedAtRouteResult("GetTipoEmpaque", new { id = tipoEmpaque.CodigoEmpaque }, tipoEmpaqueDTO);
            }
            [HttpPut("{id}")]
            public async Task<ActionResult> Put(int id, [FromBody] TipoEmpaqueCreacionDTO tipoEmpaqueActualizar)
            {
                var tipoEmpaque = mapper.Map<TipoEmpaque>(tipoEmpaqueActualizar);
                tipoEmpaque.CodigoEmpaque = id;
                contexto.Entry(tipoEmpaque).State = EntityState.Modified;
                await contexto.SaveChangesAsync();
                return NoContent();

            }
            [HttpDelete("{id}")]
            public async Task<ActionResult<TipoEmpaqueDTO>> Delete(int id)
            {
                var codigoEmpaque = await contexto.TipoEmpaques.Select(x => x.CodigoEmpaque)
                    .FirstOrDefaultAsync(x => x == id);
                if(codigoEmpaque == default(int))
                {
                    return NotFound();
                }
                contexto.Remove(new TipoEmpaque { CodigoEmpaque = id });
                await contexto.SaveChangesAsync();
                return NoContent();
            }
         

        }
    }

