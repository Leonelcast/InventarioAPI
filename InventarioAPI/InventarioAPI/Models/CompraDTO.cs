using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class CompraDTO
    {
        public int IdCompra { get; set; }
       
        public int NumeroDocumento { get; set; }

        public int CodigoProveedor { get; set; }
    
        public DateTime Fecha { get; set; }
      
        public decimal Total { get; set; }
        public virtual List<DetalleCompra> DetalleCompras { get; set; }
        public virtual Proveedor Proveedores { get; set; }
    }
}
