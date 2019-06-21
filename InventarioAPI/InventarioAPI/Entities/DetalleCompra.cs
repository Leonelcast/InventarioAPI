using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class DetalleCompra
    {
        public int IdDetalle { get; set; }
        [Required]
        public int IdCompra { get; set; }
        [Required]
        public int CodigoProduto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Compra Compras { get; set; }
    }
}
