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
    public class ProfileController : ApiController
    {
        // [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/profile/leer-todo")]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<ProfileVMR>>();

            try
            {
                respuesta.datos = ProfileBLL.LeerTodo(cantidad, pagina, textoBusqueda);
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
        [HttpGet]
        [Route("api/profile/leer-uno/{id}")]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<ProfileVMR>();

            try
            {
                respuesta.datos = ProfileBLL.LeerUno(id);
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            if (respuesta.datos == null && respuesta.mensajes.Count() == 0)
            {
                respuesta.codigo = HttpStatusCode.NotFound;
                respuesta.mensajes.Add("Elemento no encontrado");
            }
            return Content(respuesta.codigo, respuesta);
        }
        [HttpGet]
        [Route("api/profile/actualizar/{id}")]
        public IHttpActionResult Actualizar(int id, ProfileVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.user_id = id;
                ProfileBLL.Actualizar(item);
                respuesta.datos = true;
                respuesta.mensajes.Add("Perfil actualizado correctamente.");
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
        [HttpGet]
        [Route("api/profile/eliminar")]
        public IHttpActionResult Eliminar(List<long> ids)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                ProfileBLL.Eliminar(ids);
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
LeerTodo: GET https://www.creatorverse.somee.com/api/profile/leer-todo
LeerUno: GET https://www.creatorverse.somee.com/api/profile/leer-uno/{id}
Actualizar: PUT https://www.creatorverse.somee.com/api/profile/actualizar/{id}
Eliminar: DELETE https://www.creatorverse.somee.com/api/profile/eliminar
*/