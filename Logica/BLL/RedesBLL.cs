using Comun.ViewModels;
using Datos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class RedesBLL
    {
        public static ListadoPaginadoVMR<RedesVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return RedesDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }
        public static RedesVMR LeerUno(long id)
        {
            return RedesDAL.LeerUno(id);
        }
        public static void Actualizar(RedesVMR item)
        {
            RedesDAL.Actualizar(item);
        }

    }
}
