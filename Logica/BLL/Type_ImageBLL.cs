using Comun.ViewModels;
using Datos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class Type_ImageBLL
    {
        public static Type_ImageVMR LeerUno(long id)
        {
            return Type_ImageDAL.LeerUno(id);
        }
        public static void Actualizar(Type_ImageVMR item)
        {
            Type_ImageDAL.Actualizar(item);
        }
    }
}
