//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Modelo.Modelos
{
    using System;
    using System.Collections.Generic;
    
    public partial class Redes
    {
        public int id { get; set; }
        public Nullable<int> user_id { get; set; }
        public string youtube { get; set; }
        public string twitch { get; set; }
        public string kick { get; set; }
        public string twitter { get; set; }
        public string tiktok { get; set; }
        public string discord { get; set; }
        public string instagram { get; set; }
        public string facebook { get; set; }
    
        public virtual User User { get; set; }
    }
}
