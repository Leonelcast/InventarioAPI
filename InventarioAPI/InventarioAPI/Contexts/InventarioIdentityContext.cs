using InventarioAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventarioAPI.Contexts
{
    public class InventarioIdentityContext: IdentityDbContext<ApplicationUser>
    {
        public InventarioIdentityContext(DbContextOptions<InventarioIdentityContext> options): base(options)
        {

        }


    }
}
