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
    public class Type_ImageController : ApiController
    {
        // [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/type-image/leer-uno/{id}")]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<Type_ImageVMR>();

            try
            {
                respuesta.datos = Type_ImageBLL.LeerUno(id);
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
        [HttpPut]
        [Route("api/type-image/actualizar/{id}")]
        public IHttpActionResult Actualizar(int id, Type_ImageVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;
                Type_ImageBLL.Actualizar(item);
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
LeerUno: GET https://www.creatorverse.somee.com/api/type-image/leer-uno/{id}
Actualizar: PUT https://www.creatorverse.somee.com/api/type-image/actualizar/{id}
 */