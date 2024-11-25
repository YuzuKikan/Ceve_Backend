using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class Type_ImageDAL
    {
        public static Type_ImageVMR LeerUno(long id)
        {
            Type_ImageVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Type_Image.Where(x => x.id == id).Select(x => new Type_ImageVMR
                {
                    id = x.id,
                    description = x.description,
                }).FirstOrDefault();
            }

            return item;
        }

        public static void Actualizar(Type_ImageVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Type_Image.Find(item.id);

                itemUpdate.description = item.description;

                db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }
    }
}
