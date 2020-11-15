using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API_JSL.Models;

namespace API_JSL.Models
{
    public class JSL_Context : DbContext
    {
        public JSL_Context (DbContextOptions<JSL_Context> options)
            : base(options)
        {
        }

        public DbSet<API_JSL.Models.Caminhao> Caminhao { get; set; }

        public DbSet<API_JSL.Models.Endereco> Endereco { get; set; }

        public DbSet<API_JSL.Models.Motorista> Motorista { get; set; }
    }
}
