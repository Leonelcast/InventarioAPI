using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Factura
    {
        public int NumeroFactura { get; set; }
        [Required]
        public string Nit { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [Required]
        public int Total { get; set; }
        public virtual List<DetalleFactura> DetalleFacturas { get; set; }
        public virtual Cliente Clientes { get; set; }
    }
}
