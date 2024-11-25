using Comun.ViewModels;
using Logica.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

namespace WebAPI.Controllers
{
    public class User1_User2Controller : ApiController
    {
        // [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/user1-user2/cant-seguimientos/{userId}")]
        public IHttpActionResult CantSeguimientos(int userId)
        {
            var respuesta = new RespuestaVMR<(int, List<(int userId, string userName)>)>();

            try
            {
                respuesta.datos = User1_User2BLL.CantSeguimientos(userId);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = (0, null);
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

        [HttpGet]
        [Route("api/user1-user2/cant-seguidores/{userId}")]
        public IHttpActionResult CantSeguidores(int userId)
        {
            var respuesta = new RespuestaVMR<(int, List<(int seguidorId, string seguidorName)>)>();

            try
            {
                respuesta.datos = User1_User2BLL.CantSeguidores(userId);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = (0, null);
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }
        /* #########################################################################
#########################################################################
######################################################################### */
        // Parte de la creación de nuevos elementos
        [HttpGet]
        [Route("api/user1-user2/crear/{user_remitente_id}/{user_receptor_id}")]
        public IHttpActionResult Crear(int user_remitente_id, int user_receptor_id)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                // Validar que los IDs no sean iguales
                if (user_remitente_id == user_receptor_id)
                {
                    respuesta.codigo = HttpStatusCode.BadRequest;
                    respuesta.mensajes.Add("El remitente y el receptor no pueden ser la misma persona.");
                    return Content(respuesta.codigo, respuesta);
                }

                // Validar que los IDs sean mayores que cero
                if (user_remitente_id <= 0 || user_receptor_id <= 0)
                {
                    respuesta.codigo = HttpStatusCode.BadRequest;
                    respuesta.mensajes.Add("IDs deben ser mayores que cero.");
                    return Content(respuesta.codigo, respuesta);
                }

                respuesta.datos = User1_User2BLL.Crear(user_remitente_id, user_receptor_id);


            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

        [HttpPost]
        [Route("api/user1-user2/crear/{user_remitente_id}/{user_receptor_id}")]
        public IHttpActionResult Crear2(int user_remitente_id, int user_receptor_id)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.datos = User1_User2BLL.Crear(user_remitente_id, user_receptor_id);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

        /* #########################################################################
#########################################################################
######################################################################### */
        // Parte de la actualización/mantenimiento/modificación de elementos existentes y crear nuevas
        [HttpPut]
        [Route("api/user1-user2/actualizar/{user_remitente_id}/{user_receptor_id}")]
        public IHttpActionResult Actualizar(int user_remitente_id, int user_receptor_id)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                User1_User2BLL.Actualizar(user_remitente_id, user_receptor_id);
                respuesta.datos = true;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

        [HttpDelete]
        [Route("api/user1-user2/eliminar/{user_remitente_id}/{user_receptor_id}")]
        public IHttpActionResult Eliminar(int user_remitente_id, int user_receptor_id)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                User1_User2BLL.Eliminar(user_remitente_id, user_receptor_id);
                respuesta.datos = true;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

    }
}
/*
CantSeguimientos: GET https://www.creatorverse.somee.com/api/user1-user2/cant-seguimientos/{userId}
CantSeguidores: GET https://www.creatorverse.somee.com/api/user1-user2/cant-seguidores/{userId}
Crear: POST https://www.creatorverse.somee.com/api/user1-user2/crear
Actualizar: PUT https://www.creatorverse.somee.com/api/user1-user2/actualizar
Eliminar: DELETE https://www.creatorverse.somee.com/api/user1-user2/eliminar
 */