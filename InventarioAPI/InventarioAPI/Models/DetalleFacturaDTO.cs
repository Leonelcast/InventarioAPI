using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleFacturaDTO
    {

        public int CodigoDetalle { get; set; }

        public int NumeroFactura { get; set; }

        public int CodigoProducto { get; set; }
        
        public int Cantidad { get; set; }

        public decimal Precio { get; set; }
    
        public decimal Descuento { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Factura Facturas { get; set; }
    }
}
