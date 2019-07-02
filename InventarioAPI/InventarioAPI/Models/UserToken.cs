using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Models
{
    public class UserToken
    {
        public string Tokern { get; set; }
        public DateTime Expiration { get; set; }
    }
}
