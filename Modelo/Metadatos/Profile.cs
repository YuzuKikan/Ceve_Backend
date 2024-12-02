using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(ProfileMetadato))]
    public partial class Profile
    {
    }
    public class ProfileMetadato
    {
        public Nullable<System.DateTime> day_of_birth { get; set; }

        [Range(100000000, 999999999, ErrorMessage = "El número de teléfono debe tener 9 dígitos.")]
        public Nullable<int> phone { get; set; }

        [StringLength(255, ErrorMessage = "El título no debe superar los 255 caracteres.")]
        public string title { get; set; }

        [StringLength(255, ErrorMessage = "La biografía no debe superar los 255 caracteres.")]
        public string bio { get; set; }

        [StringLength(255, ErrorMessage = "El campo Email no debe superar los 255 caracteres.")]
        [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección válida.")]
        public string public_email { get; set; }

        //[Required]
        public Nullable<int> image_id { get; set; }

        //[Required]
        public int redes_id { get; set; }

        [Required(ErrorMessage = "El campo 'user_id' es obligatorio.")]
        public int user_id { get; set; }

    }
}
