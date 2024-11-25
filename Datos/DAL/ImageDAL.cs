using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class ImageDAL
    {
        public static ListadoPaginadoVMR<ImageVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<ImageVMR> resultado = new ListadoPaginadoVMR<ImageVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Image.Where(x => !x.borrado).Select(x => new ImageVMR
                {
                    id = x.id,
                    src = x.src,
                    type = x.type,
                    created_at = x.created_at,
                    user_id = x.user_id
                }); ;

                resultado.cantidadTotal = query.Count();

                resultado.elemento = query.OrderBy(x => x.id).Skip(pagina * cantidad).Take(cantidad).ToList();
            }

            return resultado;
        }

        public static ImageVMR LeerUno(long id)
        {

            ImageVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Image.Where(x => !x.borrado && x.user_id == id).Select(x => new ImageVMR
                {
                    id = x.id,
                    src = x.src,
                    type = x.type,
                    created_at = x.created_at,
                    user_id = x.user_id
                }).FirstOrDefault();
            }
            return item;
        }

        public static void Actualizar(ImageVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Image.Find(item.id);

                if (itemUpdate != null)
                {
                    itemUpdate.src = item.src;
                    itemUpdate.created_at = DateTime.Now;
                    itemUpdate.user_id = item.user_id;

                    db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró la imagen a actualizar");
                }
            }
        }

        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.Image.Where(x => ids.Contains(x.id));

                foreach (var item in items)
                {
                    item.borrado = true; // Marcar como borrado en lugar de eliminar
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }
        }
    }
}
