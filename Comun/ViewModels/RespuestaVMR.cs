using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class RespuestaVMR<T>
    {
        // Obtiene el código de error cuando ocurra
        public HttpStatusCode codigo { get; set; }
        // no se obtendrán los datos según el código
        public T datos { get; set; }
        // imprime el mensaje en cada caso de error producido
        public List<string> mensajes { get; set; }
        // constructor de la clase genérica
        public RespuestaVMR()
        {
            codigo = HttpStatusCode.OK;
            datos = default(T);
            mensajes = new List<string>();
        }

    }
}
