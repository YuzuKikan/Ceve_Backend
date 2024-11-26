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
    public class User1_User2BLL
    {
        public static (int, List<(int userId, string userName)>) CantSeguimientos(int userId)
        {
            return User1_User2DAL.CantSeguimientos(userId);
        }

        public static (int, List<(int seguidorId, string seguidorName)>) CantSeguidores(int userId)
        {
            return User1_User2DAL.CantSeguidores(userId);
        }

        public static ResultDetail Crear(int user_remitente_id, int user_receptor_id)
        {
            return User1_User2DAL.Crear(user_remitente_id, user_receptor_id);
        }


        public static void Actualizar(int user_remitente_id, int user_receptor_id)
        {
            User1_User2DAL.Actualizar(user_remitente_id, user_receptor_id);
        }

        public static void Eliminar(int user_remitente_id, int user_receptor_id)
        {
            User1_User2DAL.Eliminar(user_remitente_id, user_receptor_id);
        }
    }
}
