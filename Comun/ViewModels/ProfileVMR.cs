using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class ProfileVMR
    {
        public int id { get; set; }
        public Nullable<System.DateTime> day_of_birth { get; set; }
        public Nullable<int> phone { get; set; }
        public string title { get; set; }
        public string bio { get; set; }
        public string public_email { get; set; }
        public Nullable<int> image_id { get; set; }
        public Nullable<int> redes_id { get; set; }
        public int user_id { get; set; }
    }
}
