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
    public class ImageController : ApiController
    {
        // [EnableCors(origins: "*", headers: "*", methods: "*")]
        [HttpGet]
        [Route("api/image/leer-todo")]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<ImageVMR>>();

            try
            {
                respuesta.datos = ImageBLL.LeerTodo(cantidad, pagina, textoBusqueda);
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
        [Route("api/image/leer-uno/{id}")]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<ImageVMR>();

            try
            {
                respuesta.datos = ImageBLL.LeerUno(id);
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
        [Route("api/image/actualizar/{id}")]
        public IHttpActionResult Actualizar(int id, ImageVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;
                ImageBLL.Actualizar(item);
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
        [Route("api/image/eliminar")]
        public IHttpActionResult Eliminar(List<long> ids)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                ImageBLL.Eliminar(ids);
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
LeerTodo: GET https://www.creatorverse.somee.com/api/image/leer-todo
LeerUno: GET https://www.creatorverse.somee.com/api/image/leer-uno/{id}
Actualizar: PUT https://www.creatorverse.somee.com/api/image/actualizar/{id}
Eliminar: DELETE https://www.creatorverse.somee.com/api/image/eliminar
*/