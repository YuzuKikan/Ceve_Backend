using Comun.ViewModels;
using Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.DAL
{
    public class RedesDAL
    {
        public static ListadoPaginadoVMR<RedesVMR> LeerTodo(int cantidad, int pagina, string textoBusqueda)
        {
            ListadoPaginadoVMR<RedesVMR> resultado = new ListadoPaginadoVMR<RedesVMR>();

            using (var db = DbConexion.Create())
            {
                var query = db.Redes.Select(x => new RedesVMR
                {
                    id = x.id,
                    user_id = x.user_id,
                    youtube = x.youtube,
                    twitch = x.twitch,
                    kick = x.kick,
                    twitter = x.twitter,
                    tiktok = x.tiktok,
                    discord = x.discord,
                    instagram = x.instagram,
                    facebook = x.facebook
                });
                resultado.cantidadTotal = query.Count();

                resultado.elemento = query.OrderBy(x => x.id).Skip(pagina * cantidad).Take(cantidad).ToList();
            }

            return resultado;
        }

        public static RedesVMR LeerUno(long userId)
        {

            RedesVMR item = null;

            using (var db = DbConexion.Create())
            {
                item = db.Redes.Where(x => x.user_id == userId).Select(x => new RedesVMR
                {
                    id = x.id,
                    user_id = x.user_id,
                    youtube = x.youtube,
                    twitch = x.twitch,
                    kick = x.kick,
                    twitter = x.twitter,
                    tiktok = x.tiktok,
                    discord = x.discord,
                    instagram = x.instagram,
                    facebook = x.facebook
                }).FirstOrDefault();
            }

            return item;
        }


        public static void Actualizar(RedesVMR item)
        {
            using (var db = DbConexion.Create())
            {
                var itemUpdate = db.Redes.FirstOrDefault(x => x.user_id == item.user_id);

                if (itemUpdate != null)
                {
                    itemUpdate.youtube = item.youtube;
                    itemUpdate.twitch = item.twitch;
                    itemUpdate.kick = item.kick;
                    itemUpdate.twitter = item.twitter;
                    itemUpdate.tiktok = item.tiktok;
                    itemUpdate.discord = item.discord;
                    itemUpdate.instagram = item.instagram;
                    itemUpdate.facebook = item.facebook;

                    db.Entry(itemUpdate).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("No se encontró ninguna entrada de redes sociales para el usuario proporcionado.");
                }
            }

        }
    }
}
