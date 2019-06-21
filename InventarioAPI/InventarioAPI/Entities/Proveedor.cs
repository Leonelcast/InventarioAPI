using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Entities
{
    public class Proveedor
    {
        public int CodigoProveedor { get; set; }
        [Required]
        public string Nit { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [Required]
        public string Direccion { get; set; }
        [Required]
        public string PaginaWeb { get; set; }
        public string ContactoPrincipal { get; set; }
        public virtual List<EmailProveedor> EmailProveedores { get; set; }
        public virtual List<Compra> Compras { get; set; }
        public virtual List<TelefonoProveedor> TelefonoProveedores { get; set; }
    }
}
