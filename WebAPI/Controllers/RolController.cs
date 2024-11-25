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
    public class RolController : ApiController
    {
        // [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/rol/leer-uno/{id}")]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<RolVMR>();

            try
            {
                respuesta.datos = RolBLL.LeerUno(id);
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
    }
}
/*
 LeerUno: GET https://www.creatorverse.somee.com/api/rol/leer-uno/{id}
 */