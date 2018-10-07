using PersonasPhone.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasPhone.DAL
{
    public class Contexto : DbContext
    {
        public DbSet<Persona> Per { get; set; }
        public DbSet<TelefonoDetalle> Tele { get; set; }
        public DbSet<TypeTelefono> Type { get; set; }

        public Contexto() : base("ConStr")
        {
        }
    }
}
