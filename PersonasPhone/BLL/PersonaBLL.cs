using PersonasPhone.DAL;
using PersonasPhone.Entidades;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonasPhone.BLL
{
    public class PersonaBLL
    {
        public static bool Guardar(Persona persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Per.Add(persona) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var eliminar = contexto.Per.Find(id);
                if (contexto.Per.Remove(eliminar) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }

            }
            catch (Exception)
            {

                throw;
            }
            return paso;

        }

        public static bool Modificar(Persona persona)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var anterior = contexto.Per.Find(persona.IdPersona);
                foreach (var item in anterior.Telefonos)
                {
                    if (!persona.Telefonos.Exists(d => d.Id == item.Id))
                        contexto.Entry(item).State = EntityState.Deleted;
                        
                }
                contexto.Entry(persona).State = EntityState.Modified;
                paso = (contexto.SaveChanges() > 0);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                contexto.Dispose();
            }
            return paso;
        }

        public static Persona Buscar(int id)
        {
            Contexto contexto = new Contexto();
            Persona persona = new Persona();
            try
            {
                persona = contexto.Per.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            return persona;
        }

        public static List<Persona> GetList(Expression<Func<Persona, bool>> per)
        {
            List<Persona> persona = new List<Persona>();
            Contexto contexto = new Contexto();
            try
            {
                persona = contexto.Per.Where(per).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return persona;
        }
    }
}
