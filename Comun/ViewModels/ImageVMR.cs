using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comun.ViewModels
{
    public class ImageVMR
    {
        // definimos los campos que queremos devolver al fronend
        public int id { get; set; }
        public string src { get; set; }
        public Nullable<int> type { get; set; }
        public Nullable<System.DateTime> created_at { get; set; }
        public Nullable<int> user_id { get; set; }
    }
}
