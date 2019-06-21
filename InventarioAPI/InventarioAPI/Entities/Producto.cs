using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Producto
    {       
        public int CodigoProducto { get; set; }
        [Required]
        public int CodigoCategoria { get; set; }
        [Required]
        public int CodigoEmpaque { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Required]
        public decimal PrecioUnitario { get; set; }
        [Required]
        public decimal PrecioPorDocena { get; set; }
        [Required]
        public decimal PrecioPorMayor { get; set; }
        [Required]
        public int Existencia { get; set; }
        public string Imagen { get; set; }
        public virtual Categoria Categoria { get; set; }
        public virtual TipoEmpaque TipoEmpaque { get; set; }
        public virtual List<Inventario> Inventario { get; set; }
        public virtual List<DetalleCompra> DetalleCompras { get; set; }
        public virtual List<DetalleFactura> DetalleFacturas { get; set; }
    }
}
