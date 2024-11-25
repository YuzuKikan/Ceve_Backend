using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(Type_ImageMetadato))]
    public partial class Type_Image
    {
    }
    public class Type_ImageMetadato
    {
        [StringLength(50)]
        public string description { get; set; }

    }
}
