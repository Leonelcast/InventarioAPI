using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class FacturaDTO
    {

        public int NumeroFactura { get; set; }
       
        public string Nit { get; set; }
     
        public DateTime Fecha { get; set; }
     
        public int Total { get; set; }
        public virtual List<DetalleFactura> DetalleFacturas { get; set; }
        public virtual Cliente Clientes { get; set; }
    }
}
