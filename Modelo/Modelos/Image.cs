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
    
    public partial class Image
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Image()
        {
            this.Profile = new HashSet<Profile>();
        }
    
        public int id { get; set; }
        public string src { get; set; }
        public Nullable<int> type { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<int> user_id { get; set; }
        public bool borrado { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Profile> Profile { get; set; }
    }
}
