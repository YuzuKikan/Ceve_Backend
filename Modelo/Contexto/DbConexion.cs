using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// mismo namespace que el Modelo 
namespace Modelo.Modelos
{
    public partial class DbConexion : DbContext
    {
        private DbConexion(string stringConexion) : base(stringConexion)
        {
            // evitar la sobrecarga de los modelos // evita errores de referencia cíclico
            this.Configuration.LazyLoadingEnabled = false;
            // evitar que carguen los objetos antes de las referencias a otros objetos
            this.Configuration.ProxyCreationEnabled = false;
            // tiempo de espera de respuesta de BD // para evitar los tiempos excedidos
            this.Database.CommandTimeout = 900;
        }
        public static DbConexion Create()
        {
            return new DbConexion("name=DbConexion");
        }
    }
}
