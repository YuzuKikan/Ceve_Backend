using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(UserMetadato))]
    public partial class User
    {
    }
    public class UserMetadato
    {

        [StringLength(50, ErrorMessage = "El campo Name no debe superar los 50 caracteres.")]
        public string name { get; set; }

        [StringLength(50, ErrorMessage = "El campo Lastname no debe superar los 50 caracteres.")]
        public string lastname { get; set; }

        [StringLength(50, ErrorMessage = "El campo Username no debe superar los 50 caracteres.")]
        public string username { get; set; }

        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [StringLength(255, ErrorMessage = "El campo Email no debe superar los 255 caracteres.")]
        [EmailAddress(ErrorMessage = "El campo Email debe ser una dirección válida.")]
        public string email { get; set; }

        [Required(ErrorMessage = "El campo Password es obligatorio.")]
        [StringLength(60, ErrorMessage = "El campo Password no debe superar los 60 caracteres.")]
        [MinLength(8, ErrorMessage = "La campo Password debe tener al menos 8 caracteres.")]
        public string password { get; set; }

        
        [StringLength(20)]
        public string code { get; set; }

        
        public bool? is_active { get; set; }

        [Range(1, 2, ErrorMessage = "El campo rol_id debe ser 1 (mod) o 2 (cliente).")]
        public int rol_id { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "El campo created_at debe ser una fecha válida.")]
        public System.DateTime created_at { get; set; }

    }
}
