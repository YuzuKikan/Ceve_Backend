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
    public class RedesController : ApiController
    {
        // [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/redes/leer-todo")]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<RedesVMR>>();

            try
            {
                respuesta.datos = RedesBLL.LeerTodo(cantidad, pagina, textoBusqueda);
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
        [Route("api/redes/leer-uno/{id}")]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<RedesVMR>();

            try
            {
                respuesta.datos = RedesBLL.LeerUno(id);
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
        [Route("api/redes/actualizar/{id}")]
        public IHttpActionResult Actualizar(int id, RedesVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;
                RedesBLL.Actualizar(item);
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
Leer todo: GET https://www.creatorverse.somee.com/api/redes/leer-todo
Leer uno: GET https://www.creatorverse.somee.com/api/redes/leer-uno/{id}
Actualizar: PUT https://www.creatorverse.somee.com/api/redes/actualizar/{id}
 */