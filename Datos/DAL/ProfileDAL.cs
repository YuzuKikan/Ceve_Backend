using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class ProfileDAL
    {
        public static ListadoPaginadoVMR<ProfileVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<ProfileVMR> resultado = new ListadoPaginadoVMR<ProfileVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Profile.Where(x => !x.borrado).Select(x => new ProfileVMR
                {
                    id = x.id,
                    day_of_birth = x.day_of_birth,
                    phone = x.phone,
                    title = x.title,
                    bio = x.bio,
                    public_email = x.public_email,
                    image_id = x.image_id,
                    redes_id = x.redes_id,
                    user_id = x.user_id
                });

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(x => x.title.Contains(textoBusqueda));
                }

                resultado.cantidadTotal = query.Count();

                resultado.elemento = query.OrderBy(x => x.user_id).Skip(pagina * cantidad).Take(cantidad).ToList();
            }

            return resultado;
        }

        public static ProfileVMR LeerUno(long id)
        {
            ProfileVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Profile.Where(x => !x.borrado && x.user_id == id).Select(x => new ProfileVMR
                {
                    id = x.id,
                    day_of_birth = x.day_of_birth,
                    phone = x.phone,
                    title = x.title,
                    bio = x.bio,
                    public_email = x.public_email,
                    image_id = x.image_id,
                    redes_id = x.redes_id,
                    user_id = x.user_id
                }).FirstOrDefault();
            }

            return item;
        }

        public static void Actualizar(ProfileVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Profile.Find(item.user_id);

                if (itemUpdate != null)
                {
                    itemUpdate.day_of_birth = item.day_of_birth;
                    itemUpdate.phone = item.phone;
                    itemUpdate.title = item.title;
                    itemUpdate.bio = item.bio;
                    itemUpdate.public_email = item.public_email;
                    itemUpdate.image_id = item.image_id;
                    itemUpdate.redes_id = item.redes_id;

                    db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("No se han introducido datos Profile a actualizar");
                }
            }
        }

        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                // me aseguro que encuentro el usuario para seleccionar el profile
                var items = db.Profile.Where(x => ids.Contains(x.user_id)).ToList();
                // miro si hay profile disponibles a los que busco
                if (items.Any())
                {
                    foreach (var item in items)
                    {
                        item.borrado = true;
                        db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    }

                    db.SaveChanges();
                }
                else
                {
                    // caso donde no encontramos coincidencia
                    throw new Exception("No se encontraron perfiles con el ID");
                }

                db.SaveChanges();
            }
        }
    }
}
