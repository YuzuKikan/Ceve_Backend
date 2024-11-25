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

        [StringLength(10)]
        public Nullable<int> phone { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [StringLength(255)]
        public string bio { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string public_email { get; set; }

        //[Required]
        public Nullable<int> image_id { get; set; }

        //[Required]
        public int redes_id { get; set; }

        [Required]
        public int user_id { get; set; }

    }
}
