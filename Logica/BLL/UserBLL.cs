using Comun.Enumeration;
using Comun.ViewModels;
using Datos.DAL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logica.BLL
{
    public class UserBLL
    {
        public static ListadoPaginadoVMR<UserVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            return UserDAL.LeerTodo(cantidad, pagina, textoBusqueda);
        }
        public static UserVMR LeerUno(long id)
        {
            return UserDAL.LeerUno(id);
        }
        public static UserStatus ExisteUsuario(string email, string password)
        {
            return UserDAL.ExisteUsuario(email, password);
        }
        public static UserStatus VerificarEmailCode(string email, string code)
        {
            return UserDAL.VerificarEmailCode(email, code);
        }
        public static UserStatus VerificarEmail(string email)
        {
            return UserDAL.VerificarEmail(email);
        }
        public static UserVMR ObtenerUsuarioPorCredenciales(string email, string password)
        {
            return UserDAL.ObtenerUsuarioPorCredenciales(email, password);
        }
        public static long Crear(User item)
        {
            return UserDAL.Crear(item);
        }
        public static void Actualizar(UserVMR item)
        {
            UserDAL.Actualizar(item);
        }
        public static void ActualizarRol(int userId)
        {
            UserDAL.ActualizarRol(userId);
        }
        public static void ActualizarRolAutorizado(int userId, int userModId)
        {
            UserDAL.ActualizarRolAutorizado(userId, userModId);
        }
        public static void ActualizarCode(string email, string code)
        {
            UserDAL.ActualizarCode(email, code);
        }
        public static void ActualizarPassword(string email, string code, string password)
        {
            UserDAL.ActualizarPassword(email, code, password);
        }
        public static void Eliminar(List<long> ids)
        {
            UserDAL.Eliminar(ids);
        }
        public static void ResetTotal(List<long> ids)
        {
            UserDAL.ResetTotal(ids);
        }
        public static void RestoreTotal(List<long> ids)
        {
            UserDAL.RestoreTotal(ids);
        }
    }
}
