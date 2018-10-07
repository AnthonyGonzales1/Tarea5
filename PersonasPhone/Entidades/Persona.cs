using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonasPhone.Entidades
{
    public class Persona
    {
        [Key]
        public int IdPersona { get; set; }
        public string Nombre { get; set; }
        public int Cedula { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public virtual List<TelefonoDetalle> Telefonos { get; set; }

        /*
        public Persona(int id, string nombre, string apellido, DateTime fecha, List<Telefono> telefonoDetalle)
        {
            this.Nombre = nombre;
            this.Cedula = apellido;
            this.FechaNacimiento = fecha;
            this.TelefonoDetalle = telefonoDetalle;
        }*/

        public Persona()
        {
            this.IdPersona = 0;
            this.Nombre = string.Empty;
            this.Cedula = 0;
            this.FechaNacimiento = DateTime.Now;

            Telefonos = new List<TelefonoDetalle>();
        }
    }
}
