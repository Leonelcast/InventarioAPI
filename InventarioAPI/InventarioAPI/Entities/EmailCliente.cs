using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class EmailCliente
    {
        public int CodigoEmail { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Nit { get; set; }
        public virtual Cliente Clientes { get; set; }


    }

}
