using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class EmailProveedorDTO
    {
        public int CodigoEmail { get; set; }
        
        public string Email { get; set; }
        
        public int CodigoProveedor { get; set; }
        public virtual Proveedor Proveedor { get; set; }
    }
}
