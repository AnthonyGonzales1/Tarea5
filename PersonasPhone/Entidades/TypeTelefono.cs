using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasPhone.Entidades
{
    public class TypeTelefono
    {
        public int TipoTelefonoId { get; set; }
        public string TipoTelefono { get; set; }

        public TypeTelefono()
        {
            this.TipoTelefonoId = 0;
            this.TipoTelefono = string.Empty;
        }

        public TypeTelefono(int tipopartidoid, string descripcion)
        {
            this.TipoTelefonoId = tipopartidoid;
            this.TipoTelefono = descripcion;
        }
    }
}
