using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class DetalleCompraDTO
    {
        public int IdDetalle { get; set; }
     
        public int IdCompra { get; set; }
       
        public int CodigoProduto { get; set; }
        
        public int Cantidad { get; set; }
    
        public decimal Precio { get; set; }
        public virtual Producto Producto { get; set; }
        public virtual Compra Compras { get; set; }
    }
}
