using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class User1_User2DAL
    {
        public static (int, List<(int userId, string userName)>) CantSeguimientos(int userId)
        {
            using (var db = DbConexion.Create())
            {
                var seguimientos = db.User1_User2
                    .Where(x => x.user1_id == userId && !x.borrado)
                    .Join(db.User,
                          seguimiento => seguimiento.user2_id,
                          usuario => usuario.id,
                          (seguimiento, usuario) => new
                          {
                              userId = usuario.id,
                              userName = usuario.name
                          })
                    .ToList();
                // creando el conteo del seguimientos y lista tuplas de usuarios siguiendo
                int cantidadSeguimientos = seguimientos.Count;
                var listaSeguimientos = seguimientos.Select(x => (x.userId, x.userName)).ToList();

                return (cantidadSeguimientos, listaSeguimientos);
            }
        }

        public static (int, List<(int seguidorId, string seguidorName)>) CantSeguidores(int userId)
        {
            using (var db = DbConexion.Create())
            {
                var seguidores = db.User1_User2
                    .Where(x => x.user2_id == userId && !x.borrado)
                    .Join(db.User,
                          seguimiento => seguimiento.user1_id,
                          usuario => usuario.id,
                          (seguimiento, usuario) => new
                          {
                              seguidorId = usuario.id,
                              seguidorName = usuario.name
                          })
                    .ToList();

                int cantidadSeguidores = seguidores.Count;  // Cuenta el número de seguidores    
                var listaSeguidores = seguidores.Select(x => (x.seguidorId, x.seguidorName)).ToList();  // Crea una lista de tuplas con seguidorId y seguidorName

                return (cantidadSeguidores, listaSeguidores);  // Retorna la cantidad y la lista de seguidores
            }
        }

        // Crear ahora realiza comprobaciones de => Existencia y Control de estado a positivo
        public static ResultDetail Crear(int user_remitente_id, int user_receptor_id)
        {
            using (var db = DbConexion.Create())
            {
                // Recoger todas las relaciones user_1 a user_2
                var existeFollow = db.User1_User2
                    .FirstOrDefault(x => x.user1_id == user_remitente_id && x.user2_id == user_receptor_id);

                // Verificar si ya existe un seguimiento entre user1_id y user2_id
                if (existeFollow != null)
                {
                    if (!existeFollow.borrado)
                    {
                        // Seguimiento ya activo, devolver el ID
                        return new ResultDetail
                        {
                            Id = existeFollow.id,
                            Mensaje = "El seguimiento ya existe y está activo."
                        };
                    }
                    else
                    {
                        // Seguimiento estaba borrado, reactivarlo
                        existeFollow.borrado = false;
                        db.Entry(existeFollow).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        return new ResultDetail
                        {
                            Id = existeFollow.id,
                            Mensaje = "El seguimiento fue reactivado."
                        };
                    }
                }

                // Si no existe un seguimiento, crear uno nuevo
                var nuevoSeguimiento = new User1_User2
                {
                    user1_id = user_remitente_id,
                    user2_id = user_receptor_id,
                    borrado = false,
                    created_at = DateTime.Now // Fecha de creación
                };
                db.User1_User2.Add(nuevoSeguimiento);
                db.SaveChanges();
                return new ResultDetail
                {
                    Id = nuevoSeguimiento.id,
                    Mensaje = "Nuevo seguimiento creado."
                };
            }
        }


        // CON UNFOLLOW SE BORRA    VOLVER FOLLOW   ==> ACTUALIZAR SI EXISTE UNFOLLOW   SINO    CREAR 
        // aún falta poner una condición de busqueda y comparacion de si existe
        public static void Actualizar(int user_remitente_id, int user_receptor_id)
        {
            using (var db = DbConexion.Create())
            {
                // Buscar si existe un seguimiento entre el usuario remitente y el receptor
                var seguimiento = db.User1_User2.FirstOrDefault(x => x.user1_id == user_remitente_id && x.user2_id == user_receptor_id);

                if (seguimiento != null)
                {
                    // Si existe el seguimiento, se actualiza estado de borrado a false
                    seguimiento.borrado = false;
                    db.Entry(seguimiento).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    // Si no existe el seguimiento, se crea uno nuevo entre ambos usuarios
                    var nuevoSeguimiento = new User1_User2
                    {
                        user1_id = user_remitente_id,
                        user2_id = user_receptor_id,
                        borrado = false,
                        created_at = DateTime.Now // Fecha de creación
                    };
                    db.User1_User2.Add(nuevoSeguimiento);
                }

                db.SaveChanges();
            }
        }



        // el borrado lógico en User1_User2 evita la creación y eliminación de varios seguimientos y tener un bd más limpio
        public static void Eliminar(int user_remitente_id, int user_receptor_id)
        {
            using (var db = DbConexion.Create())
            {
                var seguimiento = db.User1_User2.FirstOrDefault(x => x.user1_id == user_remitente_id && x.user2_id == user_receptor_id);

                if (seguimiento != null)
                {
                    seguimiento.borrado = true;
                    db.Entry(seguimiento).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                // Si ya está borrado, no se realiza nada
            }
        }
        // Acción de eliminar con resultados => state: bool
        public static bool EliminarBool(int user_remitente_id, int user_receptor_id)
        {
            using (var db = DbConexion.Create())
            {
                // Buscar si existe un seguimiento entre el usuario remitente y el receptor
                var seguimiento = db.User1_User2.FirstOrDefault(x => x.user1_id == user_remitente_id && x.user2_id == user_receptor_id);

                if (seguimiento != null)
                {
                    if (seguimiento.borrado)
                    {
                        return false; // El seguimiento ya está borrado, no hacemos nada y retornamos false
                    }
                    // Si el seguimiento existe, se cambia el estado de borrado a true
                    seguimiento.borrado = true;
                    db.Entry(seguimiento).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return true; // Se realizó la eliminación
                }
                // Si ya está borrado, no se realiza nada
            }
            // Si no se encontró o no existió en primer logar
            return false;
        }

    }
}
