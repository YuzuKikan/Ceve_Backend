using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class UserDetailVMR
    {
        public int id { get; set; }
        public string name { get; set; }
        public string lastname { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public Nullable<bool> is_active { get; set; }
        public Nullable<int> rol_id { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }


        public string bio { get; set; }

        public string image_src { get; set; }

        public RedesVMR redes { get; set; }


    }
}
