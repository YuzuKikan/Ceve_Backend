using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class UserDetailDAL
    {

        public static ListadoPaginadoVMR<UserDetailVMR> LeerTodoConDetalles(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<UserDetailVMR> resultado = new ListadoPaginadoVMR<UserDetailVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.User.Where(x => !x.borrado)
                    .Join(db.Profile, u => u.id, p => p.user_id, (u, p) => new { u, p })
                    .Join(db.Image, up => up.p.image_id, i => i.id, (up, i) => new { up.u, up.p, i })
                    .Join(db.Redes, upi => upi.u.id, r => r.user_id, (upi, r) => new { upi.u, upi.p, upi.i, r })
                    .Select(x => new UserDetailVMR
                    {
                        id = x.u.id,
                        name = x.u.name + " " + (x.u.lastname != null ? (" " + x.u.lastname) : " "),
                        username = x.u.username ?? x.u.name,
                        email = x.u.email ?? " ",
                        is_active = x.u.is_active,
                        rol_id = x.u.rol_id,
                        created_at = x.u.created_at,
                        // Campo Profile
                        bio = x.p.bio,
                        // Campo Images
                        image_src = x.i.src,
                        // Campo Redes
                        redes = new RedesVMR
                        {
                            id = x.r.id,
                            user_id = x.r.user_id,
                            youtube = x.r.youtube,
                            twitch = x.r.twitch,
                            kick = x.r.kick,
                            twitter = x.r.twitter,
                            tiktok = x.r.tiktok,
                            discord = x.r.discord,
                            instagram = x.r.instagram,
                            facebook = x.r.facebook
                        }
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

        public static void ActualizarDatos(UserDetailVMR item)
        {
            using (var db = DbConexion.Create())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        // Actualizar datos de usuario
                        var user = db.User.Find(item.id);
                        if (user != null)
                        {
                            user.name = item.name;
                            user.lastname = item.lastname;
                            user.username = item.username;
                            user.email = item.email;
                            user.is_active = item.is_active;
                            user.rol_id = item.rol_id;
                            user.created_at = item.created_at; // Opcional

                            db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                        }

                        // Actualizar perfil
                        var profile = db.Profile.FirstOrDefault(p => p.user_id == item.id);
                        if (profile != null)
                        {
                            profile.bio = item.bio;
                            profile.public_email = item.email;
                            // Actualizar otros campos necesarios del perfil
                            db.Entry(profile).State = System.Data.Entity.EntityState.Modified;
                        }

                        // Actualizar imagen
                        var image = db.Image.FirstOrDefault(i => i.user_id == item.id);
                        if (image != null)
                        {
                            image.src = item.image_src;
                            // Actualizar otros campos necesarios de la imagen
                            db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                        }

                        // Actualizar redes
                        var redes = db.Redes.FirstOrDefault(r => r.user_id == item.id);
                        if (redes != null)
                        {
                            redes.youtube = item.redes.youtube;
                            redes.twitch = item.redes.twitch;
                            redes.kick = item.redes.kick;
                            redes.twitter = item.redes.twitter;
                            redes.tiktok = item.redes.tiktok;
                            redes.discord = item.redes.discord;
                            redes.instagram = item.redes.instagram;
                            redes.facebook = item.redes.facebook;

                            db.Entry(redes).State = System.Data.Entity.EntityState.Modified;
                        }

                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }


    }
}
