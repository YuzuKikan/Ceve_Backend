using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(RolMetadato))]
    public partial class Rol
    {
    }
    public class RolMetadato
    {
        [StringLength(50)]
        public string description { get; set; }

    }
}
