using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class EmailClienteDTO
    {
        public int CodigoEmail { get; set; }
        
        public string Email { get; set; }
  
        public string Nit { get; set; }
        public virtual Cliente Clientes { get; set; }
    }
}
