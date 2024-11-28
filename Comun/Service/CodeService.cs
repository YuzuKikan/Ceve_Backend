using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.Service
{
    public class CodeService
    {
        // Función externa para generar un código aleatorio de 8 cifras
        public static string GenerarCodigoAleatorio()
        {
            Random random = new Random();
            return random.Next(0, 99999999).ToString("D8");
        }
    }
}
