using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.Modelos
{
    [MetadataType(typeof(User1_User2Metadato))]
    public partial class User1_User2
    {
    }
    public class User1_User2Metadato
    {
        public int user1_id { get; set; }
        public int user2_id { get; set; }
        public System.DateTime created_at { get; set; }

    }
}
