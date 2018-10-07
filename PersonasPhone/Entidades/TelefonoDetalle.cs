using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasPhone.Entidades
{
    public class TelefonoDetalle
    {
        [Key]
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public string TypeTelefonos { get; set; }
        public string Telefonos { get; set; }

        
        public TelefonoDetalle(int id, int idPersona, string telefonos, string typeTelefono)
        {
            this.Id = id;
            this.IdPersona = idPersona;
            this.Telefonos = telefonos;
            this.TypeTelefonos = typeTelefono;
        }

        public TelefonoDetalle()
        {
            this.Id = 0;
            this.IdPersona = 0;
            this.Telefonos = string.Empty;
            this.TypeTelefonos = string.Empty;
        }
    }
}
