using InventarioAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class TelefonoProveedorDTO
    {
        public int CodigoTelefono { get; set; }
        
        public string Numero { get; set; }
        
        public string Descripcion { get; set; }
        
        public int CodigoProveedor { get; set; }
        public Proveedor Proveedores { get; set; }
    }
}
