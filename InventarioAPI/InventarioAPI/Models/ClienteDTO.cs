﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class ClienteDTO
    {
        public string Nit { get; set; }
        
        public string DPI { get; set; }
        
        public string Nombre { get; set; }
        
        public string Direccion { get; set; }
    }
}
