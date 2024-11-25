using Comun.ViewModels;
using Datos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class ProfileBLL
    {
        public static ListadoPaginadoVMR<ProfileVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return ProfileDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }
        public static ProfileVMR LeerUno(long id)
        {
            return ProfileDAL.LeerUno(id);
        }
        public static void Actualizar(ProfileVMR item)
        {
            ProfileDAL.Actualizar(item);
        }
        public static void Eliminar(List<long> ids)
        {
            ProfileDAL.Eliminar(ids);
        }
    }
}
