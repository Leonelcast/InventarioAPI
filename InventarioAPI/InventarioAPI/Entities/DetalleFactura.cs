using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class DetalleFactura
    {
        public int CodigoDetalle { get; set; }
        [Required]
        public int NumeroFactura { get; set; }
        [Required]
        public int CodigoProducto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        [Required]
        public decimal Descuento { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Factura Facturas { get; set; }
    }
}
