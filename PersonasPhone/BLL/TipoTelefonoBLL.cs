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
    public class TipoTelefonoBLL
    {
        public static bool Guardar(TypeTelefono typeTelefono)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Type.Add(typeTelefono) != null)
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
                var eliminar = contexto.Type.Find(id);
                if (contexto.Type.Remove(eliminar) != null)
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

        public static bool Modificar(TypeTelefono typeTelefono)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var anterior = contexto.Type.Find(typeTelefono.TipoTelefonoId);
                foreach (var item in anterior.TipoTelefono)
                {
                    if (!typeTelefono.TipoTelefonoId.Exists(d => d.Id == item.Id))
                        contexto.Entry(item).State = EntityState.Deleted;
                        
                }
                contexto.Entry(typeTelefono).State = EntityState.Modified;
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

        public static TypeTelefono Buscar(int id)
        {
            Contexto contexto = new Contexto();
            TypeTelefono typeTelefono = new TypeTelefono();
            try
            {
                typeTelefono = contexto.Type.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
            return typeTelefono;
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
