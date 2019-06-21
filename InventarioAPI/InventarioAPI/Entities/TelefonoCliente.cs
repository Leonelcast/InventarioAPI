using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class TelefonoCliente
    {
        public int CodigoTelefono { get; set; }
        [Required]
        public string Numero { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public string Nit { get; set; }
        public virtual Cliente Clientes { get; set; }
    }
}
