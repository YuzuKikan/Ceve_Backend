using Comun.Enumeration;
using Comun.Service;
using Comun.ViewModels;
using Logica.BLL;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebAPI.Controllers
{
    // [EnableCors(origins: "*", headers: "*", methods: "*")]
    // [EnableCors(origins: "*", headers: "*", methods: "GET, POST, PUT, DELETE, OPTIONS")]
    public class UserController : ApiController
    {
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/user/leer-todo")]
        public IHttpActionResult LeerTodo(int cantidad = 10, int pagina = 0, string textoBusqueda = null)
        {
            var respuesta = new RespuestaVMR<ListadoPaginadoVMR<UserVMR>>();

            try
            {
                respuesta.datos = UserBLL.LeerTodo(cantidad, pagina, textoBusqueda);
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
        [Route("api/user/leer-uno/{id}")]
        public IHttpActionResult LeerUno(long id)
        {
            var respuesta = new RespuestaVMR<UserVMR>();

            try
            {
                respuesta.datos = UserBLL.LeerUno(id);
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
        [Route("api/user/existe/{email}/password/{password}")]
        public IHttpActionResult ExisteUsuario(string email, string password)
        {
            var respuesta = new RespuestaVMR<UserStatus>();

            try
            {
                respuesta.datos = UserBLL.ExisteUsuario(email, password);
                respuesta.codigo = HttpStatusCode.OK; // Asume OK:200 por defecto
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
                return Content(respuesta.codigo, respuesta); // Retornar el error sin más procesamiento ni seguir
            }
            // Selecciona un caso según las opciones del enum para un mensaje adecuado y código HTTP
            switch (respuesta.datos)
            {
                case UserStatus.UsuarioNoEncontrado:
                    respuesta.codigo = HttpStatusCode.NotFound;
                    respuesta.mensajes.Add("Usuario no encontrado.");
                    break;

                case UserStatus.ContraseñaIncorrecta:
                    respuesta.codigo = HttpStatusCode.Unauthorized;
                    respuesta.mensajes.Add("Contraseña incorrecta.");
                    break;

                case UserStatus.UsuarioValido:
                    respuesta.codigo = HttpStatusCode.OK;
                    respuesta.mensajes.Add("Usuario válido.");
                    break;
            }

            return Content(respuesta.codigo, respuesta);
        }
        [HttpGet]
        [Route("api/user/obtener-por-credenciales/{email}/password/{password}")] // Ruta para el inicio de sesión
        public IHttpActionResult ObtenerUsuarioPorCredenciales(string email, string password)
        {
            var respuesta = new RespuestaVMR<UserVMR>();

            try
            {
                respuesta.datos = UserBLL.ObtenerUsuarioPorCredenciales(email, password);
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
                respuesta.mensajes.Add("Usuario no existe o contraseña incorrecta");
            }
            return Content(respuesta.codigo, respuesta);
        }

        /* #########################################################################
 #########################################################################
######################################################################### */
        [HttpGet]
        [Route("api/user/crear")]
        public IHttpActionResult Crear([FromUri] User item)
        {
            var respuesta = new RespuestaVMR<long?>();

            try
            {
                respuesta.datos = UserBLL.Crear(item);
                respuesta.mensajes.Add("Usuario creado con éxito.");
            }
            catch (Exception e)
            {
                // Captura cualquier excepción y prepara la respuesta de error
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }

        [HttpPost]
        [Route("api/user/crear")]
        public IHttpActionResult Crear2(User item)
        {
            var respuesta = new RespuestaVMR<long?>();

            if (!ModelState.IsValid)
            {
                respuesta.codigo = HttpStatusCode.BadRequest;
                respuesta.mensajes = ModelState
                                        .Where(ms => ms.Value.Errors.Count > 0)
                                        .Select(ms => $"{ms.Key}: {string.Join(", ", ms.Value.Errors.Select(e => e.ErrorMessage))}")
                                        .ToList();
                return Content(respuesta.codigo, respuesta);
            }
            try
            {
                respuesta.datos = UserBLL.Crear(item);
                respuesta.mensajes.Add("Usuario creado con éxito.");
            }
            catch (Exception e)
            {
                // Captura cualquier excepción y prepara la respuesta de error
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = null;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }
            return Content(respuesta.codigo, respuesta);
        }
        /*
        [HttpOptions]
        [Route("api/user/crear")]
        public IHttpActionResult HandlePreflightCrear()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "POST, PUT, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type");
            return ResponseMessage(response);
        }
        */
        /* #########################################################################
         #########################################################################
        ######################################################################### */
        [HttpGet]
        [Route("api/user/actualizar/{id}")]
        public IHttpActionResult Actualizar(int id, [FromUri] UserVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;
                UserBLL.Actualizar(item);
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
        /* #########################################################################
            #########################################################################
            ######################################################################### */
        [HttpGet]
        [Route("api/user/rol/{userId}")]
        public IHttpActionResult ActualizarRol(int userId)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                // Llamar al método de la capa de negocio para actualizar el rol
                UserBLL.ActualizarRol(userId);
                respuesta.datos = true;
                respuesta.mensajes.Add("Rol actualizado correctamente.");
            }
            catch (Exception e)
            {
                // Manejar errores y retornar los mensajes adecuados
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpGet]
        [Route("api/user/rol/{userId}/{userModId}")]
        public IHttpActionResult ActualizarRolAutorizado(int userId, int userModId)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                if (userId == userModId)
                {
                    respuesta.codigo = HttpStatusCode.BadRequest;
                    respuesta.datos = false;
                    respuesta.mensajes.Add("No está permitido cambiar su propio rol");
                    return Content(respuesta.codigo, respuesta);
                }
                // Llamar al método de la capa de negocio para actualizar el rol
                UserBLL.ActualizarRolAutorizado(userId, userModId);
                respuesta.datos = true;
                respuesta.mensajes.Add("Rol actualizado correctamente.");
            }
            catch (Exception e)
            {
                // Manejar errores y retornar los mensajes adecuados
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpPut]
        [Route("api/user/{userId}/rol/{nuevoRolId}/moderator/{rolModeradorId}")]
        public IHttpActionResult ActualizarRol2(int userId, int nuevoRolId, int rolModeradorId)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                // UserBLL.ActualizarRol(userId, nuevoRolId, rolModeradorId);
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
        /* #########################################################################
            #########################################################################
            ######################################################################### */
        [HttpGet]
        [Route("api/user/eliminar")]
        public IHttpActionResult Eliminar(List<long> ids)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                UserBLL.Eliminar(ids);
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
        /* #########################################################################
            #########################################################################
            ######################################################################### */
        [EnableCors(origins: "*", headers: "*", methods: "PUT, OPTIONS")]
        [HttpPut]
        [Route("api/user/actualizar/{id}")]
        public IHttpActionResult HoraActualizar(int id, [FromBody] UserVMR item)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                item.id = id;
                UserBLL.Actualizar(item);
                respuesta.datos = true;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add(e.Message);
            }
            return Content(respuesta.codigo, respuesta);
        }
        /* #########################################################################
    #########################################################################
    ######################################################################### */
        [HttpGet]
        [Route("api/user/verificarEmail/{email}/mail")]
        public IHttpActionResult VarificarEmail(string email)
        {
            var respuesta = new RespuestaVMR<UserStatus>();

            try
            {
                respuesta.datos = UserBLL.VerificarEmail(email);
                respuesta.codigo = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add(e.ToString());
                return Content(respuesta.codigo, respuesta);
            }
            switch (respuesta.datos)
            {
                case UserStatus.UsuarioNoEncontrado:
                    respuesta.codigo = HttpStatusCode.NotFound;
                    respuesta.mensajes.Add("Usuario no encontrado.");
                    break;

                case UserStatus.UsuarioValido:
                    respuesta.codigo = HttpStatusCode.OK;
                    respuesta.mensajes.Add("Usuario válido.");
                    break;
            }
            return Content(respuesta.codigo, respuesta);
        }

        [HttpGet]
        [Route("api/user/verificarEmailCode/{email}/{code}")]
        public IHttpActionResult VerificarEmailCode(string email, string code)
        {
            var respuesta = new RespuestaVMR<UserStatus>();

            try
            {
                respuesta.datos = UserBLL.VerificarEmailCode(email, code);
                respuesta.codigo = HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.mensajes.Add(e.Message);
                respuesta.mensajes.Add($"Error al enviar el correo: {e.Message}");
            }
            switch (respuesta.datos)
            {
                case UserStatus.UsuarioNoEncontrado:
                    respuesta.codigo = HttpStatusCode.NotFound;
                    respuesta.mensajes.Add("Usuario no encontrado.");
                    break;

                case UserStatus.CodeIncorrecta:
                    respuesta.codigo = HttpStatusCode.Unauthorized;
                    respuesta.mensajes.Add("Code incorrecta.");
                    break;

                case UserStatus.UsuarioValido:
                    respuesta.codigo = HttpStatusCode.OK;
                    respuesta.mensajes.Add("Código de recuperación ACEPTADO.");
                    break;
            }

            return Content(respuesta.codigo, respuesta);
        }

        [HttpGet]
        [Route("api/user/gestionarCuenta/{email}/{code}/{password}")]
        public IHttpActionResult GestionarCuenta(string email, string code, string password)
        {
            var respuesta = new RespuestaVMR<bool>();

            try
            {
                if (string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("La nueva contraseña no puede estar vacía o tenga espacios.");
                }
                UserBLL.ActualizarPassword(email, code, password);

                respuesta.codigo = HttpStatusCode.OK;
                respuesta.datos = true;
                respuesta.mensajes.Add("Gestionar cuenta del usuario exitosa.");
            }
            catch (Exception e)
            {
                respuesta.codigo = HttpStatusCode.InternalServerError;
                respuesta.datos = false;
                respuesta.mensajes.Add($"Error al gestionar la cuenta: {e.Message}");
            }

            return Content(respuesta.codigo, respuesta);
        }

        /*
        [HttpOptions]
        public IHttpActionResult Options()
        {
            return Ok();
        }
        [HttpOptions]
        [Route("api/user/crear")]
        public IHttpActionResult HandlePreflight()
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE, OPTIONS");
            response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            return ResponseMessage(response);
        }
        */

        /*
        LeerTodo:       api/user/leer-todo
        LeerUno:        api/user/leer-uno/{id}
        ExisteUsuario:  api/user/email/{email}/password/{password}
        Crear:          api/user/crear
        Actualizar:     api/user/actualizar/{id}
        ActualizarRol:  api/user/{userId}/rol/{nuevoRolId}/moderator/{rolModeradorId}
        Eliminar:       api/user/eliminar
        */
    }
}
/*
 LeerTodo:

Datos de entrada: Cantidad de elementos a leer, página actual y texto de búsqueda.
Ejemplo de datos de entrada: typescript
Copiar código
            const cantidad = 10;
            const pagina = 0;
            const textoBusqueda = 'ejemplo';

LeerUno:

Datos de entrada: ID del elemento a leer.
Ejemplo de datos de entrada: typescript
Copiar código
            const id = 123;

ExisteUsuario:

Datos de entrada: Correo electrónico y contraseña.
Ejemplo de datos de entrada: typescript
Copiar código
            const email = 'usuario@example.com';
            const password = 'contraseña123';

ObtenerUsuarioPorCredenciales:

Datos de entrada: Correo electrónico y contraseña.
Ejemplo de datos de entrada: typescript
Copiar código
            const email = 'usuario@example.com';
            const password = 'contraseña123';

Crear:

Datos de entrada: Objeto con los detalles del usuario a crear.
Ejemplo de datos de entrada: typescript
Copiar código
        const nuevoUsuario = {
            name: 'Nombre',
            lastname: 'Apellido',
            username: 'nombre_apellido',
            email: 'usuario@example.com',
            password: 'contraseña123',
            rol_id: 2 // ID del rol
        };

Actualizar:

Datos de entrada: ID del usuario a actualizar y objeto con los campos a modificar.
Ejemplo de datos de entrada: typescript
Copiar código
            const id = 123;
            const usuarioActualizado = {
                name: 'Nuevo nombre',
                lastname: 'Nuevo apellido'
            };

ActualizarRol:

Datos de entrada: ID del usuario, nuevo ID del rol y ID del rol de moderador.
Ejemplo de datos de entrada: typescript
Copiar código
            const userId = 123;
            const nuevoRolId = 3;
            const rolModeradorId = 1;

Eliminar:

Datos de entrada: Lista de IDs de usuarios a eliminar.
Ejemplo de datos de entrada: typescript
Copiar código
            const idsAEliminar = [123, 456, 789];
 */