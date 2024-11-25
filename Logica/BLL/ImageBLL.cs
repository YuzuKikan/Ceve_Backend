using Comun.ViewModels;
using Datos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class ImageBLL
    {
        public static ListadoPaginadoVMR<ImageVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return ImageDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }
        public static ImageVMR LeerUno(long id)
        {
            return ImageDAL.LeerUno(id);
        }
        public static void Actualizar(ImageVMR item)
        {
            ImageDAL.Actualizar(item);
        }
        public static void Eliminar(List<long> ids)
        {
            ImageDAL.Eliminar(ids);
        }
    }
}
