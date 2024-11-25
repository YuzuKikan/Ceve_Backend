using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(RedesMetadato))]
    public partial class Redes
    {
    }
    public class RedesMetadato
    {
        [Required]
        public int user_id { get; set; }

        [StringLength(255)]
        public string youtube { get; set; }

        [StringLength(255)]
        public string twitch { get; set; }

        [StringLength(255)]
        public string kick { get; set; }

        [StringLength(255)]
        public string twitter { get; set; }

        [StringLength(255)]
        public string tiktok { get; set; }

        [StringLength(255)]
        public string discord { get; set; }

        [StringLength(255)]
        public string instagram { get; set; }

        [StringLength(255)]
        public string facebook { get; set; }

    }
}
