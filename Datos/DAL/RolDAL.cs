using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class RolDAL
    {
        public static RolVMR LeerUno(long id)
        {
            RolVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Rol.Where(x => x.id == id).Select(x => new RolVMR
                {
                    id = x.id,
                    description = x.description,
                }).FirstOrDefault();
            }

            return item;
        }

    }
}
