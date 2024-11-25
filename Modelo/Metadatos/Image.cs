using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(ImageMetadato))]
    public partial class Image
    {
    }
    public class ImageMetadato
    {
        // [Required(ErrorMessage = "El campo src es requerido.")]
        public string src { get; set; }

        public Nullable<int> type { get; set; }

        // [Required]
        public System.DateTime created_at { get; set; }

        [Required]
        public int user_id { get; set; }

    }
}
