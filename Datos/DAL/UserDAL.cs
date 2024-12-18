﻿using Comun.Enumeration;
using Comun.Service;
using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class UserDAL
    {
        public static ListadoPaginadoVMR<UserVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<UserVMR> resultado = new ListadoPaginadoVMR<UserVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.User.Where(x => !x.borrado).Select(x => new UserVMR
                {
                    id = x.id,
                    name = x.name + " " + (x.lastname != null ? (" " + x.lastname) : " "),
                    username = x.username != null ? (x.username) : (x.name),
                    email = x.email != null ? x.email : " ",
                    password = x.password,
                    code = x.code,
                    is_active = x.is_active,
                    rol_id = x.rol_id,
                    created_at = x.created_at
                });
                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query = query.Where(x => x.name.Contains(textoBusqueda) || x.lastname.Contains(textoBusqueda) || x.username.Contains(textoBusqueda));
                }
                resultado.cantidadTotal = query.Count();

                resultado.elemento = query.OrderBy(x => x.id).Skip(pagina * cantidad).Take(cantidad).ToList();
            }

            return resultado;
        }

        public static UserVMR LeerUno(long id)
        {
            UserVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.User.Where(x => !x.borrado && x.id == id).Select(x => new UserVMR
                {
                    id = x.id,
                    name = x.name,
                    lastname = x.lastname,
                    username = x.username,
                    email = x.email,
                    password = x.password,
                    code = x.code,
                    is_active = x.is_active,
                    rol_id = x.rol_id,
                    created_at = x.created_at
                }).FirstOrDefault();
            }

            return item;
        }

        //public static bool ExisteUsuario(string email, string password)
        //{
        //    using (var db = DbConexion.Create())
        //    {
        //        // Verificar si existe un usuario con el correo electrónico proporcionado
        //        var usuario = db.User.FirstOrDefault(x => x.email == email);

        //        if (usuario != null)
        //        {
        //            // Si el usuario existe, verificar si la contraseña coincide
        //            if (usuario.password == password)
        //            {
        //                // Si la contraseña coincide, se encontró el usuario
        //                return true;
        //            }
        //        }

        //        // Si no se encontró el usuario o la contraseña no coincide, devolver false
        //        return false;
        //    }
        //}

        public static UserStatus ExisteUsuario(string email, string password)
        {
            using (var db = DbConexion.Create())
            {
                // Verificar si existe un usuario con el correo electrónico proporcionado
                var usuario = db.User.FirstOrDefault(x => x.email == email);

                if (usuario != null)
                {
                    // Si el usuario existe, verificar si la contraseña coincide
                    if (usuario.password == password)
                    {
                        // Si la contraseña coincide, se encontró el usuario
                        return UserStatus.UsuarioValido; // Usuario encontrado y contraseña correcta
                    }
                    return UserStatus.ContraseñaIncorrecta; // Contraseña incorrecta
                }
                return UserStatus.UsuarioNoEncontrado; // No se encontró el usuario
            }
        }

        public static UserStatus VerificarEmailCode(string email, string code)
        {
            using (var db = DbConexion.Create())
            {
                var usuario = db.User.FirstOrDefault(x => x.email == email);

                if (usuario != null)
                {
                    if (usuario.code == code)
                    {
                        return UserStatus.UsuarioValido;
                    }
                    return UserStatus.CodeIncorrecta;
                }
                return UserStatus.UsuarioNoEncontrado;
            }
        }

        public static UserStatus VerificarEmail(string email)
        {
            using (var db = DbConexion.Create())
            {
                var usuario = db.User.FirstOrDefault(x => x.email == email);

                if (usuario != null)
                {
                    var code = EmailService.EnviarCodigoEmail(email);
                    ActualizarCode(email, code);
                    return UserStatus.UsuarioValido;
                }
                return UserStatus.UsuarioNoEncontrado;
            }
        }

        public static UserVMR ObtenerUsuarioPorCredenciales(string email, string password)
        {
            UserVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.User.Where(x => x.email == email && x.password == password && !x.borrado)
                                 .Select(x => new UserVMR
                                 {
                                     id = x.id,
                                     name = x.name,
                                     lastname = x.lastname,
                                     username = x.username,
                                     email = x.email,
                                     password = x.password,
                                     code = x.code,
                                     is_active = x.is_active,
                                     rol_id = x.rol_id,
                                     created_at = x.created_at
                                 }).FirstOrDefault();
            }

            return item;
        }

        // Version CREAR MANUAL
        public static long Crear(User item, int? rolId = null)
        {
            using (var db = DbConexion.Create())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Crear usuario
                        var user = new User
                        {
                            name = !string.IsNullOrEmpty(item.name) ? item.name : (string.IsNullOrEmpty(item.username) ? "" : item.username),
                            lastname = !string.IsNullOrEmpty(item.lastname) ? item.lastname : "",
                            username = !string.IsNullOrEmpty(item.username) ? item.username : "",
                            email = item.email,// este es Requerido 
                            password = item.password, // este es Requerido
                            code = CodeService.GenerarCodigoAleatorio(), // redordar que code admite string(20)
                            is_active = true, // hay que ponerlo de forma predeterminada el yes
                            rol_id = rolId ?? 2, // Si no se especifica, se usa 2 por defecto
                            created_at = DateTime.Now,
                            borrado = false
                        };

                        db.User.Add(user);
                        db.SaveChanges();

                        // Crear redes __ para el usuario ligado
                        var redes = new Redes
                        {
                            user_id = user.id,
                            youtube = null,  // Inicializa con valores vacíos o predeterminados
                            twitch = null,
                            kick = null,
                            twitter = null,
                            tiktok = null,
                            discord = null,
                            instagram = null,
                            facebook = null
                        };

                        db.Redes.Add(redes);
                        db.SaveChanges();

                        // Crear imagen __ para el usuario ligado
                        var image = new Image
                        {
                            src = null,
                            type = null,
                            created_at = DateTime.Now,
                            user_id = user.id,
                            borrado = false
                        };

                        db.Image.Add(image);
                        db.SaveChanges();

                        // Crear perfil __ para el usuario ligado
                        var profile = new Profile
                        {
                            day_of_birth = null,
                            phone = null,
                            title = !string.IsNullOrEmpty(user.username) ? user.username : null,
                            bio = null,
                            public_email = null,
                            image_id = image.id,
                            redes_id = redes.id,
                            user_id = user.id,
                            borrado = false
                        };

                        db.Profile.Add(profile);
                        db.SaveChanges();

                        // Commit transaction
                        transaction.Commit();

                        return user.id;
                    }
                    catch (Exception)
                    {
                        // Rollback transaction if any error occurs
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        // Funciones para actualizar USER 
        public static void Actualizar(UserVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.User.Find(item.id);

                if (itemUpdate == null)
                {
                    throw new Exception($"No se han introducido datos User a actualizar {item.id}");
                }

                if (itemUpdate != null)
                {
                    itemUpdate.name = item.name;
                    itemUpdate.lastname = item.lastname;
                    itemUpdate.username = item.username;
                    itemUpdate.email = item.email;
                    itemUpdate.password = item.password;
                    itemUpdate.code = item.code;
                    itemUpdate.is_active = item.is_active;
                    itemUpdate.rol_id = itemUpdate.rol_id;
                    itemUpdate.created_at = itemUpdate.created_at;

                    db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception($"No se han introducido datos User a actualizar {item.id}");
                }
            }
        }

        // el usuaraio === El id para sustituir === autenticar Moderador
        public static void ActualizarRol(int userId)
        {
            using (var db = DbConexion.Create())
            {
                var user = db.User.Find(userId);

                if (user != null)
                {
                    if (user.rol_id == 1)
                    {
                        user.rol_id = 2;
                    }
                    else if (user.rol_id == 2)
                    {
                        user.rol_id = 1;
                    }

                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("Error al actualizar rol, el usuario no existe");
                }
            }
        }

        public static void ActualizarRolAutorizado(int userId, int userModId)
        {
            using (var db = DbConexion.Create())
            {
                var mod = db.User.Find(userModId);
                var user = db.User.Find(userId);

                if (mod != null)
                {
                    if (mod.rol_id != 2)
                    {
                        if (user != null)
                        {
                            if (user.rol_id == 1)
                            {
                                user.rol_id = 2;
                            }
                            else if (user.rol_id == 2)
                            {
                                user.rol_id = 1;
                            }

                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Error al actualizar rol, el usuario no existe");
                        }
                    }
                    else
                    {
                        throw new Exception("Error, solo User Mod tiene autorización");
                    }
                }
                else
                {
                    throw new Exception("Error, User Mod no existe");
                }
            }
        }

        public static void ActualizarCode(string email, string code)
        {
            using (var db = DbConexion.Create())
            {
                var user = db.User.FirstOrDefault(x => x.email == email);

                if (user != null)
                {
                    user.code = code;
                }
                else
                {
                    throw new Exception("El usuario no existe.");
                }
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void ActualizarPassword(string email, string code, string newPassword)
        {
            using (var db = DbConexion.Create())
            {
                var user = db.User.FirstOrDefault(x => x.email == email && x.code == code);

                if (user != null)
                {
                    user.password = newPassword;
                    user.code = "00000000";
                }
                else
                {
                    throw new Exception("El usuario no existe o correo incorrecto.");
                }
                db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
        }

        // Funciones de ELIMINADO
        public static void Eliminar(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var items = db.User.Where(x => ids.Contains(x.id));

                foreach (var item in items)
                {
                    item.borrado = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

        public static void ResetTotal(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var perfiles = db.Profile.Where(x => ids.Contains(x.user_id));
                foreach (var perfil in perfiles)
                {
                    perfil.borrado = true;
                    db.Entry(perfil).State = System.Data.Entity.EntityState.Modified;
                }

                var images = db.Image.Where(x => x.user_id.HasValue && ids.Contains(x.user_id.Value));
                foreach (var image in images)
                {
                    image.borrado = true;
                    db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                }

                var items = db.User.Where(x => ids.Contains(x.id));
                foreach (var item in items)
                {
                    ResetCont(item.id);
                    item.borrado = true;
                    db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

        public static void RestoreTotal(List<long> ids)
        {
            using (var db = DbConexion.Create())
            {
                var users = db.User.Where(x => ids.Contains(x.id) && x.borrado);
                foreach (var user in users)
                {
                    RestoreCont(user.id);
                    user.borrado = false;
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                }

                var images = db.Image.Where(x => x.user_id.HasValue && ids.Contains(x.user_id.Value) && x.borrado);
                foreach (var image in images)
                {
                    image.borrado = false;
                    db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                }

                var perfiles = db.Profile.Where(x => ids.Contains(x.user_id) && x.borrado);
                foreach (var perfil in perfiles)
                {
                    perfil.borrado = false;
                    db.Entry(perfil).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

        public static void ResetCont(int userId)
        {
            using (var db = DbConexion.Create())
            {
                // Buscar todas las relaciones donde el usuario es user1_id o user2_id
                var relaciones = db.User1_User2.Where(x => x.user1_id == userId || x.user2_id == userId);

                foreach (var relacion in relaciones)
                {
                    relacion.borrado = true;
                    db.Entry(relacion).State = System.Data.Entity.EntityState.Modified;
                }

                db.SaveChanges();
            }
        }

        public static void RestoreCont(int userId)
        {
            using (var db = DbConexion.Create())
            {
                // Buscar todas las relaciones donde el usuario es user1_id y esté borrado
                var columUser1 = db.User1_User2.Where(x => x.user1_id == userId && x.borrado).ToList();

                foreach (var relation in columUser1)
                {
                    var user2 = db.User.Find(relation.user2_id);
                    if (user2 != null && !user2.borrado) // buscar todas las relaciones donde el user2 existe y no esté borrado
                    {
                        relation.borrado = false;
                        db.Entry(relation).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                var columUser2 = db.User1_User2.Where(x => x.user2_id == userId && x.borrado).ToList();

                foreach (var relation in columUser2)
                {
                    var user1 = db.User.Find(relation.user1_id);
                    if (user1 != null && !user1.borrado)
                    {
                        relation.borrado = false;
                        db.Entry(relation).State = System.Data.Entity.EntityState.Modified;
                    }
                }

                // Guardar los cambios en la base de datos
                db.SaveChanges();
            }
        }
    }
}
