using Comun.ViewModels;
using Datos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class RolBLL
    {
        public static RolVMR LeerUno(long id)
        {
            return RolDAL.LeerUno(id);
        }
    }
}
